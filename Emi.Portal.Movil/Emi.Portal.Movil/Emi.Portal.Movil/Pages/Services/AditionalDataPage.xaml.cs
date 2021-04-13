using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emi.Portal.Movil.Pages.Services
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AditionalDataPage : ContentPage
    {
        public AditionalDataPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}