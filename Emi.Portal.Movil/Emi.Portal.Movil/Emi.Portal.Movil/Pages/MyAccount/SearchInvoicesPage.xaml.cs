using Emi.Portal.Movil.Logic.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Emi.Portal.Movil.Pages.MyAccount
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchInvoicesPage : ContentPage
    {
        public SearchInvoicesPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
        }
    }
}