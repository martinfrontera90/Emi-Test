using Emi.Portal.Movil.iOS.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ViewCell), typeof(ExtendedViewCellRenderer))]
namespace Emi.Portal.Movil.iOS.Renderers
{
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            UITableViewCell cell = base.GetCell(item, reusableCell, tv);
            if (cell != null)
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }
    }
}