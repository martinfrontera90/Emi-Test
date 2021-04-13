using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;
namespace Emi.Portal.Movil.Pages.CustomerService
{
    public partial class NewPQRSPage : ContentPage
    {
        public NewPQRSPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
