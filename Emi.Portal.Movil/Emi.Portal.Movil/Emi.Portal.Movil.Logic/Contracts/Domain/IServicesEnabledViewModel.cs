namespace Emi.Portal.Movil.Logic.Contracts.Domain
{
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Enumerations;

    public interface IServicesEnabledViewModel
    {
        ServiceType ServiceType { get; set; }
        ICommand RequestServiceCommand { get; }
    }
}
