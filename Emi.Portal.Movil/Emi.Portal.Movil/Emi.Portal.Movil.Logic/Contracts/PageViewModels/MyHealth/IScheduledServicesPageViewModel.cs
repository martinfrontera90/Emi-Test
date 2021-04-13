namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IScheduledServicesPageViewModel
    {        
        void LoadScheduledServices();
        ICommand RefreshScheduledServicesCommand { get; }
        string TitlePage { get; set; }
    }
}
