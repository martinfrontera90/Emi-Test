﻿namespace Emi.Portal.Movil.Pages.MyAccount.Cards
{
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;

    public partial class CardsPage : ContentPage
    {
        public CardsPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
