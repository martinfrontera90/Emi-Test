namespace Emi.Portal.Movil.Pages.MyHealth
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class ExpiredMedicalServicesPage : ContentPage
    {
        public ExpiredMedicalServicesPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
