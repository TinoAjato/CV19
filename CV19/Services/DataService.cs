using CV19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CV19.Services
{
    internal class DataService
    {
        private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";


        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDates();

            var data = GetContriesData().GroupBy( d => d.Contry );

            foreach (var country_info in data)
            {
                var country = new CountryInfo {
                    Name = country_info.Key,
                    ProvinceCounts = country_info.Select( pi => new PlaceInfo {
                        Name = pi.Province,
                        Location = new System.Windows.Point( pi.Place.Lat, pi.Place.Lon ),
                        Counts = dates.Zip( pi.Counts, ( date, count ) => new ConfirmedCount { Date = date, Count = count } )
                    } )
                };

                yield return country;
            }
        }

        private static async Task<Stream> GetDataStream()
        {
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync( _DataSourceAddress, HttpCompletionOption.ResponseHeadersRead );
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using Stream data_stream = GetDataStream().Result;
            using StreamReader data_reader = new( data_stream );
            while (!data_reader.EndOfStream)
            {
                string? line = data_reader.ReadLine();
                if (string.IsNullOrEmpty( line ))
                {
                    continue;
                }
                yield return line.Replace( "Korea,", "Korea -" ).Replace( "Bonaire,", "Bonaire -" );
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split( ',' )
            .Skip( 4 )
            .Select( s => DateTime.Parse( s, CultureInfo.InvariantCulture ) )
        .ToArray();

        private static IEnumerable<(string Province, string Contry, (double Lat, double Lon) Place, int[] Counts)> GetContriesData()
        {
            var lines = GetDataLines()
                .Skip( 1 )
                .Select( line => line.Split( ',' ) );

            foreach (var item in lines)
            {
                var province = item[0].Trim();
                var contry_name = item[1].Trim( ' ', '"' );
                var latitude = double.Parse( item[2] );
                var longitude = double.Parse( item[3] );
                var counts = item.Skip( 4 ).Select( int.Parse ).ToArray();

                yield return (province, contry_name, (latitude, longitude), counts);
            }
        }
    }
}
