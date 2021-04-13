namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.LegalContent
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public interface ILegalContentPageViewModel
    {
        ICommand CancelCallCommand { get; }
        string Icon { get; set; }
        HtmlWebViewSource HtmlSource { get; set; }
        Task LoadContentLegal(string Tag);
        bool FromRegister { get; set; }
    }
}
