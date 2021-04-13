namespace Emi.Portal.Movil.Pages.Controls
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class PdfPage : ContentPage
    {
        public PdfPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
