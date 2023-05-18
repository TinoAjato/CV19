using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ToArray : MultiConverter
    {
        public override object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
        {
            var collection = new CompositeCollection();
            foreach (var obj in values)
            {
                collection.Add( obj );
            }
            return collection;
        }

        //public override object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
        //{
        //    return value as object[];
        //}
    }
}