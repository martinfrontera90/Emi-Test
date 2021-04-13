namespace Emi.Portal.Movil.Pages.CustomerService
{
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class PQRSPage : TabbedPage
    {
        public PQRSPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is IPQRSPageViewModel viewModel)
                viewModel.CleanFirstForm();
        }
    }
}
