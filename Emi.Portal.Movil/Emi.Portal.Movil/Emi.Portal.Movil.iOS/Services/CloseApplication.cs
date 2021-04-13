
using Emi.Portal.Movil.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace Emi.Portal.Movil.iOS
{
    using System.Threading;
    using Emi.Portal.Movil.Logic.Contracts.Services;

    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
