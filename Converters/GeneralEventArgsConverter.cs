using System;
using System.Globalization;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms.Converters
{
    public class GeneralEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EventArgs)
            {
                return value;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
