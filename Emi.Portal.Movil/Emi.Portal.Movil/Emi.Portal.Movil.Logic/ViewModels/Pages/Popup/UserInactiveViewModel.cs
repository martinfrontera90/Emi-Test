namespace Emi.Portal.Movil.Logic.ViewModels.Pages.Popup
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class UserInactiveViewModel : ViewModelBase, IUserInactiveViewModel
    {
        IApiService apiService;
        IDialogService dialogService;

        public UserInactiveViewModel(IApiService apiService, IDialogService dialogService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
        }

        public string UserAccount { get; set; }

        public string CodeService { get; set; }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        private string titleButton;
        public string TitleButton
        {
            get { return titleButton; }
            set
            {
                if (titleButton != value)
                {
                    titleButton = value;
                    RaisePropertyChanged("TitleButton");
                }
            }
        }

        public ICommand SendActiveUserCommand { get { return new RelayCommand(async () => await SendActiveUser()); } }

        private async Task SendActiveUser()
        {
            ResponseBase response = new ResponseBase();

            dialogService.ShowProgress();

            if(CodeService == CodeResponse.UserDisableAccount)
            {
                RequestDeactivateUserAccount request = new RequestDeactivateUserAccount
                {
                    UserAccount = UserAccount,
                    SelectedOption = new Models.Domain.DeactivationSelected
                    {
                        Code = "3",
                        Comment = "loginRequestSendEmailActivateUser"
                    }
                };
                response = await apiService.DisableAccount(request);
            }
            else if(CodeService == CodeResponse.UserInactive)
            {
                RequestSendActivationEmail request = new RequestSendActivationEmail { User = UserAccount };
                response = await apiService.SendActivationEmail(request);
            }
            
            dialogService.HideProgress();
            await ValidateDataResponse(response);
            return;
        }

        private async Task ValidateDataResponse(ResponseBase response)
        {
            if (response.Success && response.StatusCode == 0)
            {
                if(CodeService == CodeResponse.UserDisableAccount)
                {
                    await dialogService.AlertIcon(response.Title, $"Te hemos enviado un correo de reactivación a: {UserAccount}");
                    return;
                }

                await dialogService.AlertIcon(response.Title, response.Message);
                return;
            }

            await dialogService.ShowMessage(response.Title, response.Message);
        }

    }
}
