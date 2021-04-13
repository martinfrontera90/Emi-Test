namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class DisableAccountPageViewModel : ViewModelBase, IDisableAccountPageViewModel
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

        private bool acceptDisableAccount;
        public bool AcceptDisableAccount
        {
            get { return acceptDisableAccount; }
            set
            {
                if (acceptDisableAccount != value)
                {
                    acceptDisableAccount = value;
                    RaisePropertyChanged("AcceptDisableAccount");
                }
            }
        }

        private ObservableCollection<DeactivationType> deactivationType;
        public ObservableCollection<DeactivationType> DeactivationType
        {
            get { return deactivationType; }
            set
            {
                if (deactivationType != value)
                {
                    deactivationType = value;
                    RaisePropertyChanged("DeactivationType");
                }
            }
        }

        private DeactivationType deactivationTypeSelected;
        public DeactivationType DeactivationTypeSelected
        {
            get { return deactivationTypeSelected; }
            set
            {
                if (deactivationTypeSelected != value)
                {
                    deactivationTypeSelected = value;
                    RaisePropertyChanged("DeactivationTypeSelected");
                    IsDeactivationOther = value?.Name.Trim() == "Otros".Trim();
                    Other = string.Empty;
                    ErrorOther = string.Empty;
                }
            }
        }

        private bool isDeactivationOther;
        public bool IsDeactivationOther
        {
            get { return isDeactivationOther; }
            set
            {
                if (isDeactivationOther != value)
                {
                    isDeactivationOther = value;
                    RaisePropertyChanged("IsDeactivationOther");
                }
            }
        }

        private string errorDeactivationType;
        public string ErrorDeactivationType
        {
            get { return errorDeactivationType; }
            set
            {
                if (errorDeactivationType != value)
                {
                    errorDeactivationType = value;
                    RaisePropertyChanged("ErrorDeactivationType");
                }
            }
        }

        private string other;
        public string Other
        {
            get { return other; }
            set
            {
                if (other != value)
                {
                    other = value;
                    RaisePropertyChanged("Other");
                }
            }
        }


        private string errorOther;
        public string ErrorOther
        {
            get { return errorOther; }
            set
            {
                if (errorOther != value)
                {
                    errorOther = value;
                    RaisePropertyChanged("ErrorOther");
                }
            }
        }

        #endregion

        public ICommand AcceptCommand { get { return new RelayCommand(Accept); } }

        public ICommand DisableAccountCommand { get { return new RelayCommand(async () => await DisableAccount()); } }

        public ICommand CancelCommand { get { return new RelayCommand(async () => await Cancel()); } }

        public DisableAccountPageViewModel(IApiService apiService, IDialogService dialogService,
            INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            AcceptDisableAccount = true;
            LoadTypes();
        }

        public void Accept()
        {
            AcceptDisableAccount = false;
        }

        private async Task Cancel()
        {
            await navigationService.Navigate(AppPages.LandingPage);
        }

        public async Task LoadTypes()
        {
            dialogService.ShowProgress();
            ResponseDeactivationType response = await apiService.DeactivationType();
            dialogService.HideProgress();

            DeactivationType = new ObservableCollection<DeactivationType>();
            if (response.Success && response.StatusCode == 0)
            {
                DeactivationType = new ObservableCollection<DeactivationType>(response.DataList);
            }
        }

        private async Task DisableAccount()
        {
            if (ValidateData() == false)
            {
                return;
            }

            dialogService.ShowProgress();
            RequestDeactivateUserAccount request = new RequestDeactivateUserAccount
            {
                SelectedOption = new DeactivationSelected
                {
                    Code = DeactivationTypeSelected.DeactivationTypeId,
                    Comment = Other
                }
            };
            ResponseBase response = await apiService.DisableAccount(request);
            dialogService.HideProgress();
            await ValidateDataResponseDisable(response);
            return;
        }

        private async Task ValidateDataResponseDisable(ResponseBase response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);

            if (response.Success && response.StatusCode == 0)
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
            ErrorDeactivationType = DeactivationTypeSelected == null ? AppResources.FieldRequired : string.Empty;
            ErrorOther = IsDeactivationOther ? string.IsNullOrEmpty(Other) ? AppResources.FieldRequired : string.Empty : string.Empty;

            return
                string.IsNullOrEmpty(ErrorDeactivationType) &&
                string.IsNullOrEmpty(ErrorOther);

        }
    }
}
