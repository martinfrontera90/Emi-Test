using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emi.Portal.Movil.Pages.CustomerService
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FaqsPage : ContentPage
    {
        public FaqsPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}