using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace kr.bbon.Xamarin.Forms.Abstractions
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}
