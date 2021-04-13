namespace Emi.Portal.Movil.Pages.Services
{
    using System.Linq;
    using Emi.Portal.Movil.Logic;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    public partial class ServicesPage : ContentPage
    {
        ServicesPageViewModel servicesPageViewModel;

        public ServicesPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }

            servicesPageViewModel = (ServicesPageViewModel)BindingContext;
            servicesPageViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "NewAddressAdded")
                {
                    pickerAddresses.SelectedItem = servicesPageViewModel.Addresses.Select(x => x).Last();
                }
            };
        }

    }
}
