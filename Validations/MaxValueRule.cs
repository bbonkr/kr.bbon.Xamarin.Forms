using System;
using System.Collections.Generic;
using System.Text;

namespace kr.bbon.Xamarin.Forms.Validations
{
    /// <summary>
    /// 최대값 유효성 검사 규칙
    /// </summary>
    /// <typeparam name="T">IComparable 인터페이스를 구현하는 형식</typeparam>
    public class MaxValueRule<T> : IValidationRule<T> where T : IComparable
    {
        /// <summary>
        /// <see cref="MaxValueRule"/> 클래스의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="max">최대값</param>
        /// <param name="includeMinValue">최대값 포함 여부</param>
        public MaxValueRule(T max, bool includeMinValue = true)
        {
            this.max = max;
            this.includeMinValue = includeMinValue;
        }

        public string ValidationMessage
        {
            get => String.Format(validationMessage, max);
            set => validationMessage = value;
        }

        public bool Check(T value)
        {
            // value 가 min 보다 크면 1
            // 같으면 0
            // value가 min 보다 작으면 -1
            var result = value.CompareTo(max);

            if (includeMinValue)
            {
                return result <= 0;
            }
            else
            {
                return result < 0;
            }
        }

        private readonly T max;
        private string validationMessage;
        private readonly bool includeMinValue;
    }
}
