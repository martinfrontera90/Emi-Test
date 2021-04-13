using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emi.Portal.Movil.Pages.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubMenuPage : ContentPage
    {
        public SubMenuPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}