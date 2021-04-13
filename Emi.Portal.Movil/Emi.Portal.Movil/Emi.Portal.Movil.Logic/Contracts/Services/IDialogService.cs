namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using System.Threading.Tasks;
    using Acr.UserDialogs;

    public interface IDialogService
    {
        Task<string> Family();        
        Task ShowMessage(string title, string message);
        Task<bool> ShowConfirm(string title, string message, string accept = "Sí", string deny = "No");
        IProgressDialog ShowProgress(string text = "Por favor espera...");
        void HideProgress(string text = "Por favor espera...");
        Task<string> CallCategory();
        Task<string> ServiceHistory();
        Task<string> Service();
        Task<string> Invoices(string State);
        Task<string> ValidateDates();
        Task AlertIcon(string title, string message);
        Task<string> ShowListActionsAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons);
        Task ShowUserInactive(string title, string message, string email, string titleButton, string codeService);
        Task<string> CallContactPhone(string title, string[] contactPhone);
    }
}
