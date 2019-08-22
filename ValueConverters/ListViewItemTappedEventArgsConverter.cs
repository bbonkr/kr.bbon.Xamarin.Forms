using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms.ValueConverters
{
    public class ListViewItemTappedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemTappedEventArgs)
            {
                var args = value as ItemTappedEventArgs;
                if (args != null)
                {
                    return new ListViewTappedItem
                    {
                        Group = args.Group,
                        Item = args.Item,
                        ItemIndex = args.ItemIndex,
                    };
                }
            }

            return ListViewTappedItem.Empty;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
