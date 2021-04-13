namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    public interface IMedicalCenterCoordinationPageViewModel : ICallMedicalCenterViewModel
    {
        List<CoordinationViewModel> AllCoordinations { get; set; }
        CoordinationViewModel CoordinationSelected { get; set; }
        Task LoadCoordinations();        
        ICommand NewCoordinationCommand { get; }
        ICommand RefreshPendingCoordinationsCommand { get; }
        bool IsVisiblePay { get; set; }
        bool IsReady { get; set; }
        bool IsVisibleCoordination { get; set; }
        string TitlePage { get; set; }
    }
}
