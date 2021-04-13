namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IInvoicesPageViewModel
    {
        ICommand SearchInvoicesCommand { get; }
        Task LoadData();
        InvoiceViewModel InvoiceSelected { get; set; }
        Task GoToDetails();
        string TitlePage { get; set; }
    }
}
