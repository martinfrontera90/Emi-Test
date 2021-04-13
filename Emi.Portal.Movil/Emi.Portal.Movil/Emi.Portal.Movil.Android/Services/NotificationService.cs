using Emi.Portal.Movil.Droid.Services;
using Emi.Portal.Movil.Logic.Contracts.Services;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationService))]
namespace Emi.Portal.Movil.Droid.Services
{

    using Android.App;
    using Firebase.Iid;

    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class NotificationService : FirebaseInstanceIdService, INotificationService
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