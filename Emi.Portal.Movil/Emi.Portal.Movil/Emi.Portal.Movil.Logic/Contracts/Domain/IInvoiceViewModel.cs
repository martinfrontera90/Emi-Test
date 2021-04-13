namespace Emi.Portal.Movil.Logic.Contracts.Domain
{
    using System.Windows.Input;

    public interface IInvoiceViewModel
    {
        ICommand OptionsCommand { get; }
    }
}
