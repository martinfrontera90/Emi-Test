using Emi.Portal.Movil.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessagingService))]
namespace Emi.Portal.Movil.Droid.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Android.App;
    using Android.Content;
    using Android.Graphics;
    using Android.Media;
    using Android.OS;
    using Android.Support.V4.App;
    using Firebase.Messaging;
    using Java.Lang;

    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]

    public class MessagingService : FirebaseMessagingService
    {
        const string TAG = "MessagingService";
        public static Dictionary<string, string> WebContentList { get; set; }

        public override void OnMessageReceived(RemoteMessage message)
        {
            if (message.GetNotification() != null)
            {
                SendNotification(message.GetNotification().Body);
            }
            else
            {
                WebContentList = new Dictionary<string, string>(message.Data);
                //SendNotification(message.Data.Values.First());
                SendNotification(message.Data["message"]);
            }
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));

            intent.AddFlags(ActivityFlags.ClearTop);

            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            Bitmap largeIcon = BitmapFactory.DecodeResource(Resources, Resource.Drawable.icon);

            ICharSequence notificationChannelName = new String("ucm");

            NotificationChannel mChannel = Build.VERSION.SdkInt >= BuildVersionCodes.O ? new NotificationChannel("ucm", notificationChannelName, NotificationImportance.High) : null;

            var notificationBuilder = new NotificationCompat.Builder(this)
             .SetContentTitle("ucm")
             .SetContentText(messageBody)
             .SetContentIntent(pendingIntent)
             .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
             .SetStyle(new NotificationCompat.BigTextStyle()
             .BigText(messageBody))
             .SetLargeIcon(largeIcon)
             .SetAutoCancel(true)
             .SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 })
             .SetLights(Color.Yellow, 3000, 3000);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                notificationBuilder.SetColor(Resource.Color.NotificationsColor);
            }

            notificationBuilder.SetSmallIcon(Resource.Drawable.icon);

            var notificationManager = NotificationManager.FromContext(this);

            if (mChannel != null)
            {
                notificationBuilder.SetChannelId("ucm");
                notificationManager.CreateNotificationChannel(mChannel);
            }

            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }

}
