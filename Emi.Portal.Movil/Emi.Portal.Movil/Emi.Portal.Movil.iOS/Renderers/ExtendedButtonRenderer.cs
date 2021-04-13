using Emi.Portal.Movil.Controls;
using Emi.Portal.Movil.iOS.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]
namespace Emi.Portal.Movil.iOS.Renderers
{
    using System.ComponentModel;
    using Emi.Portal.Movil.Controls;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    public class ExtendedButtonRenderer : ButtonRenderer
    {       
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var element = Element;

            if (element == null || Control == null)
            {
                return;
            }

            Control.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "VerticalContentAlignment":
                    Control.VerticalAlignment = UIControlContentVerticalAlignment.Center;
                    break;
                case "HorizontalContentAlignment":
                    Control.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                    break;
                default:
                    base.OnElementPropertyChanged(sender, e);
                    break;
            }
        }

        public new ExtendedButton Element
        {
            get
            {
                return base.Element as ExtendedButton;
            }
        }

        public ExtendedButtonRenderer()
        {

        }
    }
}