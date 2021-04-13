namespace Emi.Portal.Movil.Services
{
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Acr.UserDialogs;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup;
    using AiForms.Dialogs;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Pages.Popup;

    public class DialogService : IDialogService
    {
        IUserDialogs userDialogs;

        public DialogService()
        {
            userDialogs = UserDialogs.Instance;
        }

        public async Task ShowMessage(string title, string message)
        {   if(!string.IsNullOrWhiteSpace(title) || !string.IsNullOrWhiteSpace(message))
                await App.Current.MainPage.DisplayAlert(title, message, "Aceptar");
        }

        public async Task<bool> ShowConfirm(string title, string message, string accept = "Sí", string deny = "No")
        {
            return await App.Current.MainPage.DisplayAlert(title, message, accept, deny);
        }

        public IProgressDialog ShowProgress(string text = "Por favor espera...")
        {
            return userDialogs.Loading(text, null, null, true, MaskType.Black);
        }

        public void HideProgress(string text = "Por favor espera...")
        {
            userDialogs.Loading(text, null, null, true, MaskType.Black).Hide();
        }

        public async Task<string> Family()
        {
            return await Application.Current.MainPage.DisplayActionSheet(AppResources.TitleQuestion, AppResources.CancelText, null, AppResources.EditText, AppResources.DeleteText);
        }

        public async Task<string> CallContactPhone(string title, string[] contactPhone)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, AppResources.CancelText, null, contactPhone);
        }

        public async Task<string> CallCategory()
        {
            return await Application.Current.MainPage.DisplayActionSheet(AppResources.TitleTelephoneLinesAvailable, AppResources.CancelText, null, AppResources.TitleMedicalCare, AppResources.TitleCustomerServices);
        }

        public async Task<string> ServiceHistory()
        {
            return await Application.Current.MainPage.DisplayActionSheet(AppResources.TitleQuestion, AppResources.CancelText, null, "Descargar PDF", "Enviar PDF");
        }

        public async Task<string> Service()
        {
            return await Application.Current.MainPage.DisplayActionSheet(AppResources.TitleQuestion, AppResources.CancelText, null, "Agregar dirección", "Ver cobertura");
        }

        public async Task<string> Invoices(string State)
        {
            if (State == PaymentState.Impaga.ToString())
            {
                return await Application.Current.MainPage.DisplayActionSheet(AppResources.TitleQuestion, AppResources.CancelText, null, "Ver detalle", "Pagar");
            }
            else
            {
                return await Application.Current.MainPage.DisplayActionSheet(AppResources.TitleQuestion, AppResources.CancelText, null, "Ver detalle");
            }
            
        }

        public async Task<string> ValidateDates()
        {
            return await Application.Current.MainPage.DisplayActionSheet(AppResources.TitleValidateDates, AppResources.CancelText, null, AppResources.MessageValidateDates);
        }

        public async Task AlertIcon(string title, string message)
        {
            await Dialog.Instance.ShowAsync<AlertPage>(new { Title = title, Message = message });
        }

        public async Task<string> ShowListActionsAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons)
        {
            return await userDialogs.ActionSheetAsync(title, cancelButton, destroyButton, null, otherButtons);
        }

        public async Task ShowUserInactive(string title, string message, string email, string titleButton, string codeService)
        {
            IUserInactiveViewModel userInactive = ServiceLocator.Current.GetInstance<IUserInactiveViewModel>();
            userInactive.UserAccount = email;
            userInactive.Title = title;
            userInactive.Message = message;
            userInactive.TitleButton = titleButton;
            userInactive.CodeService = codeService;
            await Dialog.Instance.ShowAsync<UserInactivePage>(userInactive);
        }

    }
}
