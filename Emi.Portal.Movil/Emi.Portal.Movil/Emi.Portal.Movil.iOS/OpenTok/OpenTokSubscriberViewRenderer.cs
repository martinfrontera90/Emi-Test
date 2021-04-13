using Emi.Portal.Movil.iOS.OpenTok;
using Emi.Portal.Movil.Logic.VideoCall;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Emi.Portal.Movil.OpenTok.iOS.Services;

[assembly: ExportRenderer(typeof(OpenTokSubscriberView), typeof(OpenTokSubscriberViewRenderer))]
namespace Emi.Portal.Movil.iOS.OpenTok
{
    [Preserve(AllMembers = true)]
    public class OpenTokSubscriberViewRenderer : OpenTokViewRenderer
    {
        public static void Preserve() { }

        protected override UIView GetNativeView() => PlatformOpenTokService.Instance.SubscriberKit?.View;

        protected override void SubscribeResetControl() => PlatformOpenTokService.Instance.SubscriberUpdated += ResetControl;

        protected override void UnsubscribeResetControl()
        {
            PlatformOpenTokService.Instance.ClearSubscribeUpdated();
            PlatformOpenTokService.Instance.EndSession(true, false);
        }
    }
}
