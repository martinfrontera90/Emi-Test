namespace Emi.Portal.Movil.Pages.Services
{
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class PreCallPage : ContentPage
    {
        bool ValidateConection = true;

        public PreCallPage()
        {
            InitializeComponent();

            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IPrecallViewModel viewModel)
            {
                viewModel.RemovePage();
                if (ValidateConection)
                    viewModel.ValidateConection();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ValidateConection = false;
        }
    }
}
