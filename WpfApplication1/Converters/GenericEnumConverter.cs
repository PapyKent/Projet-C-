using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TodoListUCBL.WPFView.Converters
{


    public class GenericEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (!(value is Enum))
            {
                throw new ArgumentException("Value must be an enum", "value");
            }

            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }



            bool result = false;

            if (parameter is string)
            {
                result = Enum.Equals(value, Enum.Parse(value.GetType(), parameter.ToString().Trim(), true));
            }

            if (targetType == typeof(bool) || targetType == typeof(bool?))
            {
                return result;
            }
            else if (targetType == typeof(Visibility))
            {
                if (result)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            else
            {
                throw new ArgumentException("Epected target type bool", "targetType");
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}