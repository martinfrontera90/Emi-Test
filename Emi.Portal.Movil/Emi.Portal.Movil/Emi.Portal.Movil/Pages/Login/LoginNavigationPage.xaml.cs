namespace Emi.Portal.Movil.Pages.Login
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginNavigationPage : NavigationPage
    {
        public LoginNavigationPage(Page page) : base(page)
        {
            InitializeComponent();
            SetTitleIcon(this, AppConfigurations.IconNavigationBar);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Navigator = this;
        }
    }
}