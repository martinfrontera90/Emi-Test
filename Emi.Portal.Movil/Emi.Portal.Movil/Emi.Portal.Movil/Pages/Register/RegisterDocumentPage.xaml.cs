namespace Emi.Portal.Movil.Pages.Register
{
    using System;
    using Emi.Portal.Movil.Controls;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterDocumentPage : ContentPage
    {
        public RegisterDocumentPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker myPicker = (sender as Picker);
            NumDocument.Keyboard = Keyboard.Numeric;
            Document document = (Document)myPicker.SelectedItem;
            if (document != null && document.Name == "Pasaporte")
                NumDocument.Keyboard = Keyboard.Text;
        }
    }
}