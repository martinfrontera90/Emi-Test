namespace Emi.Portal.Movil.Logic
{
    using System.Windows.Input;
    public interface IAddressViewModel
    {
        ICommand SaveAddressDetailCommand { get; }
        ICommand CancelAddressDetailCommand { get; }
    }
}
