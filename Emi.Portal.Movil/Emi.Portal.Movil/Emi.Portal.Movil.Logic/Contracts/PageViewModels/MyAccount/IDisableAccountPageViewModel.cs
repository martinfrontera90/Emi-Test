namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IDisableAccountPageViewModel
    {
        ICommand AcceptCommand { get; }

        ICommand DisableAccountCommand { get; }

        ICommand CancelCommand { get; }

        Task LoadTypes();
        string TitlePage { get; set; }
    }
}
