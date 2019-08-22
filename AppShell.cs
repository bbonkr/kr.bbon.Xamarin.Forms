using Autofac;

namespace kr.bbon.Xamarin.Forms
{
    public class AppShell : ShellBase
    {

    }

    public class AppShell<TViewModel> : AppShell
       where TViewModel : ViewModelBase
    {
        public AppShell()
            : base()
        {
            using (var scope = AppContainer.Instance.Container.BeginLifetimeScope())
            {
                var viewModelObj = scope.Resolve<TViewModel>();

                viewModelObj.AttachAppearingEvent(this);
                viewModelObj.AttachDisappearingEvent(this);
                viewModelObj.AttachOnLoadEvent(this);

                this.viewModel = viewModelObj;
            }

            if (viewModel != null)
            {
                BindingContext = viewModel;
            }
        }

        public TViewModel ViewModel { get => viewModel; }

        private readonly TViewModel viewModel;
    }
}
