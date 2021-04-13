using Android.Content;
using AView = Android.Views.View;
using Android.Runtime;
using Xamarin.Forms;
using Emi.Portal.Movil.Droid.Service;
using Emi.Portal.Movil.Logic.VideoCall;
using Emi.Portal.Movil.Droid.OpenTok;

[assembly: ExportRenderer(typeof(OpenTokPublisherView), typeof(OpenTokPublisherViewRenderer))]
namespace Emi.Portal.Movil.Droid.OpenTok
{
    [Preserve(AllMembers = true)]
    public class OpenTokPublisherViewRenderer : OpenTokViewRenderer
    {
        public OpenTokPublisherViewRenderer(Context context) : base(context)
        {
        }

        public static void Preserve() { }

        protected override AView GetNativeView() => PlatformOpenTokService.Instance.PublisherKit?.View;

        protected override void SubscribeResetControl() => PlatformOpenTokService.Instance.PublisherUpdated += ResetControl;

        protected override void UnsubscribeResetControl() => PlatformOpenTokService.Instance.ClearPublisherUpdated();
    }
}