using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Webkit;
using Android.Widget;
using Emi.Portal.Movil.Droid.Services;
using Emi.Portal.Movil.Logic.Contracts.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneService))]
namespace Emi.Portal.Movil.Droid.Services
{
    public class PhoneService : IPhoneService
    {
        public void Call(string phoneNumber)
        {
            Intent phone = new Intent(Intent.ActionCall, Android.Net.Uri.Parse(string.Format("tel:{0}", phoneNumber)));

            ((MainActivity)Forms.Context).StartActivity(phone);
        }

        public bool CanOpenUrl(string url)
        {
            //Este metodo no se usa en Android
            return true;
        }

        public string DeviceId
        {
            get
            {
                return Plugin.DeviceInfo.CrossDeviceInfo.Current.Id;
            }
        }

        public bool IsiOS
        {
            get
            {
                return false;
            }
        }

        public int ApplicationIconBadgeNumber { get; set; }

        public CultureInfo CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture;
            }
        }

        public string GetIpAddress()
        {
            var adresses = Dns.GetHostAddresses(Dns.GetHostName());
            if (adresses != null && adresses[0] != null)
            {
                return adresses[0].ToString();
            }
            else
            {
                return null;
            }
        }

        public void OpenUrl(string url)
        {
            try
            {
                Uri uri;
                if (Uri.TryCreate(url, UriKind.Absolute, out uri))
                    Device.OpenUri(new Uri(url));
                else
                    throw new Exception($"'{url}' no es una url válida.");
            }
            catch
            {
                throw new Exception($"'{url}' no es una url válida.");
            }
        }

        public void OpenApp(string name)
        {
            var context = Forms.Context;

            Intent launchIntent = new Intent();
            launchIntent = context.PackageManager.GetLaunchIntentForPackage(name);

            if (launchIntent != null)
            {
                context.StartActivity(launchIntent);//null pointer check in case package name was not found
            }
            else
            {
                try
                {
                    context.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + name)));
                }
                catch (Exception e)
                {
                    context.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=" + name)));
                }
            }
        }

        public Task RunOnUIThread(Action action)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        public async Task<string> SaveFiles(string filename, byte[] bytes)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(documentsPath, filename);
            File.WriteAllBytes(filePath, bytes);
            OpenFile(filePath, filename);
            return filePath;

        }
        public void OpenFile(string filePath, string filename)
        {

            var bytes = File.ReadAllBytes(filePath);

            //Copy the private file's data to the EXTERNAL PUBLIC location
            string externalStorageState = global::Android.OS.Environment.ExternalStorageState;
            string application = "";

            string extension = System.IO.Path.GetExtension(filePath);

            switch (extension.ToLower())
            {
                case ".doc":
                case ".docx":
                    application = "application/msword";
                    break;
                case ".pdf":
                    application = "application/pdf";
                    break;
                case ".xls":
                case ".xlsx":
                    application = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                    application = "image/jpeg";
                    break;
                default:
                    application = "*/*";
                    break;
            }

            try
            {
                var externalPath = global::Android.OS.Environment.ExternalStorageDirectory.Path + "/" + filename + extension;
                File.WriteAllBytes(externalPath, bytes);

                Java.IO.File file = new Java.IO.File(externalPath);
                file.SetReadable(true);
                Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, application);
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

                Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Forms.Context, "There's no app to open dpf files", ToastLength.Short).Show();
            }
        }

        public void DeleteCookie()
        {
            CookieManager.Instance.RemoveAllCookies(null);
        }
    }
}
