using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class LocationPointToStr : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if (value is not Point point)
            {
                return null;
            }

            return $"Lat:{point.X};Lon:{point.Y}";
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if (value is not string str)
            {
                return null;
            }

            var components = str.Split( ';' );

            var latitude_str = components[0].Split( ':' )[1];
            var longitude_str = components[1].Split( ':' )[1];

            var latitude = double.NaN;
            double.TryParse( latitude_str, NumberStyles.AllowDecimalPoint, new CultureInfo( "en-us" ), out latitude );

            var longitude = double.NaN;
            double.TryParse( longitude_str, NumberStyles.AllowDecimalPoint, new CultureInfo( "en-us" ), out longitude );

            return new Point( latitude, longitude );
        }
    }
}