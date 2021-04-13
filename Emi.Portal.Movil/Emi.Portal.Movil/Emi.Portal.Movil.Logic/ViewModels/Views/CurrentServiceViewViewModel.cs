namespace Emi.Portal.Movil.Logic.ViewModels.Views
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Contracts.Views;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    public class CurrentServiceViewViewModel : ViewModelBase, ICurrentServiceViewViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        ILoginViewModel loginViewModel;
        INavigationService navigationService;
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    RaisePropertyChanged("UserName");
                }
            }
        }
        private bool inProgress;
        public bool InProgress
        {
            get { return inProgress; }
            set
            {
                if (inProgress != value)
                {
                    inProgress = value;
                    RaisePropertyChanged("InProgress");
                }
            }
        }
        private string descriptionState;
        public string DescriptionState
        {
            get { return descriptionState; }
            set
            {
                if (descriptionState != value)
                {
                    descriptionState = value;
                    RaisePropertyChanged("DescriptionState");
                }
            }
        }
        private string serviceTypeDescription;
        public string ServiceTypeDescription
        {
            get { return serviceTypeDescription; }
            set
            {
                if (serviceTypeDescription != value)
                {
                    serviceTypeDescription = value;
                    RaisePropertyChanged("ServiceTypeDescription");
                }
            }
        }
        private string messageHomeMedicalCare;
        public string MessageHomeMedicalCare
        {
            get { return messageHomeMedicalCare; }
            set
            {
                messageHomeMedicalCare = value;
                RaisePropertyChanged("MessageHomeMedicalCare");
            }
        }
        public CurrentServiceViewViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }
        public ICommand ScheduledServicesCommand { get { return new RelayCommand(ScheduledServices); } }
        public async Task GetMedicalHomeService()
        {
            dialogService.ShowProgress();
            loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            RequestExistsMedicalHomeService request = new RequestExistsMedicalHomeService
            {
                Document = loginViewModel.User.Document,
                DocumentType = loginViewModel.User.DocumentType,
                IdReference = loginViewModel.User.IdReference
            };
            ResponseExistsMedicalHomeService response = await apiService.GetExistsMedicalHomeService(request);
            if (response.ExistsMedicalHomeServiceResponse != null && response.ExistsMedicalHomeServiceResponse.CurrentService && response.ExistsMedicalHomeServiceResponse.ServiceTypeDescription.Equals("Atención médica domiciliaria"))
            {
                InProgress = response.ExistsMedicalHomeServiceResponse.CurrentService;
                DescriptionState = response.ExistsMedicalHomeServiceResponse.DescriptionState;
                MessageHomeMedicalCare = $"Tiene un servicio {ServiceTypeDescription} en curso {DescriptionState}";
                ServiceTypeDescription = response.ExistsMedicalHomeServiceResponse.ServiceTypeDescription;
                UserName = response.ExistsMedicalHomeServiceResponse.UserName;
            }
            else
            {
                InProgress = false;
            }
            dialogService.HideProgress();
        }
        private async void ScheduledServices()
        {
            await navigationService.Navigate(AppPages.ScheduledServicesPage);
        }
    }
}