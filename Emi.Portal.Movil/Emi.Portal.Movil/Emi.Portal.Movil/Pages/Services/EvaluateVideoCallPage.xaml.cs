namespace Emi.Portal.Movil.Pages.Services
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EvaluateVideoCallPage : ContentPage
    {
        public EvaluateVideoCallPage()
        {
            InitializeComponent();

            if (Device.iOS == Device.RuntimePlatform)
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
        }
    }
}
