namespace Emi.Portal.Movil.Pages.MyAccount
{
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class RegisterMinorPage : ContentPage
    {
        public RegisterMinorPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }


    }
}
