using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms
{
    public static class ColorExtensions
    {
        public static string ToHexString(this Color color)
        {
            Func<double, int> getIntegerValue = (value) => (int)(value * 255);

            var hexString = $"#{getIntegerValue(color.A):X2}{getIntegerValue(color.R):X2}{getIntegerValue(color.G):X2}{getIntegerValue(color.B):X2}";

            return hexString;
        }
    }
}
