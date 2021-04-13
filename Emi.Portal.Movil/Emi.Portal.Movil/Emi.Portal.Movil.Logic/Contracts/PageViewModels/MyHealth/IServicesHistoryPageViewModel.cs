namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    public interface IServicesHistoryPageViewModel
    {
        Task DownloadPDF();
        Task LoadData();
        ICommand SearchServicesHistoryCommand { get; }
        Task SendPDF();
        ServiceHistoryViewModel ServiceHistorySelected { get; set; }
        string TitlePage { get; set; }
    }
}
