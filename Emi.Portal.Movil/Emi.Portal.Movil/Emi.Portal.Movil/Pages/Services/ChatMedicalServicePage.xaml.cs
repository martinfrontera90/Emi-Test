namespace Emi.Portal.Movil.Pages.Services
{
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatMedicalServicePage : ContentPage
    {
        IDialogService dialogService;
        public ChatMedicalServicePage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}