namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Windows.Input;
    public interface IBeneficiariesPageViewModel
    {
        ICommand RefreshBeneficiariesCommand { get; }
        string TitlePage { get; set; }
    }
}
