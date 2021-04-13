namespace Emi.Portal.Movil.Pages.Register
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterDataPersonalPage : ContentPage
	{
		public RegisterDataPersonalPage ()
		{
			InitializeComponent ();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }
	}
}