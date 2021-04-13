namespace Emi.Portal.Movil.Pages.Home
{
    using System;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : MasterDetailPage
    {

        public MenuPage(Page page = null)
        {
            Icon = "menu.png";
            InitializeComponent();
            Master.Icon = "menu.png";
            
            Detail = new MainPage((Page)Activator.CreateInstance(typeof(LandingPage)));

            MenuList.ItemSelected += (sender, args) =>
            {
                if (args.SelectedItem is MenuItemViewModel menuItemViewModel && menuItemViewModel.SelectCommand != null)
                {                    
                    menuItemViewModel.SelectCommand.Execute(null);
                    IsPresented = false;
                }
                MenuList.SelectedItem = null;
            };
        }
    }
}