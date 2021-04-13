namespace Emi.Portal.Movil.Logic.ViewModels.Pages
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Login;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class RememberPasswordPageViewModel : ViewModelBase, IRememberPasswordPageViewModel
    {
        #region Properties
        IApiService apiService;
        INavigationService navigationService;
        IDialogService dialogService;

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    RaisePropertyChanged(() => Email);

                    ErrorEmail = string.Empty;
                    ValidateEmail();
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

        private bool otherEmail;
        public bool OtherEmail
        {
            get { return otherEmail; }
            set
            {
                if (otherEmail != value)
                {
                    otherEmail = value;
                    RaisePropertyChanged("OtherEmail");
                }
            }
        }

        private ObservableCollection<AsociatedUserAccounts> asociatedUserAccounts;
        public ObservableCollection<AsociatedUserAccounts> AsociatedUserAccounts
        {
            get { return asociatedUserAccounts; }
            set
            {
                if (asociatedUserAccounts != value)
                {
                    asociatedUserAccounts = value;
                    RaisePropertyChanged("AsociatedUserAccounts");
                }
            }
        }

        private AsociatedUserAccounts asociatedUserSelected;
        public AsociatedUserAccounts AsociatedUserSelected
        {
            get { return asociatedUserSelected; }
            set
            {
                if (asociatedUserSelected != value)
                {
                    asociatedUserSelected = value;
                    RaisePropertyChanged("AsociatedUserSelected");
                    if (value != null)
                    {
                        Email = value.CryptedEmail;
                    }
                }
            }
        }

        private bool isVerifyCode;
        public bool IsVerifyCode
        {
            get { return isVerifyCode; }
            set
            {
                if (isVerifyCode != value)
                {
                    isVerifyCode = value;
                    RaisePropertyChanged("IsVerifyCode");
                }
            }
        }

        private string verifyCode;
        public string VerifyCode
        {
            get { return verifyCode; }
            set
            {
                if (verifyCode != value)
                {
                    verifyCode = value;
                    RaisePropertyChanged("VerifyCode");
                }
            }
        }

        private string errorVerifyCode;
        public string ErrorVerifyCode
        {
            get { return errorVerifyCode; }
            set
            {
                if (errorVerifyCode != value)
                {
                    errorVerifyCode = value;
                    RaisePropertyChanged("ErrorVerifyCode");
                }
            }
        }

        #endregion

        #region Commmands
        public ICommand RememberPasswordCommand { get { return new RelayCommand(async () => await ValidateUser()); } }

        public ICommand ClosedCommand { get { return new RelayCommand(async () => await Closed()); } }

        public ICommand VerifyCodeCommand { get { return new RelayCommand(async () => await SendVerifyCode()); } }

        private async Task Closed()
        {
            await navigationService.ClosedModal();
        }
        #endregion

        #region Methods

        public async Task ValidateUser()
        {
            ValidateEmail();
            if (string.IsNullOrEmpty(ErrorEmail))
            {
                dialogService.ShowProgress();

                bool isNumeric = double.TryParse(Email, out double phoneNumber);
                if (isNumeric)
                {
                    RequestAsociatedUserAccounts request = new RequestAsociatedUserAccounts { Cellphone = Email };
                    ResponseAsociatedUserAccounts response = await apiService.GetAsociatedUserAccounts(request);
                    dialogService.HideProgress();

                    if (response.Success)
                    {
                        if (response.StatusCode == 0)
                        {
                            OtherEmail = response.AsociatedUserAccounts.Count > 1;
                            if (OtherEmail)
                            {
                                AsociatedUserAccounts = new ObservableCollection<AsociatedUserAccounts>(response.AsociatedUserAccounts);
                                return;
                            }
                        }
                        else
                        {
                            await dialogService.ShowMessage(response.Title, response.Message);
                            return;
                        }
                    }
                }

                await RememberPasword();
            }
        }


        public async Task RememberPasword()
        {
            if (string.IsNullOrEmpty(ErrorEmail))
            {
                dialogService.ShowProgress();
                RequestForgotPassword request = new RequestForgotPassword { User = Email };
                ResponseForgotPassword response = await apiService.ForgotPassword(request);
                dialogService.HideProgress();

                if (response.Success)
                {
                    if (response.StatusCode == 0)
                    {
                        bool isNumeric = double.TryParse(Email, out double phoneNumber);
                        if (isNumeric)
                        {
                            IsVerifyCode = true;
                            return;
                        }

                        await dialogService.ShowMessage(response.Title, response.Message);
                        await navigationService.ClosedModal();
                    }
                    else
                    {
                        if (response.StatusCode == int.Parse(CodeResponse.UserInactive))
                        {
                            await dialogService.ShowUserInactive(response.Title, response.Message, Email, "Enviar Correo", CodeResponse.UserInactive);
                            await navigationService.ClosedModal();
                        }
                        await dialogService.ShowMessage(response.Title, response.Message);                        
                    }
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
            }
        }

        private void ValidateEmail()
        {
            if (OtherEmail)
            {
                ErrorEmail = AsociatedUserSelected == null ? "Seleccione correo electrónico válido." : string.Empty;
            }

            if (string.IsNullOrEmpty(Email))
            {
                ErrorEmail = AppResources.EmailOrPhoneRequired;
                return;
            }
            bool isNumeric = double.TryParse(Email, out double phoneNumber);
            if (isNumeric)
            {
                ErrorEmail = (Email.Length == 1 || Email.Length != 9) || Email.Substring(0, 2) != "09"  ? AppResources.PhoneValidate : string.Empty;
            }
            else
            {
                ErrorEmail = ValidatorHelper.IsValidEmail(Email) ? string.Empty : AppResources.WriteValidEmail;
            }
        }

        public async Task SendVerifyCode()
        {
            ValidateVerifyCode();
            if (string.IsNullOrEmpty(ErrorVerifyCode))
            {
                dialogService.ShowProgress();
                RequestVerifyCodeForgortPassword request = new RequestVerifyCodeForgortPassword { Code = VerifyCode, User = Email };
                ResponseBase response = await apiService.VerifyCodeForgortPassword(request);
                dialogService.HideProgress();

                if (response.Success)
                {
                    if (response.StatusCode == 0)
                    {
                        dialogService.ShowProgress();
                        RequestAuthorizeChangePassword requestAuthorize = new RequestAuthorizeChangePassword { CellPhone = Email };
                        ResponseAuthorizeChangePassword responseAuthorize = await apiService.AuthorizeChangePasswordByCellPhone(requestAuthorize);
                        dialogService.HideProgress();

                        if (responseAuthorize.Success)
                        {
                            if (responseAuthorize.StatusCode == 0)
                            {
                                IEditPasswordPageViewModel editPassword = ServiceLocator.Current.GetInstance<IEditPasswordPageViewModel>();
                                editPassword.Email = responseAuthorize.AuthorizeChangePassword.UserName;
                                editPassword.Code = responseAuthorize.AuthorizeChangePassword.Code;
                                editPassword.CleanData();

                                await navigationService.ShowModal(Enumerations.AppPages.EditPasswordPage);
                                return;
                            }

                            await dialogService.ShowMessage(responseAuthorize.Title, responseAuthorize.Message);
                            return;
                        }
                    }
                }

                await dialogService.ShowMessage(response.Title, response.Message);
                return;
            }
        }

        private void ValidateVerifyCode()
        {
            ErrorVerifyCode = string.IsNullOrEmpty(VerifyCode) ? AppResources.VerifyCodeRequired : string.Empty;
        }

        public void CleanData()
        {
            Email = string.Empty;
            ErrorEmail = string.Empty;
            OtherEmail = false;
            AsociatedUserAccounts = new ObservableCollection<AsociatedUserAccounts>();
            AsociatedUserSelected = null;
            IsVerifyCode = false;
            VerifyCode = string.Empty;
            ErrorVerifyCode = string.Empty;
        }

        #endregion

        #region Constructor
        public RememberPasswordPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            OtherEmail = false;
        }
        #endregion
    }
}
