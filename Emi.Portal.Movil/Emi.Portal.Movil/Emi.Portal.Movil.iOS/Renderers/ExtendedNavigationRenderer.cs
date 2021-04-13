using Emi.Portal.Movil.iOS.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(ExtendedNavigationRenderer))]
namespace Emi.Portal.Movil.iOS.Renderers
{
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    public class ExtendedNavigationRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();          
            NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationBar.ShadowImage = new UIImage();
        }
    }
}