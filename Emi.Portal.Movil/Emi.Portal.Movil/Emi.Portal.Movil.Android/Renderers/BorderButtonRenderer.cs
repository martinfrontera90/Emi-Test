using Emi.Portal.Movil.Droid.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Button), typeof(BorderButtonRenderer))]
namespace Emi.Portal.Movil.Droid.Renderers
{
    using System.ComponentModel;
    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// Custom renderer for buttons with a custom border
    /// </summary>
    public class BorderButtonRenderer : ButtonRenderer
    {
        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}