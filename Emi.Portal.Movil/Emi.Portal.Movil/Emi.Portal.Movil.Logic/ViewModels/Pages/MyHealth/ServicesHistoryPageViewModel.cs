namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyHealth
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System.Linq;

    public class ServicesHistoryPageViewModel : ViewModelBase, IServicesHistoryPageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        ILoginViewModel loginViewModel;
        INavigationService navigationService;
        IPhoneService phoneService;
        IValidatorService validatorService;
        IPermissionService permissionService;

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

        private DateTime initDate;
        public DateTime InitDate
        {
            get { return initDate; }
            set
            {
                if (initDate != value)
                {
                    initDate = value;
                    RaisePropertyChanged("InitDate");
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    RaisePropertyChanged("EndDate");
                }
            }
        }

        private DateTime maximumDate;
        public DateTime MaximumDate
        {
            get { return maximumDate; }
            set
            {
                if (maximumDate != value)
                {
                    maximumDate = value;
                    RaisePropertyChanged("MaximumDate");
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

        private ObservableCollection<ServicesType> services;
        public ObservableCollection<ServicesType> Services
        {
            get { return services; }
            set
            {
                if (services != value)
                {
                    services = value;
                    RaisePropertyChanged(AppConfigurations.ServicesController);
                }
            }
        }

        private ServicesType serviceSelected;
        public ServicesType ServiceSelected
        {
            get { return serviceSelected; }
            set
            {
                if (serviceSelected != value)
                {
                    serviceSelected = value;
                    RaisePropertyChanged("ServiceSelected");
                }
            }
        }

        private ObservableCollection<Speciality> specialities;
        public ObservableCollection<Speciality> Specialities
        {
            get { return specialities; }
            set
            {
                if (specialities != value)
                {
                    specialities = value;
                    RaisePropertyChanged("Specialities");
                }
            }
        }

        private Speciality specialitySelected;
        public Speciality SpecialitySelected
        {
            get { return specialitySelected; }
            set
            {
                if (specialitySelected != value)
                {
                    specialitySelected = value;
                    RaisePropertyChanged("SpecialitySelected");
                }
            }
        }

        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities
        {
            get { return cities; }
            set
            {
                if (cities != value)
                {
                    cities = value;
                    RaisePropertyChanged("Cities");
                }
            }
        }

        private City citySelected;
        public City CitySelected
        {
            get { return citySelected; }
            set
            {
                if (citySelected != value)
                {
                    citySelected = value;
                    RaisePropertyChanged("CitySelected");
                }
            }
        }

        private ObservableCollection<Doctor> doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                if (doctors != value)
                {
                    doctors = value;
                    RaisePropertyChanged("Doctors");
                }
            }
        }

        private Doctor doctorSelected;
        public Doctor DoctorSelected
        {
            get { return doctorSelected; }
            set
            {
                if (doctorSelected != value)
                {
                    doctorSelected = value;
                    RaisePropertyChanged("DoctorSelected");
                }
            }
        }

        private ObservableCollection<ServiceHistoryViewModel> servicesHistory;
        public ObservableCollection<ServiceHistoryViewModel> ServicesHistory
        {
            get { return servicesHistory; }
            set
            {
                if (servicesHistory != value)
                {
                    servicesHistory = value;
                    RaisePropertyChanged("ServicesHistory");
                }
            }
        }

        private ServiceHistoryViewModel serviceHistorySelected;
        public ServiceHistoryViewModel ServiceHistorySelected
        {
            get { return serviceHistorySelected; }
            set
            {
                if (serviceHistorySelected != value)
                {
                    serviceHistorySelected = value;
                    RaisePropertyChanged("ServiceHistorySelected");
                }
            }
        }

        private ObservableCollection<Minor> minors;
        public ObservableCollection<Minor> Minors
        {
            get { return minors; }
            set
            {
                if (minors != value)
                {
                    minors = value;
                    RaisePropertyChanged("Minors");
                }
            }
        }

        private Minor minorSelected;
        public Minor MinorSelected
        {
            get { return minorSelected; }
            set
            {
                if (minorSelected != value)
                {
                    minorSelected = value;
                    RaisePropertyChanged("MinorSelected");
                    if (minorSelected != null)
                    {
                        RequestServicesHistoryLists request = new RequestServicesHistoryLists
                        {
                            Document = minorSelected.Document,
                            DocumentType = minorSelected.DocumentType
                        };

                        ServiceHistory(request);
                    }
                }
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                RaisePropertyChanged("IsRefreshing");
            }
        }

        private bool isVisiblePatient;
        public bool IsVisiblePatient
        {
            get { return isVisiblePatient; }
            set
            {
                isVisiblePatient = value;
                RaisePropertyChanged("IsVisiblePatient");
            }
        }

        private bool setUserAutenticated;
        public bool SetUserAutenticated
        {
            get { return setUserAutenticated; }
            set
            {
                setUserAutenticated = value;
                RaisePropertyChanged("SetUserAutenticated");
            }
        }

        public ICommand SearchServicesHistoryCommand { get { return new RelayCommand(SearchServicesHistory); } }

        private async void SearchServicesHistory()
        {
            dialogService.ShowProgress();
            RequestServicesHistory request = new RequestServicesHistory
            {
                DoctorName = DoctorSelected != null && DoctorSelected.Code != "-1" ? DoctorSelected.Code : string.Empty,
                Document = MinorSelected.Document,
                DocumentType = MinorSelected.DocumentType,
                EndDate = EndDate.ToString("yyyyMMdd"),
                InitDate = InitDate.ToString("yyyyMMdd"),
                City = CitySelected != null && CitySelected.Code != "-1" ? CitySelected.Code : string.Empty,
                ServiceType = ServiceSelected != null && ServiceSelected.Code != "-1" ? ServiceSelected.Code : string.Empty,
                Speciality = SpecialitySelected != null && SpecialitySelected.Code != "-1" ? SpecialitySelected.Code : string.Empty
            };

            ResponseServicesHistory response = await apiService.GetServicesHistory(request);
            dialogService.HideProgress();
            ValidateResponseServicesHistory(response);
        }

        private async void ServiceHistory(RequestServicesHistoryLists request)
        {
            dialogService.ShowProgress();
            ResponseServicesHistoryLists response = await apiService.GetServicesHistoryLists(request);
            ValidateResponseServicesHistoryLists(response);
            dialogService.HideProgress();
        }


        private async void ValidateResponseServicesHistory(ResponseServicesHistory response)
        {
            if (response.Success && response.StatusCode == 0)
            {
                if (response.ServicesHistory != null && response.ServicesHistory.Count > 0)
                {
                    ServicesHistory = new ObservableCollection<ServiceHistoryViewModel>();
                    foreach (ServiceHistory service in response.ServicesHistory)
                    {
                        ServiceHistoryViewModel viewModel = new ServiceHistoryViewModel
                        {
                            UserName = service.UserName,
                            UserDocumentTypeStr = service.UserDocumentTypeStr,
                            UserDocument = service.UserDocument,
                            ServiceTypeDescription = service.ServiceTypeDescription,
                            SpecialityName = service.SpecialityName,
                            DoctorName = service.DoctorName,
                            Date = service.Date,
                            //CityName = service.CityName,
                            FileCode = service.FileCode,
                            ServiceNumber = service.ServiceNumber,
                            Address = service.Address
                        };
                        ServicesHistory.Add(viewModel);
                    }
                    await navigationService.Navigate(Enumerations.AppPages.ServicesHistoryPage);
                }
                else
                {
                    await dialogService.ShowMessage(AppResources.TitleServicesHistory, "No se han encontrado resultados para tu búsqueda.");
                }
            }
            else
            {
                await dialogService.ShowMessage(response.Title, response.Message);
            }
        }

        public async Task LoadData()
        {
            dialogService.ShowProgress();

            RequestMinorAuthorizations requestMinor = new RequestMinorAuthorizations
            {
                DocumentType= loginViewModel.User.DocumentType,
                Document= loginViewModel.User.Document
            };

            Minors.Clear();

            ResponseMinorAuthorizations responseMinor = await apiService.GetMinorAuthorizations(requestMinor);

            dialogService.HideProgress();

            if (await validatorService.ValidateResponse(responseMinor) == false)
            {
                await navigationService.Back();
                return;
            }

            dialogService.ShowProgress();

            MinorSelected = new Minor
            {
                Document = loginViewModel.User.Document,
                DocumentType = loginViewModel.User.DocumentType,
                NameOne = loginViewModel.User.NameOne,
                NameTwo = loginViewModel.User.NameTwo,
                LastNameOne = loginViewModel.User.LastNameOne,
                LastNameTwo = loginViewModel.User.LastNameTwo
            };

            IsVisiblePatient = SetUserAutenticated = responseMinor.Minors != null && responseMinor.Minors.Where(x => x.Status.Equals("1")).ToList().Count > 0;

            if (IsVisiblePatient)
            {
                Minors.Add(MinorSelected);

                foreach (Minor minor in responseMinor.Minors)
                {
                    if (minor.Status == "1")
                    {
                        Minors.Add(minor);
                    }
                }
            }

            RequestServicesHistoryLists request = new RequestServicesHistoryLists
            {
                Document = loginViewModel.User.Document,
                DocumentType = loginViewModel.User.DocumentType
            };

            ResponseServicesHistoryLists response = await apiService.GetServicesHistoryLists(request);
            ValidateResponseServicesHistoryLists(response);
            dialogService.HideProgress();
        }

        private void ValidateResponseServicesHistoryLists(ResponseServicesHistoryLists response)
        {
            if (response.Success && response.StatusCode == 0)
            {
                Cities = new ObservableCollection<City>();
                Doctors = new ObservableCollection<Doctor>();
                Services = new ObservableCollection<ServicesType>();
                Specialities = new ObservableCollection<Speciality>();

                Cities.Add(new City { Code = "-1", Name = " " });
                foreach (City city in response.ServicesHistoryLists.Cities)
                {
                    if (!string.IsNullOrEmpty(city.Code) && !string.IsNullOrEmpty(city.Name))
                    {
                        Cities.Add(city);
                    }
                }

                Doctors.Add(new Doctor { Code = "-1", Name = " " });
                foreach (Doctor doctor in response.ServicesHistoryLists.Doctors)
                {
                    if (!string.IsNullOrEmpty(doctor.Code) && !string.IsNullOrEmpty(doctor.Name))
                    {
                        Doctors.Add(doctor);
                    }
                }

                Services.Add(new ServicesType { Code = "-1", Name = " " });
                foreach (ServicesType service in response.ServicesHistoryLists.ServicesType)
                {
                    if (!string.IsNullOrEmpty(service.Code) && !string.IsNullOrEmpty(service.Name))
                    {
                        Services.Add(service);
                    }
                }

                Specialities.Add(new Speciality { Code = "-1", Name = " " });
                foreach (Speciality speciality in response.ServicesHistoryLists.Specialities)
                {
                    if (!string.IsNullOrEmpty(speciality.Code) && !string.IsNullOrEmpty(speciality.Name))
                    {
                        Specialities.Add(speciality);
                    }
                }
            }
        }

        public async Task DownloadPDF()
        {
            dialogService.ShowProgress();
            RequestServiceFile request = new RequestServiceFile
            {
                Code = ServiceHistorySelected.FileCode,
                User = loginViewModel.User.UserName
            };
            ResponseServiceFile response = await apiService.GetServiceFile(request);
            dialogService.HideProgress();
            ValidateResponseServiceFile(response);
        }

        private async void ValidateResponseServiceFile(ResponseServiceFile response)
        {
            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            dialogService.ShowProgress();
            byte[] sPDFDecoded = Convert.FromBase64String(response.Value);

            if (phoneService.IsiOS || (!phoneService.IsiOS && await permissionService.CheckPermissions(Plugin.Permissions.Abstractions.Permission.Storage)))
            {
                await phoneService.SaveFiles(AppConfigurations.TitleFileServiceHistory, sPDFDecoded);
            }

            dialogService.HideProgress();
        }

        public async Task SendPDF()
        {
            dialogService.ShowProgress();
            RequestSendServiceFile request = new RequestSendServiceFile
            {
                Code = ServiceHistorySelected.FileCode,
                User = loginViewModel.User.UserName
            };
            ResponseSendServiceFile response = await apiService.SendServiceFile(request);
            dialogService.HideProgress();
            ValidateResponseSendServiceFile(response);
        }

        private void ValidateResponseSendServiceFile(ResponseSendServiceFile response)
        {
            dialogService.ShowMessage(response.Title, response.Message);
        }

        public ServicesHistoryPageViewModel(IApiService apiService, IDialogService dialogService, ILoginViewModel loginViewModel, INavigationService navigationService, IPhoneService phoneService, IValidatorService validatorService, IPermissionService permissionService)
        {

            EndDate = DateTime.Now;
            InitDate = EndDate.AddYears(-1);
            MaximumDate = DateTime.Now;

            IsCO = AppConfigurations.Brand == "emi";
            IsUY = AppConfigurations.Brand == "ucm";

            Cities = new ObservableCollection<City>();
            Doctors = new ObservableCollection<Doctor>();
            Services = new ObservableCollection<ServicesType>();
            ServicesHistory = new ObservableCollection<ServiceHistoryViewModel>();
            Specialities = new ObservableCollection<Speciality>();
            Minors = new ObservableCollection<Minor>();
            IsVisiblePatient = false;

            this.apiService = apiService;
            this.dialogService = dialogService;
            this.loginViewModel = loginViewModel;
            this.navigationService = navigationService;
            this.phoneService = phoneService;
            this.validatorService = validatorService;
            this.permissionService = permissionService;
        }
    }
}
