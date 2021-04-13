namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public interface IChatCustomerServicePageViewModel
    {
        ICommand ExitChatCommand { get; }
        UrlWebViewSource HtmlSource { get; set; }
        Task LoadChatPage();
    }
}
