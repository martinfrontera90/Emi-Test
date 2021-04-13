namespace Emi.Portal.Movil.Logic.Contracts.Domain
{
    using System.Windows.Input;

    public interface ICoordinationViewModel
    {        
        ICommand CallMedicalCenterCommand { get; }
        ICommand CallCategoryCommand { get; }
        ICommand CancelCoordinationCommand { get; }
        ICommand ConfirmationCommand { get; }
        void DeleteCoordination();
        ICommand SelectCommand { get; }
    }
}
