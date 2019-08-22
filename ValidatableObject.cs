using kr.bbon.Xamarin.Forms.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kr.bbon.Xamarin.Forms
{
    public class ValidatableObject<T> : NotifyPropertyChangedObject, IValidity
    {
        public ValidatableObject()
        {
            isValid = true;
            errors = new List<string>();
            validations = new List<IValidationRule<T>>();
        }

        public List<IValidationRule<T>> Validations => validations;

        public List<string> Errors
        {
            get
            {
                return errors;
            }
            set
            {
                SetProperty(ref errors, value);
            }
        }

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                SetProperty(ref this.value, value);
            }
        }

        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                SetProperty(ref isValid, value);
            }
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        private readonly List<IValidationRule<T>> validations;
        private List<string> errors;
        private T value;
        private bool isValid;
    }
}
