namespace Emi.Portal.Movil.Logic.Contracts.Domain
{
    using System.Windows.Input;
    public interface IPersonViewModel
    {
        ICommand InformationCommand { get; }
        ICommand OptionsCommand { get; }
        ICommand UpdateMemberCommand { get; }
    }
}
