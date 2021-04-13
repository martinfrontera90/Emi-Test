using Emi.Portal.Movil.iOS.OpenTok;
using Emi.Portal.Movil.Logic.VideoCall;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Emi.Portal.Movil.OpenTok.iOS.Services;

[assembly: ExportRenderer(typeof(OpenTokPublisherView), typeof(OpenTokPublisherViewRenderer))]
namespace Emi.Portal.Movil.iOS.OpenTok
{
    [Preserve(AllMembers = true)]
    public class OpenTokPublisherViewRenderer : OpenTokViewRenderer
    {
        public static void Preserve() { }

        protected override UIView GetNativeView() => PlatformOpenTokService.Instance.PublisherKit?.View;

        protected override void SubscribeResetControl() => PlatformOpenTokService.Instance.PublisherUpdated += ResetControl;

        protected override void UnsubscribeResetControl() => PlatformOpenTokService.Instance.ClearPublisherUpdated();
    }
}
