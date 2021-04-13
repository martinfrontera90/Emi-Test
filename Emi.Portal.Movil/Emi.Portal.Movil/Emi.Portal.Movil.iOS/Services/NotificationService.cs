using Emi.Portal.Movil.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationService))]
namespace Emi.Portal.Movil.iOS.Services
{
    using Emi.Portal.Movil.Logic.Contracts.Services;

    public class NotificationService : INotificationService
    {
        public void RegisterNotifications()
        {
            App.Context.RegisterNotifications();
        }

        public void UnregisterNotifications()
        {
            App.Context.UnregisterNotifications();
        }
    }
}
