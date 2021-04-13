namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Windows.Input;

    public interface IChangeEmailPageViewModel
    {
        ICommand ChangeEmailCommand { get; }

        ICommand CancelChangeEmailCommand { get; }
        string TitlePage { get; set; }
    }
}
