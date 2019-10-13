using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms.Controls
{
    /// <summary>
    /// 빈값을 허용하는 <see cref="DatePicker"/> 확장 뷰 컨트롤입니다.
    /// </summary>
    public class NullableDatePicker : DatePicker
    {
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate),
            typeof(DateTime?),
            typeof(NullableDatePicker),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: default(DateTime?),
            propertyChanged: (bindable, oldValue, newValue) => {

                var ctl = (NullableDatePicker)bindable;
                var newDateTimeValue = (DateTime?)newValue;
                if (newDateTimeValue.HasValue)
                {
                    ctl.Format = ctl.DateFormat;
                    ctl.Date = newDateTimeValue.Value;
                }
                else
                {
                    ctl.Format = "날짜를 선택하세요.";
                }
            });

        public static readonly BindableProperty DateFormatProperty = BindableProperty.Create(
            nameof(DateFormat),
            typeof(string),
            typeof(NullableDatePicker),
            defaultValue: default(string),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (NullableDatePicker)bindable;
                ctrl.DateFormat = (string)newValue;
            });

        public DateTime? NullableDate
        {
            get
            {
                return (DateTime?)GetValue(NullableDateProperty);
            }
            set
            {
                SetValue(NullableDateProperty, value);

                //UpdateDate();
            }
        }

        public string DateFormat
        {
            get => (string)GetValue(DateFormatProperty);
            set => SetValue(DateFormatProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            UpdateDate();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Date))
            {
                NullableDate = Date;
            }
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                Format = DateFormat;
                Date = NullableDate.Value;
            }
            else
            {
                Format = "날짜를 선택하세요.";
            }
        }
    }
}
