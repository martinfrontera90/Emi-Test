namespace Emi.Portal.Movil.Pages.MyHealth
{
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.MyHealth;
    using Xamarin.Forms;

    public partial class SearchServicesHistoryPage : ContentPage
    {
        public SearchServicesHistoryPage()
        {
            InitializeComponent();

            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }

            ServicesHistoryPageViewModel servicesHistory = (ServicesHistoryPageViewModel)BindingContext;

            servicesHistory.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "SetUserAutenticated")
                {
                    if (patients.Items.Count > 0)
                    {
                        patients.SelectedIndex = 0;
                    }
                }
            };
        }
    }
}
