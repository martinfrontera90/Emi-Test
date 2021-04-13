namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Windows.Input;
    public interface IChangePasswordPageViewModel
    {
        ICommand ChangePasswordCommand { get; }

        ICommand CancelChangePasswordCommand { get; }
        string TitlePage { get; set; }
    }
}
