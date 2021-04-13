using System;
using System.Collections.Generic;
using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;

namespace Emi.Portal.Movil.Pages.Services
{
    public partial class SurveyQueuingPage : ContentPage
    {
        public SurveyQueuingPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}
