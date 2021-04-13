namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class ChangePasswordPageViewModel : ViewModelBase, IChangePasswordPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IPhoneService phoneService;

        private string titlePage;
        public string TitlePage
        {
            get { return titlePage; }
            set
            {
                titlePage = value;
                RaisePropertyChanged("TitlePage");
            }
        }

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

        private string errorOldPassword;
        public string ErrorOldPassword
        {
            get { return errorOldPassword; }
            set
            {
                if (errorOldPassword != value)
                {
                    errorOldPassword = value;
                    RaisePropertyChanged("ErrorOldPassword");
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

        private string oldPassword;
        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                if (oldPassword != value)
                {
                    oldPassword = value;
                    RaisePropertyChanged("OldPassword");
                }
            }
        }
        #endregion

        #region Commands
        public ICommand ChangePasswordCommand { get { return new RelayCommand(ChangePassword); } }

        public ICommand CancelChangePasswordCommand { get { return new RelayCommand(CancelChangePassword); } }
        #endregion

        private async void ChangePassword()
        {
            if (ValidateData())
            {
                if (await dialogService.ShowConfirm("Estás a punto de cambiar tu contraseña", "¿Estás seguro que la deseas cambiar?"))
                {
                    dialogService.ShowProgress();
                    RequestChangePassword request = new RequestChangePassword
                    {
                        NewPassword = NewPassword,
                        OldPassword = OldPassword,
                        UserName = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.UserName
                    };
                    ResponseChangePassword response = await apiService.ChangePassword(request);
                    dialogService.HideProgress();
                    ValidateDataResponseChangePassword(response);                     
                }
                return;
            }

            await dialogService.ShowMessage(AppResources.TittleInvalidData, "Hay registros con errores.");
        }

        private async void ValidateDataResponseChangePassword(ResponseChangePassword response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);

            if (response.Success && response.StatusCode == 0)
            {
                await navigationService.Navigate(AppPages.LoginPage);
            }
        }

        private bool ValidateData()
        {
            ErrorOldPassword = string.IsNullOrEmpty(OldPassword) ? AppResources.PasswordRequired : string.Empty;
            ErrorNewPassword = string.IsNullOrEmpty(NewPassword) ? AppResources.PasswordRequired : string.Empty;
            ErrorConfirmPassword = string.IsNullOrEmpty(ConfirmPassword) ? AppResources.PasswordRequired : string.Empty;

            if (string.IsNullOrEmpty(ErrorOldPassword) && string.IsNullOrEmpty(ErrorNewPassword) && string.IsNullOrEmpty(ErrorConfirmPassword))
            {
                ErrorNewPassword = (NewPassword.Length < 8) ? AppResources.PasswordLength : string.Empty;
                ErrorConfirmPassword = ValidatorHelper.IsEqualData(NewPassword, ConfirmPassword) ? string.Empty : AppResources.DifferentPassword;
            }
            else
            {
                return false;
            }

            return string.IsNullOrEmpty(ErrorOldPassword) && string.IsNullOrEmpty(ErrorNewPassword) && string.IsNullOrEmpty(ErrorConfirmPassword);
        }

        public ChangePasswordPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService, IPhoneService phoneService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.phoneService = phoneService;

        }

        public async void CancelChangePassword()
        {
            await navigationService.Navigate(AppPages.LandingPage);
        }
    }
}
