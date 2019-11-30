using kr.bbon.Xamarin.Forms.Abstractions;
using kr.bbon.Xamarin.Forms.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms
{
    /// <summary>
    /// 뷰모델 추상형식을 제공합니다.
    /// </summary>
    public abstract class ViewModelBase : NotifyPropertyChangedObject
    {
        private const string LOG_TAG = nameof(ViewModelBase);

        /// <summary>
        /// ViewModelBase 클래스의 인스턴스를 생성합니다.
        /// </summary>
        public ViewModelBase()
        {
            AddValidations();
            InitializeCommands();
        }

        #region Member Property

        private string title = String.Empty;
        private string subtitle = String.Empty;
        private string icon = String.Empty;
        private bool isBusy = false;
        private bool isNotBusy = true;
        private bool canLoadMore = false;
        private string header = String.Empty;
        private string footer = String.Empty;

        /// <summary>
        /// 제목을 나타냅니다.
        /// </summary>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        /// <summary>
        /// 부제목을 나타냅니다.
        /// </summary>
        public string Subtitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }

        /// <summary>
        /// 아이콘을 나타냅니다.
        /// </summary>
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        /// <summary>
        /// 동작중을 나타냅니다.
        /// </summary>
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (SetProperty(ref isBusy, value))
                {
                    IsNotBusy = !isBusy;
                }
            }
        }

        /// <summary>
        /// 동작중이 아님을 나타냅니다.
        /// </summary>
        public bool IsNotBusy
        {
            get => isNotBusy;
            set
            {
                if (SetProperty(ref isNotBusy, value))
                {
                    IsBusy = !isNotBusy;
                }
            }
        }

        /// <summary>
        /// 내용을 더 읽을 수 있는지 여부를 나타냅니다.
        /// </summary>
        public bool CanLoadMore
        {
            get => canLoadMore;
            set => SetProperty(ref canLoadMore, value);
        }

        /// <summary>
        /// 머릿글을 나타냅니다.
        /// </summary>
        public string Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        /// <summary>
        /// 바닥글을 나타냅니다.
        /// </summary>
        public string Footer
        {
            get => footer;
            set => SetProperty(ref footer, value);
        }

        #endregion

        /// <summary>
        /// 디버깅에 사용되는 식별자를 가져옵니다.
        /// </summary>
        protected string DebugIdentifier
        {
            get
            {
                var debugIdentifier = String.Empty;
#if DEBUG
                if (Debugger.IsAttached)
                {
                    // 디버깅에만 사용합니다.
                    debugIdentifier = $"[{ Guid.NewGuid().ToString()}]";
                }
#endif
                return debugIdentifier;
            }
        }

        /// <summary>
        /// 메인 페이지를 가져옵니다.
        /// </summary>
        public Page MainPage
        {
            get { return Application.Current.MainPage; }
        }

        /// <summary>
        /// View 에서 ViewModel로 초기 데이터를 전달합니다.
        /// </summary>
        /// <param name="obj">초기화에 사용될 데이터</param>
        public virtual void InitializeWithData(object obj) { }

        /// <summary>
        /// page.Appearing 이벤트를 추가합니다.
        /// </summary>
        /// <param name="page"></param>
        public virtual void AttachAppearingEvent(Page page)
        {
            page.Appearing -= PageOnAppearing;
            page.Appearing += PageOnAppearing;
        }

        /// <summary>
        /// page.Disappearing 이벤트를 추가합니다.
        /// </summary>
        /// <param name="page"></param>
        public virtual void AttachDisappearingEvent(Page page)
        {
            page.Disappearing -= PageOnDisappearing;
            page.Disappearing += PageOnDisappearing;
        }

        /// <summary>
        /// 페이지 로드 이벤트를 추가합니다.
        /// <para>페이지 로드 이벤트: page.Disappearing 이벤트를 최초 한번 실행</para>
        /// </summary>
        /// <param name="page"></param>
        public virtual void AttachOnLoadEvent(Page page)
        {
            page.Appearing -= PageOnLoad;
            page.Appearing += PageOnLoad;
        }

        /// <summary>
        /// page.Appearing 이벤트에서 처리할 작업을 지정합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void PageOnAppearing(object sender, EventArgs e) { }

        /// <summary>
        /// page.Disappearing 이벤트에서 처리할 작업을 지정합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void PageOnDisappearing(object sender, EventArgs e) { }

        /// <summary>
        /// 페이지 로드 이벤트에서 처리할 작업을 지정합니다.
        /// <para>페이지 로드 이벤트: page.Disappearing 이벤트를 최초 한번 실행</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnLoad(object sender, EventArgs e) { }

        /// <summary>
        /// 커맨드를 초기화합니다.
        /// </summary>
        protected virtual void InitializeCommands() { }

        /// <summary>
        /// 입력 유효성 검사 규칙을 추가합니다.
        /// </summary>
        protected virtual void AddValidations() { }

        /// <summary>
        /// 인수로 제공된 객체의 공개 속성(public property)를 기준으로 뷰모델의 속성 값을 설정합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        protected virtual void SetPropertyValue<T>(T dataSource)
        {
            var propertyFlags =
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.CreateInstance
                ;

            var thisProperies = this.GetType().GetProperties(propertyFlags);
            var objectProperties = typeof(T).GetProperties(propertyFlags);
            foreach (var property in objectProperties)
            {
                var propertyValue = property.GetValue(dataSource);
                var propertyName = property.Name;

                var thisProperty = thisProperies.Where(x => x.Name == propertyName)
                    .FirstOrDefault();

                if (thisProperty != null && propertyValue != null)
                {
                    thisProperty.SetValue(this, propertyValue);
                }
            }
        }

        protected virtual T MergeObject<T>(T baseOn, T mergedData)
        {
            var propertyFlags =
               System.Reflection.BindingFlags.Instance |
               System.Reflection.BindingFlags.Public |
               System.Reflection.BindingFlags.CreateInstance
               ;

            var thisProperies = typeof(T).GetProperties(propertyFlags);

            foreach (var property in thisProperies)
            {
                var propertyValue = property.GetValue(mergedData);
                var propertyName = property.Name;

                var thisProperty = thisProperies.Where(x => x.Name == propertyName)
                    .FirstOrDefault();

                if (thisProperty != null && propertyValue != null)
                {
                    thisProperty.SetValue(baseOn, propertyValue);
                }
            }

            return baseOn;
        }

        protected void ChangeCanExecute(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            ((Command)command).ChangeCanExecute();
        }

        private void PageOnLoad(object sender, EventArgs e)
        {
            if (!IsPageLoaded)
            {
                OnLoad(sender, e);

                IsPageLoaded = true;
            }
        }


        /// <summary>
        /// 확인창을 출력합니다.
        /// </summary>
        /// <param name="title">제목</param>
        /// <param name="message">메시지</param>
        /// <param name="accept">긍정 버튼 표시 문자열</param>
        /// <param name="cancel">부정 버튼 표시 문자열</param>
        /// <param name="callback">사용자 버튼 선택 후 실행</param>
        /// <returns></returns>
        protected virtual Task<bool> Confirm(string title, string message, string accept, string cancel, Action<bool> callback = null)
        {
            var result = false;
            var _title = title;
            var _accept = accept;
            var _cancel = cancel;
            if (String.IsNullOrWhiteSpace(_title))
            {
                _title = "확인";
            }

            if (String.IsNullOrWhiteSpace(_accept))
            {
                _accept = "예";
            }

            if (String.IsNullOrWhiteSpace(_cancel))
            {
                _cancel = "아니오";
            }

            XamarinSupport.RunOnMainThread(async () =>
            {
                result = await MainPage.DisplayAlert(_title, message, _accept, _cancel);
                callback?.Invoke(result);
            });

            return Task.FromResult(result);
        }

        /// <summary>
        /// 확인창을 출력합니다. 제목: 확인
        /// </summary>
        /// <param name="message">메시지</param>
        /// <param name="accept">긍정 버튼 표시 문자열</param>
        /// <param name="cancel">부정 버튼 표시 문자열</param>
        /// <returns></returns>
        protected virtual Task<bool> Confirm(string message, string accept, string cancel)
        {
            return Confirm(null, message, accept, cancel);
        }

        /// <summary>
        /// 확인창을 출력합니다. 제목: 확인; 긍정: 예; 부정: 아니오
        /// </summary>
        /// <param name="message">메시지</param>
        /// <returns></returns>
        protected virtual Task<bool> Confirm(string message)
        {
            return Confirm(null, message, null, null);
        }

        /// <summary>
        /// 알림창을 엽니다.
        /// </summary>
        /// <param name="title">제목</param>
        /// <param name="message">메시지</param>
        /// <param name="cancel">닫기 버튼 표시 문자열</param>
        /// <param name="callback">사용자 닫기 버튼 선택 후 실행</param>
        /// <returns></returns>
        protected virtual Task Alert(string title, string message, string cancel, Action callback = null)
        {
            var _title = title;
            var _cancel = cancel;

            if (String.IsNullOrWhiteSpace(_title))
            {
                _title = "알림";
            }

            if (String.IsNullOrWhiteSpace(_cancel))
            {
                _cancel = "확인";
            }

            XamarinSupport.RunOnMainThread(async () =>
            {
                await MainPage.DisplayAlert(_title, message, _cancel);

                callback?.Invoke();
            });

            return Task.CompletedTask;
        }

        /// <summary>
        /// 알림창을 엽니다. 제목: 알림
        /// </summary>
        /// <param name="message">메시지</param>
        /// <param name="cancel">닫기 버튼 표시 문자열</param>
        /// <returns></returns>
        protected virtual async Task Alert(string message, string cancel)
        {
            await Alert(null, message, cancel);
        }

        /// <summary>
        /// 알림창을 엽니다. 제목: 알림; 닫기: 확인
        /// </summary>
        /// <param name="message">메시지</param>
        /// <returns></returns>
        protected virtual async Task Alert(string message)
        {
            await Alert(null, message, null);
        }

     
        private bool IsPageLoaded = false;
    }
}



