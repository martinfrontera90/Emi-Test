using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emi.Portal.Movil.Pages.Loader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoaderPage : ContentPage
    {
        string id;
        string message;
        string code;
        string url;
        public LoaderPage(string id, string message, string code, string url)
        {
            InitializeComponent();
            this.id = id;
            this.message = message;
            this.code = code;
            this.url = url;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await App.Locator.LoaderPage.Start(id, message, code, url);            
        }
    }
}

