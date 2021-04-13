
using Emi.Portal.Movil.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace Emi.Portal.Movil.Droid.Services
{
    using Android.App;
    using Emi.Portal.Movil.Logic.Contracts.Services;

    public class CloseApplication : ICloseApplication
	{
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}
