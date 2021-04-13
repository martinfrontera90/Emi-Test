using Emi.Portal.Movil.Controls;
using Emi.Portal.Movil.iOS.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(PdfWebView), typeof(PdfWebViewRenderer))]
namespace Emi.Portal.Movil.iOS.Renderers
{
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    public class PdfWebViewRenderer : WkWebViewRenderer
    {
        UIWebView pdfControl;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (NativeView != null && e.NewElement != null)
            {
                pdfControl = NativeView as UIWebView;

                if (pdfControl == null)
                    return;

                pdfControl.ScalesPageToFit = true;
            }
        }
    }
}
