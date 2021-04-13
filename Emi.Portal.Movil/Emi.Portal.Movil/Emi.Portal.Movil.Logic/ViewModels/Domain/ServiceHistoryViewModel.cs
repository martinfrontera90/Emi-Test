namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class ServiceHistoryViewModel : ViewModelBase, IServiceHistoryViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        ILoginViewModel loginViewModel;

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                if (code != value)
                {
                    code = value;
                    RaisePropertyChanged("Code");
                }
            }
        }

        private string cost;
        public string Cost
        {
            get { return cost; }
            set
            {
                if (cost != value)
                {
                    cost = value;
                    RaisePropertyChanged("Cost");
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

        private string specialityName;
        public string SpecialityName
        {
            get { return specialityName; }
            set
            {
                if (specialityName != value)
                {
                    specialityName = value;
                    RaisePropertyChanged("SpecialityName");
                }
            }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    RaisePropertyChanged("Date");
                }
            }
        }

        private string doctorName;
        public string DoctorName
        {
            get { return doctorName; }
            set
            {
                if (doctorName != value)
                {
                    doctorName = value;
                    RaisePropertyChanged("DoctorName");
                }
            }
        }

        private string cityName;
        public string CityName
        {
            get { return cityName; }
            set
            {
                if (cityName != value)
                {
                    cityName = value;
                    RaisePropertyChanged("CityName");
                }
            }
        }

        private string fileCode;
        public string FileCode
        {
            get { return fileCode; }
            set
            {
                if (fileCode != value)
                {
                    fileCode = value;
                    RaisePropertyChanged("FileCode");
                }
            }
        }

        private bool isCO;
        public bool IsCO
        {
            get { return isCO; }
            set
            {
                if (isCO != value)
                {
                    isCO = value;
                    RaisePropertyChanged("IsCO");
                }
            }
        }

        private bool isUY;
        public bool IsUY
        {
            get { return isUY; }
            set
            {
                if (isUY != value)
                {
                    isUY = value;
                    RaisePropertyChanged("IsUY");
                }
            }
        }

        private ServiceType serviceType;
        public ServiceType ServiceType
        {
            get { return serviceType; }
            set
            {
                if (serviceType != value)
                {
                    serviceType = value;
                    RaisePropertyChanged("ServiceType");

                }
            }
        }

        private bool cancelable;
        public bool Cancelable
        {
            get { return cancelable; }
            set
            {
                if (cancelable != value)
                {
                    cancelable = value;
                    RaisePropertyChanged("Cancelable");
                }
            }
        }

        private bool canceled;
        public bool Canceled
        {
            get { return canceled ? false : true; }
            set
            {
                if (canceled != value)
                {
                    canceled = value;
                    RaisePropertyChanged("Canceled");
                }
            }
        }

        public PendingCoordination Coordinationasd { get; set; }

        private PendingCoordination coordination;
        public PendingCoordination Coordination
        {
            get { return coordination; }
            set
            {
                if (coordination != value)
                {
                    coordination = value;
                    RaisePropertyChanged("Coordination");
                }
            }
        }

        public string UserNameModified
        {
            get { return !string.IsNullOrEmpty(this.UserName) ? this.UserName : string.Format("{0} {1} {2} {3}", loginViewModel.User.NameOne, loginViewModel.User.NameTwo, loginViewModel.User.LastNameOne, loginViewModel.User.LastNameTwo); }
        }

        private string serviceNumber;
        public string ServiceNumber
        {
            get { return this.serviceType == ServiceType.DoctorHome ? string.Format("No.{0}", this.serviceNumber) : this.specialityName; }
            set
            {
                if (serviceNumber != value)
                {
                    serviceNumber = value;
                    RaisePropertyChanged("ServiceNumber");
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

        private string idService;
        public string IdService
        {
            get { return idService; }
            set
            {
                if (idService != value)
                {
                    idService = value;
                    RaisePropertyChanged("IdService");
                }
            }
        }

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

        private string userDocument;
        public string UserDocument
        {
            get { return userDocument; }
            set
            {
                if (userDocument != value)
                {
                    userDocument = value;
                    RaisePropertyChanged("UserDocument");
                }
            }
        }

        private string userDocumentType;
        public string UserDocumentType
        {
            get { return userDocumentType; }
            set
            {
                if (userDocumentType != value)
                {
                    userDocumentType = value;
                    RaisePropertyChanged("UserDocumentType");
                }
            }
        }

        private string userDocumentTypeStr;
        public string UserDocumentTypeStr
        {
            get { return userDocumentTypeStr; }
            set
            {
                if (userDocumentTypeStr != value)
                {
                    userDocumentTypeStr = value;
                    RaisePropertyChanged("UserDocumentTypeStr");
                }
            }
        }

        public ICommand OptionsCommand { get { return new RelayCommand(Options); } }
        public ICommand CancelScheduledServicesCommand { get { return new RelayCommand(CancelScheduledServices); } }

        private async void CancelScheduledServices()
        {
            if (this.serviceType.Equals(ServiceType.DoctorHome))
            {
                if (await dialogService.ShowConfirm(AppResources.CancelMedicalHomeService, string.Format(AppResources.MessageCancelMedigalHomeService, UserNameModified)))

                {
                    dialogService.ShowProgress();
                    RequestCancelMedicalHomeService request = new RequestCancelMedicalHomeService
                    {
                        IdService = IdService,
                    };


                    ResponseCancelMedicalHomeService response = await apiService.CancelMedicalHomeService(request);
                    dialogService.HideProgress();
                    ValidateResponseCancelMedicalHomeService(response);
                }
            }
            else
            {
                #region original
                if (await dialogService.ShowConfirm(AppResources.TitleSheduledServices, AppResources.CancelSheduledService))
                {
                    dialogService.ShowProgress();
                    ResponseLogin user =  ServiceLocator.Current.GetInstance<ILoginViewModel>().User;
                    int.TryParse(user.DocumentType, out int applicantDocumentType);

                    RequestCancelService request = new RequestCancelService
                    {
                        Code = Code,
                        Document = user.Document,
                        DocumentType = user.DocumentType,
                        PendingCoordination = Coordination,
                        ServiceType = ServiceType.ToString(),
                        applicant = new Applicant
                        {
                            ApplicantCellPhone = user.CellPhone,
                            ApplicantDocument = user.Document,
                            ApplicantDocumentType = applicantDocumentType,
                            ApplicantDocumentTypeName = user.DocumentTypeName,
                            ApplicantLastNameOne = user.LastNameOne,
                            ApplicantLastNameTwo = user.LastNameTwo,
                            ApplicantMail = user.UserName,
                            ApplicantNameOne = user.NameOne,
                            ApplicantNameTwo = user.NameTwo 
                        }

                    };
                    ResponseCancelService response = await apiService.CancelService(request);
                    dialogService.HideProgress();
                    ValidateResponseCancelService(response);
                }
                #endregion
            }
        }

        private async void ValidateResponseCancelMedicalHomeService(ResponseCancelMedicalHomeService response)
        {
            await dialogService.ShowMessage(response.Title, response.CancelMedicalHomeServiceResponse.Message);
            if (response.Success && response.StatusCode == 0)
            {
                IScheduledServicesPageViewModel scheduledServicesPageViewModel = ServiceLocator.Current.GetInstance<IScheduledServicesPageViewModel>();
                scheduledServicesPageViewModel.LoadScheduledServices();
            }
        }

        private async void ValidateResponseCancelService(ResponseCancelService response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);
            if (response.Success && response.StatusCode == 0)
            {
                IScheduledServicesPageViewModel scheduledServicesPageViewModel = ServiceLocator.Current.GetInstance<IScheduledServicesPageViewModel>();
                scheduledServicesPageViewModel.LoadScheduledServices();
            }
        }

        private async void Options()
        {
            string option = await dialogService.ServiceHistory();

            IServicesHistoryPageViewModel serviceHistoryViewModel = ServiceLocator.Current.GetInstance<IServicesHistoryPageViewModel>();
            serviceHistoryViewModel.ServiceHistorySelected = this;

            switch (option)
            {
                case "Descargar PDF":
                    await serviceHistoryViewModel.DownloadPDF();
                    break;
                case "Enviar PDF":
                    await serviceHistoryViewModel.SendPDF();
                    break;
                default:
                    break;
            }
        }


        public ServiceHistoryViewModel()
        {
            IsCO = AppConfigurations.Brand == "emi";
            IsUY = AppConfigurations.Brand == "ucm";
            apiService = ServiceLocator.Current.GetInstance<IApiService>();
            dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
        }
    }
}
