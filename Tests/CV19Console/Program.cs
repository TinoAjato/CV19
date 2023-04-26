﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CV19Console
{
    internal class Program
    {
        const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        static void Main( string[] args )
        {
            var client = new HttpClient();
            var response = client.GetAsync( data_url ).Result;
            var csv_str = response.Content.ReadAsStringAsync().Result;



            Console.ReadLine();
        }
    }
}
