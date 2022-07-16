using System;
using System.Collections.Generic;
using System.Text;

namespace kr.bbon.Xamarin.Forms.Validations
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string ?? value.ToString();

            return !String.IsNullOrWhiteSpace(str);
        }
    }
}
