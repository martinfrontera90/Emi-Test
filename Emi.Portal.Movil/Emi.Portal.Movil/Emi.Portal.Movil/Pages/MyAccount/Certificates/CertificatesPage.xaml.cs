namespace Emi.Portal.Movil.Pages.MyAccount.Certificates
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class CertificatesPage : ContentPage
    {
        public CertificatesPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
