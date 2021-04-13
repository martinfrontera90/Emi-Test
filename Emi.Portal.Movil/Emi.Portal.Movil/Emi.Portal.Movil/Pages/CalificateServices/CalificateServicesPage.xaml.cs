using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emi.Portal.Movil.Pages.CalificateServices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QualifyServicesPage : ContentPage
    {
        public QualifyServicesPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                if (Device.iOS == Device.RuntimePlatform)
                {
                    NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
                }
            }
        }
    }
}