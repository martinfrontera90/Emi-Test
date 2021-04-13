using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emi.Portal.Movil.Pages.MedicalCenterCoordination
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulesMedicalCenterCoordinationPage : ContentPage
    {
        public SchedulesMedicalCenterCoordinationPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}