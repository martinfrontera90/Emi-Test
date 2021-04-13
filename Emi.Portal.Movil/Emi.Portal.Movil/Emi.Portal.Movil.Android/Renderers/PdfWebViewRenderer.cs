using Emi.Portal.Movil.Controls;
using Emi.Portal.Movil.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(PdfWebView), typeof(PdfWebViewRenderer))]
namespace Emi.Portal.Movil.Droid.Renderers
{
    using Android.Content;
    using Xamarin.Forms.Platform.Android;

    public class PdfWebViewRenderer : WebViewRenderer
    {
        public PdfWebViewRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as PdfWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                var urlWeb = (UrlWebViewSource)customWebView.Source;
                //Control.LoadUrl("https://drive.google.com/viewerng/viewer?embedded=true&url=" + urlWeb.Url);
            }

        }
    }
}