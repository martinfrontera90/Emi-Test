namespace Emi.Portal.Movil.Pages.Services
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class PediatricPage : ContentPage
    {
        public PediatricPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
