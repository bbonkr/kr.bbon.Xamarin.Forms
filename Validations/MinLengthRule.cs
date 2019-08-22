using System;

namespace kr.bbon.Xamarin.Forms.Validations
{
    /// <summary>
    /// 문자열 최소 길이 제한 규칙 (문자열의 길이가 최소값 이상인 경우 유효합니다.)
    /// </summary>
    public class MinLengthRule : IValidationRule<string>
    {
        public string ValidationMessage
        {
            get => String.Format(validationMessage, MinimumLength);
            set => validationMessage = value;
        }

        /// <summary>
        /// 최소 길이 
        /// </summary>
        public int MinimumLength { get; set; } = 0;

        public bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }

            return value.Length >= MinimumLength;
        }


        private string validationMessage;
    }
}
