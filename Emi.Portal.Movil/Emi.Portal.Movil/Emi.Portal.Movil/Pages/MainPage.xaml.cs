namespace Emi.Portal.Movil.Pages
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    public partial class MainPage : NavigationPage
    {
        public MainPage(Page page) : base(page)
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
