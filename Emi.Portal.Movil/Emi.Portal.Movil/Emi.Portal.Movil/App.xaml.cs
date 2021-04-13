namespace Emi.Portal.Movil
{
    using Emi.Portal.Movil.Infrastructure;
    using Emi.Portal.Movil.Logic.Contracts;
    using Emi.Portal.Movil.Logic.VideoCall;
    using Emi.Portal.Movil.Pages.Loader;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Analytics;
    using Microsoft.AppCenter.Crashes;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using AndroidSpecific = Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public static IContext Context { get; set; }
        public App(string id, string message, string code, string url)
        {
            InitializeComponent();
            AndroidSpecific.Application.SetWindowSoftInputModeAdjust(this, AndroidSpecific.WindowSoftInputModeAdjust.Resize);
            MainPage = new LoaderPage(id, message, code, url);            
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=93e5c76e-cc3e-4840-9c37-347b8c8eba20;" + "android=1d251b4f-9d95-44f4-86bc-6af2e0f57251;", typeof(Analytics), typeof(Crashes));
            CrossOpenTok.Current.Error += (m) => MainPage.DisplayAlert("ERROR", m, "OK");
            Plugin.InputKit.Shared.Controls.RadioButton.GlobalSetting.Color = Color.Blue;
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static InstanceLocator Locator
        {
            get
            {
                return (InstanceLocator)App.Current.Resources["Locator"];
            }
        }

        public static NavigationPage Navigator { get; set; }
    }
}
