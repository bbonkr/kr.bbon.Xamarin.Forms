using kr.bbon.Xamarin.Forms.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kr.bbon.Xamarin.Forms
{
    /// <summary>
    /// 유효성 검사를 제공하는 데이터 객체입니다.
    /// </summary>
    /// <typeparam name="T">값의 형식을 지정합니다.</typeparam>
    public class ValidatableObject<T> : NotifyPropertyChangedObject, IValidity
    {
        /// <summary>
        /// <see cref="ValidatableObject"/> 클래스의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="label">엔트리 레이블로 사용될 문자열</param>
        public ValidatableObject(string label)
        {
            this.label = label;
            isValid = true;
            errors = new List<string>();
            validations = new List<IValidationRule<T>>();
        }

        /// <summary>
        /// <see cref="ValidatableObject"/> 클래스의 인스턴스를 초기화합니다.
        /// </summary>
        public ValidatableObject() : this("")
        {

        }

        /// <summary>
        /// 유효성 검사 규칙 목록
        /// </summary>
        public List<IValidationRule<T>> Validations => validations;

        /// <summary>
        /// 유효성 검사 실패 메시지
        /// </summary>
        public List<string> Errors
        {
            get => errors;
            set => SetProperty(ref errors, value);
        }

        /// <summary>
        /// 값을 나타냅니다.
        /// </summary>
        public T Value
        {
            get => value;
            set => SetProperty(ref this.value, value);
        }

        /// <summary>
        /// 유효성 검사 통과 여부를 나타냅니다.
        /// </summary>
        public bool IsValid
        {
            get => isValid;
            set => SetProperty(ref isValid, value);
        }

        /// <summary>
        /// 유효성 검사를 실행합니다.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        /// <summary>
        /// 유효성 검사 규칙을 추가합니다.
        /// </summary>
        /// <param name="rule"></param>
        public void AddValidationRule(IValidationRule<T> rule)
        {
            Validations.Add(rule);
            //IsValid = false;
        }

        /// <summary>
        /// 유효성 검사 규칙을 추가합니다.
        /// </summary>
        /// <param name="rules"></param>
        public void AddValidationRules(IEnumerable<IValidationRule<T>> rules)
        {
            if (rules == null || rules.Count() == 0) { return; }

            foreach (var rule in rules)
            {
                AddValidationRule(rule);
            }
        }

        public void AddValidationRules(params IValidationRule<T>[] rules)
        {
            if (rules == null || rules.Length == 0) { return; }

            foreach (var rule in rules)
            {
                AddValidationRule(rule);
            }

            //AddValidationRules(rules);
        }

        public string Label
        {
            get => label;
        }

        private readonly List<IValidationRule<T>> validations;
        private List<string> errors;
        private T value;
        private bool isValid;
        private string label;
    }
}
