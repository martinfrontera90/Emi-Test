namespace Emi.Portal.Movil.Pages.Services
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    public partial class SchedulePediatricPage : ContentPage
    {
        public SchedulePediatricPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
