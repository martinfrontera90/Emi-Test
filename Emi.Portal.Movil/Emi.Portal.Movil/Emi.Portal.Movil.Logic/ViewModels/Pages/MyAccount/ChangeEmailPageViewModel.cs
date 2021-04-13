namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class ChangeEmailPageViewModel : ViewModelBase, IChangeEmailPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;

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

        private string confirmEmail;
        public string ConfirmEmail
        {
            get { return confirmEmail; }
            set
            {
                if (confirmEmail != value)
                {
                    confirmEmail = value;
                    RaisePropertyChanged("ConfirmEmail");
                }
            }
        }

        private string errorConfirmEmail;
        public string ErrorConfirmEmail
        {
            get { return errorConfirmEmail; }
            set
            {
                if (errorConfirmEmail != value)
                {
                    errorConfirmEmail = value;
                    RaisePropertyChanged("ErrorConfirmEmail");
                }
            }
        }

        private string errorNewEmail;
        public string ErrorNewEmail
        {
            get { return errorNewEmail; }
            set
            {
                if (errorNewEmail != value)
                {
                    errorNewEmail = value;
                    RaisePropertyChanged("ErrorNewEmail");
                }
            }
        }

        private string errorOldEmail;
        public string ErrorOldEmail
        {
            get { return errorOldEmail; }
            set
            {
                if (errorOldEmail != value)
                {
                    errorOldEmail = value;
                    RaisePropertyChanged("ErrorOldEmail");
                }
            }
        }

        private string newEmail;
        public string NewEmail
        {
            get { return newEmail; }
            set
            {
                if (newEmail != value)
                {
                    newEmail = value;
                    RaisePropertyChanged("NewEmail");
                }
            }
        }

        private string oldEmail;
        public string OldEmail
        {
            get { return oldEmail; }
            set
            {
                if (oldEmail != value)
                {
                    oldEmail = value;
                    RaisePropertyChanged("OldEmail");
                }
            }
        }
        #endregion

        #region Commands
        public ICommand ChangeEmailCommand { get { return new RelayCommand(async () => await ChangeEmail()); } }

        public ICommand CancelChangeEmailCommand { get { return new RelayCommand(async() => await CancelChangeEmail()); } }

        #endregion

        private async Task ChangeEmail()
        {
            if (ValidateData())
            {
                if (await dialogService.ShowConfirm(AppResources.QuestionConfirmChangeEmail, AppResources.ConfirmChangeEmail))
                {
                    dialogService.ShowProgress();
                    RequestEmail request = new RequestEmail
                    {
                        OldEmail = OldEmail,
                        NewEmail = NewEmail,
                        ConfirmEmail = ConfirmEmail
                    };
                    ResponseBase response = await apiService.ChangeEmail(request);
                    dialogService.HideProgress();
                    await ValidateDataResponseChangeEmail(response);
                }
                else
                {
                    OldEmail = string.Empty;
                    NewEmail = string.Empty;
                    ConfirmEmail = string.Empty;
                }
                return;
            }
        }

        private async Task ValidateDataResponseChangeEmail(ResponseBase response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);

            if (response.Success && response.StatusCode == 87)
            {
                INotificationService notificationsService = ServiceLocator.Current.GetInstance<INotificationService>();
                ILoginPageViewModel loginPageViewModel = ServiceLocator.Current.GetInstance<ILoginPageViewModel>();
                ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
                IFileService fileService = ServiceLocator.Current.GetInstance<IFileService>();

                loginViewModel.User = null;
                loginPageViewModel.Email = string.Empty;
                loginPageViewModel.Password = string.Empty;
                notificationsService.UnregisterNotifications();

                await fileService.SaveAsync(string.Format("{0} User", AppConfigurations.Brand), loginViewModel.User);
                await navigationService.Navigate(AppPages.LoginPage);
            }
        }

        private bool ValidateData()
        {
            ErrorOldEmail = string.IsNullOrEmpty(OldEmail) ? AppResources.MailRequired : string.Empty;
            ErrorNewEmail = string.IsNullOrEmpty(NewEmail) ? AppResources.MailRequired : string.Empty;
            ErrorConfirmEmail = string.IsNullOrEmpty(ConfirmEmail) ? AppResources.MailRequired : string.Empty;

            if (string.IsNullOrEmpty(ErrorOldEmail) &&
                string.IsNullOrEmpty(ErrorNewEmail) &&
                string.IsNullOrEmpty(ErrorConfirmEmail))
            {
                ErrorOldEmail = ValidatorHelper.IsValidEmail(OldEmail) ? string.Empty : AppResources.WriteValidEmail;
                ErrorNewEmail = ValidatorHelper.IsValidEmail(NewEmail) ? string.Empty : AppResources.WriteValidEmail;
                ErrorConfirmEmail = ValidatorHelper.IsValidEmail(ConfirmEmail) ? string.Empty : AppResources.WriteValidEmail;

                if (string.IsNullOrEmpty(ErrorOldEmail))
                {
                    ErrorOldEmail = OldEmail.Equals(ServiceLocator.Current.GetInstance<ILoginViewModel>().User.UserName) ? string.Empty : AppResources.EmailCurrentError;
                }

                if(string.IsNullOrEmpty(ErrorNewEmail) && string.IsNullOrEmpty(ErrorConfirmEmail))
                {
                    ErrorConfirmEmail = ConfirmEmail.Equals(NewEmail) ? string.Empty : AppResources.EmailEqualError;
                }
            }
            else
            {
                return false;
            }

            return string.IsNullOrEmpty(ErrorOldEmail) &&
                string.IsNullOrEmpty(ErrorNewEmail) &&
                string.IsNullOrEmpty(ErrorConfirmEmail);
        }

        public ChangeEmailPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

        public async Task CancelChangeEmail()
        {
            await navigationService.Navigate(AppPages.LandingPage);
        }

    }
}
