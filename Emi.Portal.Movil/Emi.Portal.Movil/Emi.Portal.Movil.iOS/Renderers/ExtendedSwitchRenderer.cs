using Emi.Portal.Movil.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Switch), typeof(ExtendedSwitchRenderer))]
namespace Emi.Portal.Movil.iOS.Renderers
{
    public class ExtendedSwitchRenderer : SwitchRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);
            Switch element = (Switch)Element;
            if (element != null)
            {
                Control.OnTintColor = UIKit.UIColor.FromRGB(52, 195, 234);
            }
        }
    }
}
