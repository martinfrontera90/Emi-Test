namespace Emi.Portal.Movil.iOS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CoreGraphics;
    using Emi.Portal.Movil.iOS.Controls;
    using Emi.Portal.Movil.Logic.Contracts;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;
    using Foundation;
    using Microsoft.AppCenter.Analytics;
    using CommonServiceLocator;
    using UIKit;
    using WindowsAzure.Messaging;
    using Emi.Portal.Movil.OpenTok.iOS.Services;

    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IContext
    {
        UIActivityIndicator activityIndicator;
        SBNotificationHub Hub { get; set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            PlatformOpenTokService.Init();
            global::Xamarin.Forms.Forms.Init();
            AiForms.Dialogs.Dialogs.Init();
            Xamarin.FormsMaps.Init();
            Xamarin.FormsGoogleMaps.Init(AppConfigurations.IosGoogleMapsAPIKey);
            App.Context = this;

            Firebase.Core.App.Configure();
            Rg.Plugins.Popup.Popup.Init();
            Plugin.InputKit.Platforms.iOS.Config.Init();

            CGRect bounds = UIScreen.MainScreen.Bounds;
            if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight)
            {
                bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
            }
            activityIndicator = new UIActivityIndicator(bounds);

            string id = null;
            string alert = null;
            string code = null;
            string url = string.Empty;

            if (options != null && options.Keys != null && options.Keys.Count() != 0 && options.ContainsKey(new NSString("UIApplicationLaunchOptionsRemoteNotificationKey")))
            {
                var remoteNotificationKey = options.ObjectForKey(new NSString("UIApplicationLaunchOptionsRemoteNotificationKey")) as NSDictionary;

                if (null != remoteNotificationKey && remoteNotificationKey.ContainsKey(new NSString("aps")))
                {
                    NSDictionary aps = remoteNotificationKey.ObjectForKey(new NSString("aps")) as NSDictionary;

                    if (aps.ContainsKey(new NSString("id")))
                        id = (aps[new NSString("id")] as NSString).ToString();

                    if (aps.ContainsKey(new NSString("alert")))
                        alert = (aps[new NSString("alert")] as NSString).ToString();

                    if (aps.ContainsKey(new NSString("code")))
                        code = (aps[new NSString("code")] as NSString).ToString();

                    if (aps.ContainsKey(new NSString("Url")))
                        url = (aps[new NSString("Url")] as NSString).ToString();
                }
            }

            LoadApplication(new App(id, alert, code, url));
            
            return base.FinishedLaunching(app, options);
        }

        public void RegisterNotifications()
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    UIUserNotificationSettings pushSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
                    UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                    UIApplication.SharedApplication.RegisterForRemoteNotifications();
                }
                else
                {
                    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
                }
            }
            catch (Exception ex)
            {
                Dictionary<string, string> properties = new Dictionary<string, string>();
                properties.Add("Message", ex.Message);
                properties.Add("Source", ex.Source);
                properties.Add("StackTrace", ex.StackTrace);
                Analytics.TrackEvent("NotificationRegistrationError", properties);
            }
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            //base.FailedToRegisterForRemoteNotifications(application, error);
        }

        public void UnregisterNotifications()
        {
            UIApplication.SharedApplication.UnregisterForRemoteNotifications();
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Hub = new SBNotificationHub(AppConfigurations.NotificationHubConnectionString, AppConfigurations.NotificationHubName);

            Hub.UnregisterAllAsync(deviceToken);

            Hub.UnregisterAll(deviceToken, (error) =>
            {
                if (error != null)
                {
                    Console.WriteLine("Error calling Unregister: {0}", error.ToString());
                    return;
                }

                List<string> tags = new List<string>();

                tags.Add(AppConfigurations.Environment);

                if (ServiceLocator.Current.GetInstance<ILoginViewModel>() != null && ServiceLocator.Current.GetInstance<ILoginViewModel>().User != null)
                {
                    tags.Add(ServiceLocator.Current.GetInstance<ILoginViewModel>().User.UserName);
                }

                Hub.RegisterNative(deviceToken, new NSSet(tags.ToArray()), (errorCallback) =>
                {
                    if (errorCallback != null)
                        Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
                });
            });
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            ProcessNotification(userInfo, false);
        }

        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string alert = string.Empty;
                string id = string.Empty;
                string message = string.Empty;
                string code  = string.Empty;
                string url = string.Empty;

                if (aps.ContainsKey(new NSString("alert")))
                    alert = (aps[new NSString("alert")] as NSString).ToString();

                if (aps.ContainsKey(new NSString("id")))
                    id = (aps[new NSString("id")] as NSString).ToString();

                if (aps.ContainsKey(new NSString("message")))
                    message = (aps[new NSString("message")] as NSString).ToString();

                if (aps.ContainsKey(new NSString("code")))
                    code = (aps[new NSString("code")] as NSString).ToString();

                if (aps.ContainsKey(new NSString("Url")))
                    url = (aps[new NSString("Url")] as NSString).ToString();

                if (!fromFinishedLaunching && !string.IsNullOrEmpty(alert))
                {
                    UIAlertView avAlert = new UIAlertView(AppResources.ApplicationName, alert, null, "OK"); //TODO: Usar los recursos
                    avAlert.Clicked += (sender, buttonArgs) =>
                    {
                        App.Locator.LoaderPage.Start(id, message, code, url);
                    };

                    avAlert.Show();
                }
            }
        }
    }
}
