namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    public interface IRememberPasswordPageViewModel
    {
        ICommand ClosedCommand { get; }
        ICommand RememberPasswordCommand { get; }
        ICommand VerifyCodeCommand { get; }
        Task ValidateUser();
        void CleanData();
        string Email { get; set; }
    }
}
