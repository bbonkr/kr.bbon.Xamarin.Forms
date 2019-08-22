using System;

namespace kr.bbon.Xamarin.Forms.Validations
{
    public class SameValueRule<T> : IValidationRule<T>
    {
        /// <summary>
        /// 유효성 검사 메시지
        /// </summary>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// 값 비교 대상 객체 
        /// </summary>
        public ValidatableObject<T> CompareTo { get; set; }

        /// <summary>
        /// 대소문자 무시 여부; 값의 비교는 문자열로 처리합니다. 
        /// </summary>
        public bool IgnoreCase { get; set; } = true;

        public bool Check(T value)
        {
            if (CompareTo == null)
            {
                throw new ArgumentException("비교 대상이 설정되지 않았습니다.");
            }

            if (value == null && CompareTo.Value == null)
            {
                // 대상값과 현재값이 모두 빈값이면 유효한 것으로 처리합니다.
                return true;
            }

            if (value == null)
            {
                return false;
            }

            var stringValue = value.ToString();
            var compareToValue = CompareTo.Value.ToString();

            return stringValue.Equals(compareToValue, IgnoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture);
        }
    }
}
