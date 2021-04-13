namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    using System.Windows.Input;
    public interface ILoginPageViewModel
    {
        string Email { get; set; }
        ICommand GoToRememberPasswordCommand { get; }
        ICommand LoginCommand { get; }
        string Password { get; set; }
        ICommand RegisterCommand { get; }
        
    }
}
