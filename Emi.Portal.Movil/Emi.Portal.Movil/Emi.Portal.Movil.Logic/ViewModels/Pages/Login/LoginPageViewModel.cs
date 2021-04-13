namespace Emi.Portal.Movil.Logic.ViewModels.Pages
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Home;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class LoginPageViewModel : ViewModelBase, ILoginPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        IExceptionService exceptionService;
        IFileService fileService;
        INavigationService navigationService;
        INotificationService notificationsService;
		IPhoneService phoneService;

        private string affiliateType;
        public string AffiliateType
        {
            get { return affiliateType; }
            set
            {
                if (affiliateType != value)
                {
                    affiliateType = value;
                    RaisePropertyChanged("AffiliateType");
                }
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                RaisePropertyChanged(nameof(NewPassword));
            }
        }

        private string errorNewPassword;
        public string ErrorNewPassword
        {
            get { return errorNewPassword; }
            set
            {
                errorNewPassword = value;
                RaisePropertyChanged(nameof(ErrorNewPassword));
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }

        private string errorConfirmPassword;
        public string ErrorConfirmPassword
        {
            get { return errorConfirmPassword; }
            set
            {
                errorConfirmPassword = value;
                RaisePropertyChanged(nameof(ErrorConfirmPassword));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }

        private string errorEmail;
        public string ErrorEmail
        {
            get { return errorEmail; }
            set
            {
                if (errorEmail != value)

                {
                    errorEmail = value;
                    RaisePropertyChanged("ErrorEmail");
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }

        private string errorPassword;
        public string ErrorPassword
        {
            get { return errorPassword; }
            set
            {
                if (errorPassword != value)
                {
                    errorPassword = value;
                    RaisePropertyChanged("ErrorPassword");
                }
            }
        }

        public string IdRegister { get; set; }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(async () => await Login()); } }
        public ICommand RegisterCommand { get { return new RelayCommand(async () => await Register()); } }
        public ICommand ChangePasswordCommand { get { return new RelayCommand(async () => await ChangePassword()); } }
        public ICommand GoToRememberPasswordCommand { get { return new RelayCommand(async () => await GoToRememberPassword()); } }
        public ICommand CancelCommand { get { return new RelayCommand(CancelChangePassword); } }
        #endregion

        #region Constructor
        public LoginPageViewModel(INavigationService navigationService, IApiService apiService, IDialogService dialogService, INotificationService notificationsService, IFileService fileService, IExceptionService exceptionService, IPhoneService phoneService)
        {
            this.navigationService = navigationService;
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.notificationsService = notificationsService;
            this.fileService = fileService;
            this.exceptionService = exceptionService;
			this.phoneService = phoneService;
#if DEBUG
            ///Email = "alejitap42@gmail.com";
            ///Password = "aparada9.";
#endif
        }
        #endregion

        #region Methods
        private async void CancelChangePassword()
        {
            Password = string.Empty;
            Email = string.Empty;
            await navigationService.BackModal();
        }
        private async Task Login()
        {
            ValidateEmail();
            ValidatePassword();
            if (string.IsNullOrEmpty(ErrorEmail) && string.IsNullOrEmpty(ErrorPassword))
            {
                dialogService.ShowProgress();
                Login login = new Login();
                ViewModelHelper.SetLoginPageViemModelToLogin(this, login);
                RequestLogin request = new RequestLogin { Login = login };
                ResponseLogin responseLogin = await apiService.Login(request);
                if (responseLogin.UserMidd?.Equals("True") == true && responseLogin.ChangedPassword?.Equals("False") == true)
                {
                    FirstLogin();
                    //await navigationService.Navigate(AppPages.LoginPage);
                }
                else
                {
                    dialogService.HideProgress();
                    await ValidateResponde(responseLogin);
                }
            }
        }

        private async void FirstLogin()
        {
            dialogService.HideProgress();
            await navigationService.ShowModal(AppPages.FirstChangePasswordPage);
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

        private async Task ChangePassword()
        {
            try
            {
                if (ValidateData())
                {
                    RequestChangePassword request = new RequestChangePassword
                    {
                        NewPassword = NewPassword,
                        OldPassword = Password,
                        UserName = Email
                    };
                    ResponseChangePassword response = await apiService.ChangePassword(request);
                    if (response.Success && response.StatusCode == 0)
                    {
                        Password = string.Empty;
                        Email = string.Empty;
                        await dialogService.ShowMessage("", "Contraseña cambiada con éxito");
                        await navigationService.Navigate(AppPages.LoginPage);
                    }
                }

            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private async Task Register()
        {
            await navigationService.Navigate(AppPages.RegisterDocumentPage);
        }

        private async Task GoToRememberPassword()
        {
            IRememberPasswordPageViewModel rememberPassword = ServiceLocator.Current.GetInstance<IRememberPasswordPageViewModel>();
            rememberPassword.CleanData();
            rememberPassword.Email = Email;
            await navigationService.ShowModal(AppPages.RememberPasswordPage);
        }

        private async Task ValidateResponde(ResponseLogin responseLogin)
        {
            ErrorEmail = string.Empty;
            ErrorPassword = string.Empty;

            if (responseLogin.Success == false || (responseLogin.StatusCode != null && responseLogin.StatusCode != "0"))
            {
                if (responseLogin.StatusCode == CodeResponse.UserDisableAccount || responseLogin.StatusCode == CodeResponse.UserInactive)
                {
                    await dialogService.ShowUserInactive(responseLogin.Tittle, responseLogin.Message, Email,
                        responseLogin.StatusCode == CodeResponse.UserDisableAccount ? "Reactivar Usuario" : "Enviar Correo", responseLogin.StatusCode);
                    return;
                }


                await dialogService.ShowMessage(responseLogin.Tittle, responseLogin.Message);
                return;
            }

            if (responseLogin.Success)
            {
                ViewModelHelper.SetResponseLoginToLoginViewModel(this, responseLogin);
                ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
                ILandingPageViewModel landingPageViewModel = ServiceLocator.Current.GetInstance<ILandingPageViewModel>();                
                landingPageViewModel.UserName = loginViewModel.User.NameOne;
                apiService.Token = loginViewModel.User.Access_token;
				try
                {
                    await fileService.SaveAsync(string.Format("{0} User", AppConfigurations.Brand), loginViewModel.User);
                }
                catch (Exception ex)
                {
                    exceptionService.RegisterException(ex);
                }

                ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
                await callViewModel.Init();

                notificationsService.RegisterNotifications();

				RequestSoftwareVersion request = new RequestSoftwareVersion();
                ResponseSoftwareVersion response = await apiService.GetSoftwareVersion(request);
                IMenuPageViewModel menuPageViewModel = ServiceLocator.Current.GetInstance<IMenuPageViewModel>();

                if (response.Success && response.StatusCode == 0)
                    menuPageViewModel.Version = response.Value;

                menuPageViewModel.LoadMenu();
                await navigationService.Navigate(AppPages.MenuPage);
                IContingencyMessagePageViewModel contingencyMessage = ServiceLocator.Current.GetInstance<IContingencyMessagePageViewModel>();
                contingencyMessage.LoadData();
            }
            //else
            //{
            //    if (responseLogin.StatusCode == "401")
            //    {
            //        await dialogService.ShowUserInactive(responseLogin.Tittle, responseLogin.Message, Email);
            //        return;
            //    }

            //    if (responseLogin.StatusCode == "18")
            //    {
            //        if (await dialogService.ShowConfirm("Usuario inactivo", string.Format("{0} {1}", responseLogin.Message, AppResources.SendActivationEmail)))
            //        {
            //            dialogService.ShowProgress();
            //            RequestSendActivationEmail request = new RequestSendActivationEmail() { User = Email };
            //            ResponseSendActivationEmail response = await apiService.SendActivationEmail(request);
            //            dialogService.HideProgress();
            //            await dialogService.ShowMessage(response.Title, response.Message);
            //        }
            //        return;
            //    }
            //    await dialogService.ShowMessage(responseLogin.Tittle, responseLogin.Message);
            //}

        }

        private void ValidateEmail()
        {
            if (string.IsNullOrEmpty(Email))
            {
                ErrorEmail = AppResources.MailRequired;
                return;
            }
            ErrorEmail = ValidatorHelper.IsValidEmail(Email) ? string.Empty : AppResources.WriteValidEmail;
        }

        private void ValidatePassword()
        {
            ErrorPassword = string.IsNullOrEmpty(Password) ? AppResources.PasswordRequired : string.Empty;
        }
        #endregion
    }
}
