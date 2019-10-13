using System;

namespace kr.bbon.Xamarin.Forms.Validations
{
    /// <summary>
    /// 최소값 유효성 검사 규칙
    /// </summary>
    /// <typeparam name="T">IComparable 인터페이스를 구현하는 형식</typeparam>
    public class MinValueRule<T> : IValidationRule<T> where T : IComparable
    {
        /// <summary>
        /// <see cref="MinValueRule"/> 클래스의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="min">최소값</param>
        /// <param name="includeMinValue">최소값 포함 여부</param>
        public MinValueRule(T min, bool includeMinValue = true)
        {
            this.min = min;
            this.includeMinValue = includeMinValue;
        }

        public string ValidationMessage
        {
            get => String.Format(validationMessage, min);
            set => validationMessage = value;
        }

        public bool Check(T value)
        {
            // value 가 min 보다 크면 1
            // 같으면 0
            // value가 min 보다 작으면 -1
            var result = value.CompareTo(min);

            if (includeMinValue)
            {
                return result >= 0;
            }
            else
            {
                return result > 0;
            }
        }

        private readonly T min;
        private string validationMessage;
        private readonly bool includeMinValue;
    }
}
