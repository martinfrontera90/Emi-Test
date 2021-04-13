using Emi.Portal.Movil.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneService))]
namespace Emi.Portal.Movil.iOS.Services
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Foundation;
    using QuickLook;
    using UIKit;
    using Xamarin.Forms;

    public class PhoneService : IPhoneService
    {
        public void Call(string phoneNumber)
        {
            Device.OpenUri(new Uri("tel:" + phoneNumber));
        }

        public bool CanOpenUrl(string url)
        {
            return UIApplication.SharedApplication.CanOpenUrl(new NSUrl(url));
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
                return true;
            }
        }

        public int ApplicationIconBadgeNumber
        {
            get { return (int)UIApplication.SharedApplication.ApplicationIconBadgeNumber; }
            set { UIApplication.SharedApplication.ApplicationIconBadgeNumber = value; }
        }

        public CultureInfo CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture;
            }
        }

        public void OpenUrl(string url)
        {
            Uri uri;
            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
                Device.OpenUri(new Uri(url));
            else
                throw new Exception($"'{url}' no es una url válida.");
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

        public string GetIpAddress()
        {
            string ipAddress = null;

            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = addrInfo.Address.ToString();
                        }
                    }
                }
            }

            return ipAddress;
        }

        public async Task<string> SaveFiles(string filename, byte[] bytes)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(documentsPath, filename);
            File.WriteAllBytes(filePath, bytes);
            OpenPDF(filePath);
            return filePath;
        }

        public void OpenPDF(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            QLPreviewController previewController = new QLPreviewController();
            previewController.DataSource = new PDFPreviewControllerDataSource(fi.FullName, fi.Name);

            UINavigationController controller = FindNavigationController();
            if (controller != null)
                controller.PresentViewController(previewController, true, null);
        }

        private UINavigationController FindNavigationController()
        {
            foreach (var window in UIApplication.SharedApplication.Windows)
            {
                if (window.RootViewController.NavigationController != null)
                    return window.RootViewController.NavigationController;
                else
                {
                    UINavigationController val = CheckSubs(window.RootViewController.ChildViewControllers);
                    if (val != null)
                        return val;
                }
            }

            return null;
        }

        private UINavigationController CheckSubs(UIViewController[] controllers)
        {
            foreach (var controller in controllers)
            {
                if (controller.NavigationController != null)
                    return controller.NavigationController;
                else
                {
                    UINavigationController val = CheckSubs(controller.ChildViewControllers);
                    if (val != null)
                        return val;
                }
            }
            return null;
        }

        public class PDFItem : QLPreviewItem
        {
            string title;
            string uri;

            public PDFItem(string title, string uri)
            {
                this.title = title;
                this.uri = uri;
            }

            public override string ItemTitle
            {
                get { return title; }
            }

            public override NSUrl ItemUrl
            {
                get { return NSUrl.FromFilename(uri); }
            }
        }

        public class PDFPreviewControllerDataSource : QLPreviewControllerDataSource
        {
            string url = "";
            string filename = "";

            public PDFPreviewControllerDataSource(string url, string filename)
            {
                this.url = url;
                this.filename = filename;
            }

            public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
            {
                return new PDFItem(filename, url);
            }

            public override nint PreviewItemCount(QLPreviewController controller)
            {
                return 1;
            }
        }

        public void DeleteCookie()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
                CookieStorage.DeleteCookie(cookie);
        }
    }
}