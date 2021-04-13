using Emi.Portal.Movil.Controls;
using Emi.Portal.Movil.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]
namespace Emi.Portal.Movil.Droid.Renderers
{
    using Emi.Portal.Movil.Controls;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class ExtendedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            UpdateAlignment();
        }        

        private void UpdateAlignment()
        {
            ExtendedButton element = Element as ExtendedButton;
            if (element == null || Control == null)
            {
                return;
            }                        
            Control.Gravity = Android.Views.GravityFlags.Center | Android.Views.GravityFlags.Start;
        }
    }
}