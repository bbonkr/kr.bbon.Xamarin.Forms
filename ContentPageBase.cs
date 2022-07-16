using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Xamarin.Forms;

namespace kr.bbon.Xamarin.Forms
{
    public class ContentPageBase : ContentPage
    {
        public ContentPageBase()
               : base()
        {

        }
    }

    public class AppContentPage<TViewModel> : ContentPageBase<TViewModel> where TViewModel : ViewModelBase
    {
        public AppContentPage() : base() { }

        public AppContentPage(object data) : base(data) { }
    }

    [Obsolete("ContentPageBase<TViewModel> 클래스는 제거될 예정입니다. AppContentPage<TViewModel> 클래스를 사용하십시오.")]
    public class ContentPageBase<TViewModel> : ContentPageBase where TViewModel : ViewModelBase
    {
        public ContentPageBase() : this(null)
        {

        }

        public ContentPageBase(object data)
            : base()
        {
            var configurationAction = new Action<ContainerBuilder>((builder) =>
            {
                builder.RegisterInstance(Navigation).As<INavigation>();
            });

            // ViewModel 을 준비합니다.
            using (var scope = AppContainer.Instance.Container.BeginLifetimeScope(configurationAction))
            {
                var vm = scope.Resolve<TViewModel>();

                vm.InitializeWithData(data);
                vm.AttachAppearingEvent(this);
                vm.AttachDisappearingEvent(this);
                vm.AttachOnLoadEvent(this);

                viewModel = vm;
            }

            this.Appearing += ContentPageBase_Appearing;
        }

        private void ContentPageBase_Appearing(object sender, EventArgs e)
        {
            if (!isLoaded)
            {
                SetViewModelAsBindingContext();

                isLoaded = true;
            }
        }

        public TViewModel ViewModel { get => viewModel; }


        /// <summary>
        /// ViewModel 의 인스턴스를 BindingContext 속성에 바인딩합니다.
        /// <para>InitializeComponent(); 메서드 실행 후 호출해야 합니다.</para>
        /// </summary>
        private void SetViewModelAsBindingContext()
        {
            BindingContext = ViewModel;
        }

        private readonly TViewModel viewModel;
        private bool isLoaded = false;
    }
}
