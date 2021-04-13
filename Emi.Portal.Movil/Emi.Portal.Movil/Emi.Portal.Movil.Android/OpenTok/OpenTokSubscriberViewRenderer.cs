using Xamarin.Forms;
using Android.Content;
using AView = Android.Views.View;
using Android.Runtime;
using Emi.Portal.Movil.Logic.VideoCall;
using Emi.Portal.Movil.Droid.OpenTok;
using Emi.Portal.Movil.Droid.Service;

[assembly: ExportRenderer(typeof(OpenTokSubscriberView), typeof(OpenTokSubscriberViewRenderer))]
namespace Emi.Portal.Movil.Droid.OpenTok
{
    [Preserve(AllMembers = true)]
    public class OpenTokSubscriberViewRenderer : OpenTokViewRenderer
    {
        public OpenTokSubscriberViewRenderer(Context context) : base(context)
        {
        }

        public static void Preserve() { }

        protected override AView GetNativeView() => PlatformOpenTokService.Instance.SubscriberKit?.View;

        protected override void SubscribeResetControl() => PlatformOpenTokService.Instance.SubscriberUpdated += ResetControl;

        protected override void UnsubscribeResetControl()
        {
            PlatformOpenTokService.Instance.ClearSubscribeUpdated();
            PlatformOpenTokService.Instance.EndSession(true, false);
        }
    }
}