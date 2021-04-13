namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup
{
    using System.Windows.Input;

    public interface IUserInactiveViewModel
    {
        ICommand SendActiveUserCommand { get; }

        string UserAccount { get; set; }

        string Title { get; set; }

        string Message { get; set; }

        string CodeService { get; set; }

        string TitleButton { get; set; }
    }
}
