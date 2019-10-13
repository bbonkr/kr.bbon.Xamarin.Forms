using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms.Controls
{
    /// <summary>
    /// 빈값을 허용하는 <see cref="TimePicker"/> 확장 뷰 컨트롤입니다.
    /// </summary>
    public class NullableTimePicker : TimePicker
    {
        public static readonly BindableProperty NullableTimeProperty = BindableProperty.Create(nameof(NullableTime),
            typeof(TimeSpan?),
            typeof(NullableTimePicker),
             defaultBindingMode: BindingMode.TwoWay,
            defaultValue: default(TimeSpan?),
            propertyChanged: (bindable, oldValue, newValue) => {
                var ctl = (NullableTimePicker)bindable;
                var newTimeSpanValue = (TimeSpan?)newValue;
                if (newTimeSpanValue.HasValue)
                {
                    ctl.Format = ctl.TimeFormat;
                    ctl.Time = newTimeSpanValue.Value;
                }
                else
                {
                    ctl.Format = "시각을 선택하세요.";
                }
            });

        public static readonly BindableProperty TimeFormatProperty = BindableProperty.Create(
            nameof(TimeFormat),
            typeof(string),
            typeof(NullableTimePicker),
            defaultValue: "HH:mm",
            defaultBindingMode: BindingMode.OneWay,
            propertyChanging: (bindable, oldValue, newValue) =>
            {
                var ctrl = (NullableDatePicker)bindable;
                ctrl.DateFormat = (string)newValue;
            });

        /// <summary>
        /// 시각
        /// </summary>
        public TimeSpan? NullableTime
        {
            get
            {
                return (TimeSpan?)GetValue(NullableTimeProperty);
            }
            set
            {
                SetValue(NullableTimeProperty, value);

                //UpdateTime();
            }
        }

        /// <summary>
        /// 시각 출력 형식
        /// </summary>
        public string TimeFormat
        {
            get => (string)GetValue(TimeFormatProperty);
            set => SetValue(TimeFormatProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            UpdateTime();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Time))
            {
                NullableTime = Time;
            }
        }

        private void UpdateTime()
        {
            if (NullableTime.HasValue)
            {
                Format = TimeFormat;
                Time = NullableTime.Value;
            }
            else
            {
                Format = "시각을 선택하세요.";
            }
        }
    }
}
