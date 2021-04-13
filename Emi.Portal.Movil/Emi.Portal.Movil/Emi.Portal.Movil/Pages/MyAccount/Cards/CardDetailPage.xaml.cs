namespace Emi.Portal.Movil.Pages.MyAccount.Cards
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class CardDetailPage : ContentPage
    {
        public CardDetailPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
