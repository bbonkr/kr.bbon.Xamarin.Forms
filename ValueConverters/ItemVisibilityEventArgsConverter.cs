using System;
using System.Globalization;
using Xamarin.Forms;


namespace kr.bbon.Xamarin.Forms.ValueConverters
{
    public class ItemVisibilityEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemVisibilityEventArgs)
            {
                var args = value as ItemVisibilityEventArgs;

                if (args != null)
                {
                    return new ListViewVisibilityItem
                    {
                        Item = args.Item,
                        ItemIndex = args.ItemIndex,
                    };
                }
            }

            return ListViewVisibilityItem.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
