using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using kr.bbon.Xamarin.Forms.Abstractions;
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
                var appCenterdiagnosticsService = scope.Resolve<IAppCenterDiagnosticsService>();

                vm.InitializeWithData(data);
                vm.AttachAppearingEvent(this);
                vm.AttachDisappearingEvent(this);
                vm.AttachOnLoadEvent(this);

                viewModel = vm;
                diagnosticsService = appCenterdiagnosticsService;
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

        public IAppCenterDiagnosticsService DiagnosticsService { get => diagnosticsService; }

        /// <summary>
        /// ViewModel 의 인스턴스를 BindingContext 속성에 바인딩합니다.
        /// <para>InitializeComponent(); 메서드 실행 후 호출해야 합니다.</para>
        /// </summary>
        protected void SetViewModelAsBindingContext()
        {
            BindingContext = ViewModel;
        }

        private readonly TViewModel viewModel;
        public readonly IAppCenterDiagnosticsService diagnosticsService;
        private bool isLoaded = false;
    }
}
