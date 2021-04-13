namespace Emi.Portal.Movil.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Droid.Service;
    using Firebase;
    using Firebase.Iid;
    using WindowsAzure.Messaging;
    using Xamarin;
    using Emi.Portal.Movil.Droid.Services;

    [Activity(Label = "ucm", Icon = "@drawable/icon", Theme = "@style/MainTheme",
    ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IContext
    {
        ProgressDialog progressDialog;
        IExceptionService exceptionService;
        private readonly string TAG = "MainActivity";
        NotificationHub hub;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            PlatformOpenTokService.Init();
            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            progressDialog = new ProgressDialog(this);
            progressDialog.Indeterminate = true;
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.SetCancelable(false);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);
            FormsGoogleMaps.Init(this, bundle);
            FirebaseApp.InitializeApp(this);
            Acr.UserDialogs.UserDialogs.Init(this);
            AiForms.Dialogs.Dialogs.Init(this);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            App.Context = this;

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                    }
                }
            }

            string url = "";
            if (MessagingService.WebContentList != null)
            {
                if(MessagingService.WebContentList.ContainsKey("Url"))
                    url = MessagingService.WebContentList["Url"];
                MessagingService.WebContentList = null;
            }


            var id = Intent.GetStringExtra("id");
            var message = Intent.GetStringExtra("message");
            var code = Intent.GetStringExtra("code");

            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            builder.DetectFileUriExposure();

            LoadApplication(new App(id, message, code, url));
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                var navigatorStackCount = App.Navigator == null ? 0 : App.Navigator.Navigation.NavigationStack.Count;

                if (navigatorStackCount <= 1)
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle(AppConfigurations.Brand);
                    builder.SetMessage(AppResources.ExitApplicationMessage);
                    builder.SetCancelable(false);
                    builder.SetPositiveButton(AppResources.YesButtonText, delegate { Process.KillProcess(Process.MyPid()); });
                    builder.SetNegativeButton(AppResources.NoButtonText, delegate { });
                    builder.Show();

                    return false;
                }
            }
            return base.OnKeyUp(keyCode, e);
        }

        public void RegisterNotifications()
        {
            try
            {
                var refreshedToken = FirebaseInstanceId.Instance.Token;
                SendRegistrationToServer(refreshedToken);
            }
            catch (Exception ex)
            {
                exceptionService = ServiceLocator.Current.GetInstance<IExceptionService>();
                exceptionService.RegisterException(ex);
            }

        }

        private void SendRegistrationToServer(string token)
        {
            ILoginViewModel login = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            hub = new NotificationHub(AppConfigurations.NotificationHubName, AppConfigurations.NotificationHubConnectionString, this);

			var tags = new List<string>
			{
				//"ucm"
			};

			if (login != null && login.User != null && string.IsNullOrEmpty(login.User.UserName) == false)
            {
                //tags.Add(login.User.UserName);
				tags.Add(ServiceLocator.Current.GetInstance<ILoginViewModel>().User.UserName);
			}

            try
            {
                Task.Run(() =>
                {
                    hub.Unregister();
                    var regID = hub.Register(token, tags.ToArray());
                });

            }
            catch (Exception ex)
            {
                exceptionService = ServiceLocator.Current.GetInstance<IExceptionService>();
                exceptionService.RegisterException(ex);
            }
        }

        public void UnregisterNotifications()
        {
            try
            {
                hub = new NotificationHub(AppConfigurations.NotificationHubName, AppConfigurations.NotificationHubConnectionString, this);

                Task.Run(() =>
                {
                    hub.Unregister();
                });
            }
            catch (Exception ex)
            {
                exceptionService = ServiceLocator.Current.GetInstance<IExceptionService>();
                exceptionService.RegisterException(ex);
            }
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

