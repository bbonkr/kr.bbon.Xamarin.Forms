using System;

namespace kr.bbon.Xamarin.Forms.Validations
{
    /// <summary>
    /// 문자열 최대 길이 제한 규칙 (문자열의 길이가 최대값 이하인 경우 유효합니다.)
    /// </summary>
    public class MaxLengthRule : IValidationRule<string>
    {
        public string ValidationMessage
        {
            get => String.Format(validationMessage, MaximumLength);
            set => validationMessage = value;
        }

        /// <summary>
        /// 최대 길이 
        /// </summary>
        public int MaximumLength { get; set; } = 0;

        public bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }

            return value.Length <= MaximumLength;
        }

        private string validationMessage;
    }
}
