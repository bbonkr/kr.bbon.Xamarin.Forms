using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace kr.bbon.Xamarin.Forms
{
    public abstract class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// 멤버 속성의 값을 설정합니다.
        /// </summary>
        /// <typeparam name="T">멤버 속성의 형식</typeparam>
        /// <param name="store">멈버 속성을 저장하는 필드</param>
        /// <param name="value">멤버 속성에 저장할 값</param>
        /// <param name="propertyName">멤버 속성 이름</param>
        /// <param name="onChanged">값 변경된 경우 실행</param>
        /// <param name="validateFunc">값을 변경하기 위한 조건</param>
        /// <returns>값 변경 여부</returns>
        protected virtual bool SetProperty<T>(ref T store, T value, [CallerMemberName] string propertyName = "", Action onChanged = null, Func<T, T, bool> validateFunc = null, bool overwrite = false)
        {
            if (!overwrite && EqualityComparer<T>.Default.Equals(store, value))
            {
                // 기존 값과 갱신 요청 값이 같으면 변경하지 않습니다.
                return false;
            }

            if (validateFunc != null && !validateFunc(store, value))
            {
                // 유효성 검사가 있는 경우 유효하지 않으면 변경하지 않습니다.
                return false;
            }

            store = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);

            return true;
        }
    }
}



