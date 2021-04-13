using System;
using System.Collections.Generic;
using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;

namespace Emi.Portal.Movil.Pages.MyHealth
{
    public partial class ProductsAndPlansPage : ContentPage
    {
        public ProductsAndPlansPage()
        {
            InitializeComponent();
            if(Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIconImageSource(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
