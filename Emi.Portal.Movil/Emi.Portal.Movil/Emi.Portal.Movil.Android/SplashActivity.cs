namespace Emi.Portal.Movil.Droid
{
    using System.Threading;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;

    [Activity(Label = "ucm", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/Theme.Splash",
        ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ThreadPool.QueueUserWorkItem(o => LoadActivity());
        }

        private void LoadActivity()
        {                       
            RunOnUiThread(() =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            });
        }
    }
}