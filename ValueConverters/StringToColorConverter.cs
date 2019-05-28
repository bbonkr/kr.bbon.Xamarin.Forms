using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms.ValueConverters
{
    public class StringToColorConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || String.IsNullOrWhiteSpace(value.ToString()))
            {
                return Color.FromHex("#FFFFFFFF");
            }

            string hex = value.ToString();

            if (!hex.StartsWith("#"))
            {
                hex = $"#{hex}";
            }

            return Color.FromHex(hex);
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hexString = String.Empty;

            var color = (Color)value;

            hexString = color.ToHexString();

            return hexString;
        }
    }

    public class StringToTransparentColorConverter : StringToColorConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)base.Convert(value, targetType, parameter, culture);

            if (parameter != null)
            {
                double alpha = 0;

                if (!double.TryParse(parameter.ToString(), out alpha))
                {
                    alpha = .8;
                }

                if (color.A < alpha)
                {
                    alpha = color.A;
                }

                color = Color.FromRgba(color.R, color.G, color.B, alpha);
            }

            return color;
        }
    }
}
