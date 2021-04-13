namespace Emi.Portal.Movil.Logic.ViewModels.Pages
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Login;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class EditPasswordPageViewModel : ViewModelBase, IEditPasswordPageViewModel
    {
        IApiService apiService;
        INavigationService navigationService;
        IDialogService dialogService;


        public EditPasswordPageViewModel(IApiService apiService, IDialogService dialogService,
            INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

        public ICommand ChangePasswordCommand { get { return new RelayCommand(async () => await ChangePassword()); } }

        public ICommand ClosedCommand { get { return new RelayCommand(async () => await Closed()); } }

        private async Task Closed()
        {
            await navigationService.ClosedModal();
        }

        public string Email { get; set; }

        public string Code { get; set; }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    RaisePropertyChanged("ConfirmPassword");
                }
            }
        }

        private string errorConfirmPassword;
        public string ErrorConfirmPassword
        {
            get { return errorConfirmPassword; }
            set
            {
                if (errorConfirmPassword != value)
                {
                    errorConfirmPassword = value;
                    RaisePropertyChanged("ErrorConfirmPassword");
                }
            }
        }

        private string errorNewPassword;
        public string ErrorNewPassword
        {
            get { return errorNewPassword; }
            set
            {
                if (errorNewPassword != value)
                {
                    errorNewPassword = value;
                    RaisePropertyChanged("ErrorNewPassword");
                }
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    RaisePropertyChanged("NewPassword");
                }
            }
        }

        private async Task ChangePassword()
        {
            if (ValidateData())
            {
                dialogService.ShowProgress();
                RequestSetPassword request = new RequestSetPassword
                {
                    NewPassword = NewPassword,
                    ConfirmPassword = ConfirmPassword,
                    Email = Email,
                    Code = Code
                };
                ResponseBase response = await apiService.SetPassword(request);
                dialogService.HideProgress();
                await ValidateDataResponseChangePassword(response);
                return;
            }
        }

        private async Task ValidateDataResponseChangePassword(ResponseBase response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);

            if (response.Success && response.StatusCode == 24 || response.StatusCode == 9)
            {
                await navigationService.ClosedModal();
                await navigationService.ClosedModal();
            }
        }

        private bool ValidateData()
        {
            ErrorNewPassword = string.IsNullOrEmpty(NewPassword) ? AppResources.PasswordRequired : string.Empty;
            ErrorConfirmPassword = string.IsNullOrEmpty(ConfirmPassword) ? AppResources.PasswordRequired : string.Empty;

            if (string.IsNullOrEmpty(ErrorNewPassword) && string.IsNullOrEmpty(ErrorConfirmPassword))
            {
                ErrorNewPassword = (NewPassword.Length < 8) ? AppResources.PasswordLength : string.Empty;
                ErrorConfirmPassword = ValidatorHelper.IsEqualData(NewPassword, ConfirmPassword) ? string.Empty : AppResources.DifferentPassword;
            }
            else
            {
                return false;
            }

            return string.IsNullOrEmpty(ErrorNewPassword) && string.IsNullOrEmpty(ErrorConfirmPassword);
        }

        public void CleanData()
        {
            ConfirmPassword = string.Empty;
            ErrorConfirmPassword = string.Empty;
            NewPassword = string.Empty;
            ErrorNewPassword = string.Empty;
        }
    }
}
