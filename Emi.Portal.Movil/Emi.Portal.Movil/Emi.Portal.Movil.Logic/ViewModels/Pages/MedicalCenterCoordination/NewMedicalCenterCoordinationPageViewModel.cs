namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MedicalCenterCoordination
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class NewMedicalCenterCoordinationPageViewModel : ViewModelBase, INewMedicalCenterCoordinationPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IPhoneService phoneService;
        IValidatorService validatorService;

        public ResponsePreConfirmNewMedicalCenterCoordination responsePreConfirmation { get; set; }
        public CoordinationViewModel PreCoordination { get; set; }

        public ObservableCollection<Service> Services { get; set; }
        public ObservableCollection<Speciality> Specialities { get; set; }

        private PersonViewModel personSelected;
        public PersonViewModel PersonSelected
        {
            get { return personSelected; }
            set
            {
                if (personSelected != value)
                {
                    personSelected = value;
                    RaisePropertyChanged("PersonSelected");
                }
            }
        }

        private Service serviceSelected;
        public Service ServiceSelected
        {
            get { return serviceSelected; }
            set
            {
                if (serviceSelected != value)
                {
                    serviceSelected = value;
                    RaisePropertyChanged("ServiceSelected");
                    LoadSpeciality();

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
                    LoadMedicalCenters();

                }
            }
        }
        public List<Schedule> AllSchedules { get; set; }
        public ObservableCollection<MedicalCenter> MedicalCenters { get; set; }

        private ObservableCollection<ScheduleViewModel> schedules;
        public ObservableCollection<ScheduleViewModel> Schedules
        {
            get { return schedules; }
            set
            {
                if (schedules != value)
                {
                    schedules = value;
                    RaisePropertyChanged("Schedules");
                }
            }
        }

        private MedicalCenter medicalCenterSelected;
        public MedicalCenter MedicalCenterSelected
        {
            get { return medicalCenterSelected; }
            set
            {
                if (medicalCenterSelected != value)
                {
                    medicalCenterSelected = value;
                    RaisePropertyChanged("MedicalCenterSelected");
                    if (medicalCenterSelected != null)
                        LoadSchedules(medicalCenterSelected.MedicalCenterSchedules);
                    else
                        Schedules.Clear();
                }
            }
        }

        private ScheduleViewModel scheduleSelected;
        public ScheduleViewModel ScheduleSelected
        {
            get { return scheduleSelected; }
            set
            {
                if (scheduleSelected != value)
                {
                    scheduleSelected = value;
                    RaisePropertyChanged("ScheduleSelected");
                }
            }
        }

        private ObservableCollection<CoordinationPaymentMethodViewModel> paymentMethods;
        public ObservableCollection<CoordinationPaymentMethodViewModel> PaymentMethods
        {
            get { return paymentMethods; }
            set
            {
                paymentMethods = value;
                RaisePropertyChanged("PaymentMethods");
            }
        }

        private CoordinationPaymentMethodViewModel paymentMethodSelected;
        public CoordinationPaymentMethodViewModel PaymentMethodSelected
        {
            get { return paymentMethodSelected; }
            set
            {
                if (paymentMethodSelected != value)
                {
                    paymentMethodSelected = value;
                    RaisePropertyChanged("Selected");
                }
            }
        }

        private string htmlCoordinationPayment;
        public string HtmlCoordinationPayment
        {
            get { return htmlCoordinationPayment; }
            set
            {
                if (htmlCoordinationPayment != value)
                {
                    htmlCoordinationPayment = value;
                    RaisePropertyChanged("HtmlCoordinationPayment");
                }
            }
        }

        private ObservableCollection<PersonViewModel> people;
        public ObservableCollection<PersonViewModel> People
        {
            get { return people; }
            set
            {
                if (people != value)
                {
                    people = value;
                    RaisePropertyChanged("People");
                }
            }
        }

        private ObservableCollection<ServicesEnabledViewModel> servicesEnableds;
        public ObservableCollection<ServicesEnabledViewModel> ServicesEnableds
        {
            get { return servicesEnableds; }
            set
            {
                if (servicesEnableds != value)
                {
                    servicesEnableds = value;
                    RaisePropertyChanged("ServicesEnableds");
                }
            }
        }

        private bool isVisiblePersonGrid;
        public bool IsVisiblePersonGrid
        {
            get { return isVisiblePersonGrid; }
            set
            {
                if (isVisiblePersonGrid != value)
                {
                    isVisiblePersonGrid = value;
                    RaisePropertyChanged("IsVisiblePersonGrid");
                }
            }
        }

        private bool isVisibleEntryPersonFullNames;
        public bool IsVisibleEntryPersonFullNames
        {
            get { return isVisibleEntryPersonFullNames; }
            set
            {
                if (isVisibleEntryPersonFullNames != value)
                {
                    isVisibleEntryPersonFullNames = value;
                    RaisePropertyChanged("IsVisibleEntryPersonFullNames");
                }
            }
        }        
        #endregion

        #region Commands
        public ICommand ClosePaymentPageCommand { get { return new RelayCommand(ClosePaymentPage); } }
        public ICommand InformationCommand { get { return new RelayCommand(Information); } }
        public ICommand SchedulesCommand { get { return new RelayCommand(SelectSchedule); } }
        #endregion

        #region Methods
        private async void Information()
        {
            await dialogService.ShowMessage(AppResources.TittleNewCoordination, AppResources.AddFamiliyCoordinationPage);
        }

        private async void SelectSchedule()
        {
            if (MedicalCenterSelected != null)
            {
                await navigationService.Navigate(AppPages.SchedulesMedicalCenterCoordinationPage);
                return;
            }

            if (SpecialitySelected != null && SpecialitySelected.Type == "3")
            {
                await dialogService.ShowMessage(AppResources.TittleMedicalCenterCoordination, SpecialitySelected.Message);
                return;
            }

            await dialogService.ShowMessage(AppResources.TittleMedicalCenterCoordination, "Por favor seleccionar un centro médico.");

        }

        private async void ClosePaymentPage()
        {
            if (await dialogService.ShowConfirm(AppResources.TitlePayCoordination, "¿Desea abandonar la página de pago?"))
            {
                await navigationService.BackModal();
            }
        }

        private async void LoadSpeciality()
        {
            if (ServiceSelected != null)
            {
                dialogService.ShowProgress();
                RequestSpecialities requestSpecialities = new RequestSpecialities
                {
                    ServiceType = ServiceSelected.Code
                };
                ResponseSpecialities responseSpecialities = await apiService.GetSpecialties(requestSpecialities);
                dialogService.HideProgress();
                ValidateResponseSpecialities(responseSpecialities);
            }
            else
                Specialities.Clear();
        }

        private void ValidateResponseSpecialities(ResponseSpecialities responseSpecialities)
        {
            if (responseSpecialities.Success)
            {
                Specialities.Clear();
                foreach (Speciality speciality in responseSpecialities.MedicalSpecialites)
                    Specialities.Add(speciality);
            }
        }

        public async Task LoadData()
        {
            dialogService.ShowProgress();
            RequestServiceTypes requestServiceTypes = new RequestServiceTypes();
            ResponseServiceTypes responseServiceTypes = await apiService.GetServicesTypes(requestServiceTypes);
            ValidateResponseServiceTypes(responseServiceTypes);
            dialogService.HideProgress();            
        }

        public async Task LoadPersons()
        {
            if (PersonSelected != null)
            {
                IsVisiblePersonGrid = false;
                IsVisibleEntryPersonFullNames = true;
                return;
            }

            IsVisiblePersonGrid = true;
            IsVisibleEntryPersonFullNames = false;
            dialogService.ShowProgress();

            ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            RequestPeople request = new RequestPeople
            {
                IdReference = loginViewModel.User.IdReference,
                Document = loginViewModel.User.Document,
                DocumentType = loginViewModel.User.DocumentType
            };
            ResponseBeneficiaries response = await apiService.GetPersons(request);
            dialogService.HideProgress();
            ValidateResponseGetPersons(response);
        }

        private async void ValidateResponseGetPersons(ResponseBeneficiaries response)
        {
            People = new ObservableCollection<PersonViewModel>();
            ServicesEnableds = new ObservableCollection<ServicesEnabledViewModel>();
            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            foreach (var person in response.Beneficiaries)
            {
                PersonViewModel personViewModel = new PersonViewModel();
                ViewModelHelper.SetPersonToPersonViewModel(personViewModel, person);
                People.Add(personViewModel);
            }
        }

        private async void ValidateResponseServiceTypes(ResponseServiceTypes response)
        {
            Services.Clear();
            if (response.Success)
            {
                foreach (Service service in response.Services)
                    Services.Add(service);
                return;
            }

            await dialogService.ShowMessage(response.Title, response.Message);
            await navigationService.Back();
        }

        public void ClearData()
        {
            Schedules = new ObservableCollection<ScheduleViewModel>();
            Specialities = new ObservableCollection<Speciality>();
            Services = new ObservableCollection<Service>();
            MedicalCenters = new ObservableCollection<MedicalCenter>();        
        }

        public async void PreConfirmNewCoordination()
        {
            if (PersonSelected == null || ServiceSelected == null || SpecialitySelected == null || MedicalCenterSelected == null)
            {
                await dialogService.ShowMessage(string.Empty, "Por favor seleccionar las opciones.");
                return;
            }

            if (specialitySelected.Type == "3")
            {
                await dialogService.ShowMessage("Coordinación", specialitySelected.Message);
                return;
            }
            PreConfirm();
        }
        private async void PreConfirm()
        {
            RequestPreConfirmNewMedicalCenterCoordination requestPreConfirm = new RequestPreConfirmNewMedicalCenterCoordination
            {
                ClinicCode = MedicalCenterSelected.ClinicCode,
                DocumentType = PersonSelected.DocumentType,
                Email = PersonSelected.Email,
                IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference,
                LocalCode = MedicalCenterSelected.LocalCode,
                Number = PersonSelected.Document,
                Phone = string.IsNullOrEmpty(PersonSelected.CellPhone) ? PersonSelected.Phone : PersonSelected.CellPhone,
                RDACode = medicalCenterSelected.RDACode,
                SpecialityCode = specialitySelected.Code,
            };
            dialogService.ShowProgress();
            responsePreConfirmation = await apiService.PreConfirmNewMedicalCenterCoordination(requestPreConfirm);
            dialogService.HideProgress();
            ValidatePreConfirmationNewMedicalCenterCoordination(responsePreConfirmation);
        }

        private async void ValidatePreConfirmationNewMedicalCenterCoordination(ResponsePreConfirmNewMedicalCenterCoordination response)
        {
            if (response.Success)
            {
                if (response.StatusCode == 0)
                {
                    LoadDetailPreConfirmNewCoordination(response);
                    return;
                }
            }
            await dialogService.ShowMessage(response.Title, response.Message);
        }

        private async void LoadDetailPreConfirmNewCoordination(ResponsePreConfirmNewMedicalCenterCoordination response)
        {
            IMedicalCenterCoordinationPageViewModel medicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
            if (response.PreConfirm != null)
            {
                PreCoordination = new CoordinationViewModel
                {
                    Address = MedicalCenterSelected.Address,
                    AgendaName = ServiceSelected.Name,
                    ClinicCode = MedicalCenterSelected.ClinicCode,
                    ClinicName = MedicalCenterSelected.ClinicName,
                    Date = ScheduleSelected.Date,
                    Document = PersonSelected.Document,
                    FullAddress = string.Format("{0} - {1}", MedicalCenterSelected.ClinicName, response.PreConfirm.ClinicAddress),
                    Hour = ScheduleSelected.Time,
                    IsVisiblePay = true,
                    IsVisibleRecommendation = false,
                    Latitude = double.Parse((response.PreConfirm.Latitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator))),
                    Longitude = double.Parse((response.PreConfirm.Longitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator))),
                    Names = PersonSelected.Names,
                    NameSpecialty = SpecialitySelected.Description,
                    Price = response.PreConfirm.Price,
                    RDACode = MedicalCenterSelected.RDACode,
                    SpecialityCode = SpecialitySelected.Code,
                    Time = ScheduleSelected.Time,
                    TitleButton = response.PreConfirm.Price == "0.0" ? AppResources.TitleConfirmNewCoordination : AppResources.TitlePay,
                    TypeCoordination = SpecialitySelected.Description,
                    YearMonthDay = ScheduleSelected.YearMonthDay
                };
                medicalCenterCoordinationPageViewModel.CoordinationSelected = PreCoordination;
                await navigationService.Navigate(AppPages.MedicalCenterCoordinationDetailPage);
            }
        }

        private void LoadSchedules(List<Schedule> medicalCenterSchedules)
        {
            Schedules.Clear();
            dialogService.ShowProgress();
            foreach (Schedule schedule in medicalCenterSchedules)
            {
                ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
                ViewModelHelper.SetScheduleToScheduleViewModel(scheduleViewModel, schedule);
                Schedules.Add(scheduleViewModel);
            }
            dialogService.HideProgress();
        }

        public async void LoadMedicalCenters()
        {
            if (SpecialitySelected == null)
            {
                MedicalCenters.Clear();
                return;
            }

            if (SpecialitySelected.Type == "3")
            {
                await dialogService.ShowMessage(AppResources.TittleMedicalCenterCoordination, SpecialitySelected.Message);
                return;
            }

            if (SpecialitySelected.Type == "2")
            {
                await dialogService.ShowMessage(AppResources.TittleMedicalCenterCoordination, SpecialitySelected.Message);
            }

            dialogService.ShowProgress();
            RequestMedicalCenterSchedules requestMedicalCenterSchedules = new RequestMedicalCenterSchedules
            {
                SpecialityCode = SpecialitySelected.Code,
            };

            ResponseMedicalCenterSchedules responseMedicalCenterSchedules = await apiService.GetMedicalCenterSchedules(requestMedicalCenterSchedules);
            dialogService.HideProgress();
            ValidateMedicalCenterSchedules(responseMedicalCenterSchedules);            

        }
        private async void ValidateMedicalCenterSchedules(ResponseMedicalCenterSchedules response)
        {
            MedicalCenters.Clear();
            if (response.Success && response.StatusCode == 0 && response.MedicalCenters != null)
            {

                if (response.MedicalCenters != null && response.MedicalCenters.Count == 0)
                {
                    await dialogService.ShowMessage(SpecialitySelected.Description, "La especialidad que estás solicitando no tiene agenda  disponible en las próximas 72 h hábiles. Por favor, inténtalo de nuevo a la brevedad o comunícate al 2487 3333 de 9 a 19 horas.");
                    return;
                }

                dialogService.ShowProgress();
                foreach (MedicalCenter medicalCenter in response.MedicalCenters)
                {
                    MedicalCenters.Add(medicalCenter);
                }
                dialogService.HideProgress();
            }
            else
                await dialogService.ShowMessage(response.Title, response.Message);

        }

        public async void ConfirmNewCoordination()
        {
            RequestNewMedicalCenterCoordination requestNewMedicalCenterCoordination = new RequestNewMedicalCenterCoordination
            {
                ClinicCode = MedicalCenterSelected.ClinicCode,
                DocumentType = PersonSelected.DocumentType,
                Email = PersonSelected.Email,
                IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference,
                LocalCode = MedicalCenterSelected.LocalCode,
                Number = PersonSelected.Document,
                PatientCode = responsePreConfirmation.PreConfirm.PatientCode,
                PatientName = PersonSelected.FullNames,
                Phone = PersonSelected.Phone,
                ProductCode = responsePreConfirmation.PreConfirm.ProductCode,
                RDACode = medicalCenterSelected.RDACode,
                SpecialityCode = specialitySelected.Code,
                Time = ScheduleSelected.Time,
                YearMonthDay = ScheduleSelected.YearMonthDay,
            };
            dialogService.ShowProgress();
            ResponsePendingCoordinations responsePendingCoordinations = await apiService.ConfirmCoordination(requestNewMedicalCenterCoordination);
            dialogService.HideProgress();
            ValidateNewMedicalCenterCoordination(responsePendingCoordinations);
        }

        private async void ValidateNewMedicalCenterCoordination(ResponsePendingCoordinations response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);

            if (response.Success)
            {
                if (response.StatusCode == 0)
                {
                    IMedicalCenterCoordinationPageViewModel medicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
                    medicalCenterCoordinationPageViewModel.CoordinationSelected.Recommendations = response.Recommendations;
                    medicalCenterCoordinationPageViewModel.CoordinationSelected.IsVisiblePay = false;
                    medicalCenterCoordinationPageViewModel.CoordinationSelected.IsVisibleRecommendation = true;
                    await navigationService.Navigate(AppPages.MedicalCenterCoordinationDetailPage, true);
                }
            }
        }

        public async void GetPaymentMethods()
        {
            RequestCoordinationPaymentMethod request = new RequestCoordinationPaymentMethod
            {
                ClinicCode = MedicalCenterSelected.ClinicCode,
                DocumentType = PersonSelected.DocumentType,
                Email = PersonSelected.Email,
                IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference,
                LocalCode = MedicalCenterSelected.LocalCode,
                Number = PersonSelected.Document,
                PatientCode = responsePreConfirmation.PreConfirm.PatientCode,
                Phone = PersonSelected.Phone,
                Price = responsePreConfirmation.PreConfirm.Price,
                ProductCode = responsePreConfirmation.PreConfirm.ProductCode,
                RDACode = medicalCenterSelected.RDACode,
                SpecialityCode = specialitySelected.Code,
                Time = ScheduleSelected.Time,
                YearMonthDay = ScheduleSelected.YearMonthDay
            };
            dialogService.ShowProgress();
            ResponseCoordinationPaymentMethod response = await apiService.GetPaymentMethods(request);
            ValidateResponseCoordinationPaymentMethod(response);
        }

        private async void ValidateResponseCoordinationPaymentMethod(ResponseCoordinationPaymentMethod response)
        {
            dialogService.HideProgress();
            if (response.Success)
            {
                PaymentMethods.Clear();
                CoordinationPaymentMethod ExternalMethod = response.CoordinationPaymentMethods.Where(x => x.ExternalMethod).FirstOrDefault();

                if (ExternalMethod != null)
                {
                    ExternalMethod.IconApp = "pasarela";
                    ExternalMethod.PaymentMethodName = "Pago en línea";
                    ExternalMethod.PaymentMethodDescription = string.Empty;
                    SetCoordination(ExternalMethod);
                }

                foreach (CoordinationPaymentMethod item in response.CoordinationPaymentMethods)
                {
                    if (item.ExternalMethod == false)
                    {
                        SetCoordination(item);
                    }
                }
                await navigationService.Navigate(AppPages.CoordinationPaymentMethodPage);
            }
            else
            {
                await dialogService.ShowMessage(response.Title, response.Message);
            }
        }

        void SetCoordination(CoordinationPaymentMethod model)
        {
            CoordinationPaymentMethodViewModel coordinationPaymentMethodViewModel = new CoordinationPaymentMethodViewModel();
            ViewModelHelper.SetCoordinationPaymentMethodToCoordinationPaymentMethodViewModel(model, coordinationPaymentMethodViewModel);
            PaymentMethods.Add(coordinationPaymentMethodViewModel);
        }

        public async void Payment()
        {
            if (PaymentMethodSelected.ExternalMethod == false)
            {
                dialogService.ShowProgress();
                RequestPayMedicalCenterCoordination request = new RequestPayMedicalCenterCoordination
                {
                    ClinicCode = MedicalCenterSelected.ClinicCode,
                    DocumentType = PersonSelected.DocumentType,
                    Email = PersonSelected.Email,
                    IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference,
                    LocalCode = MedicalCenterSelected.LocalCode,
                    Number = PersonSelected.Document,
                    PatientCode = responsePreConfirmation.PreConfirm.PatientCode,
                    PatientName = PersonSelected.FullNames,
                    Phone = string.IsNullOrEmpty(PersonSelected.CellPhone) ? PersonSelected.Phone : PersonSelected.CellPhone,
                    ProductCode = responsePreConfirmation.PreConfirm.ProductCode,
                    RDACode = MedicalCenterSelected.RDACode,
                    SpecialityCode = SpecialitySelected.Code,
                    Time = ScheduleSelected.Time,
                    YearMonthDay = ScheduleSelected.YearMonthDay,
                    Installments = PaymentMethodSelected.InstallmentSelected.ToString(),
                    PaymentMethodCode = PaymentMethodSelected.PaymentMethodCode,
                    PaymentMethodName = PaymentMethodSelected.PaymentMethodName,
                    Price = responsePreConfirmation.PreConfirm.Price,
                    UserEmail = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.UserName
                };
                ResponsePayMedicalCenterCoordination response = await apiService.PayMedicalCenterCoordination(request);
                dialogService.HideProgress();
                ValidateResponsePayMedicalCenterCoordination(response);
            }
            else
            {
                string Url = string.Format("{0}?Price={1}&UserEmail={2}&IdReference={3}&Token={4}&RDACode={5}&LocalCode={6}&ClinicCode={7}&SpecialityCode={8}&DocumentType={9}&Number={10}&PatientCode={11}&ProductCode={12}&YearMonthDay={13}&Time={14}&Phone={15}&Email={16}&PaymentMethodCode=Online&PaymentMethodName=Online&Installments={17}&AgendaType={18}&PatientName={19}",
                    AppConfigurations.UrlPayCoordinationApp,
                    responsePreConfirmation.PreConfirm.Price,
                    ServiceLocator.Current.GetInstance<ILoginViewModel>().User.UserName,
                    ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference,
                    "0123456789",
                    MedicalCenterSelected.RDACode,
                    MedicalCenterSelected.LocalCode,
                    MedicalCenterSelected.ClinicCode,
                    SpecialitySelected.Code,
                    PersonSelected.DocumentType,
                    PersonSelected.Document,
                    responsePreConfirmation.PreConfirm.PatientCode,
                    responsePreConfirmation.PreConfirm.ProductCode,
                    ScheduleSelected.YearMonthDay,
                    ScheduleSelected.Time,
                    PersonSelected.CellPhone != null ? PersonSelected.CellPhone : PersonSelected.Phone,
                    PersonSelected.Email,
                    PaymentMethodSelected.InstallmentSelected.ToString(),
                    ServiceSelected.Code,
                    PersonSelected.FullNames);

                await dialogService.ShowMessage(AppResources.TittleMedicalCenterCoordination, "Tu coordinación quedó reservada, finalizado el proceso de pago se te notificará confirmada");
                phoneService.OpenUrl(Url);
                await navigationService.BackToRoot();
                await navigationService.Navigate(AppPages.MedicalCenterCoordinationPage);
            }
        }

        private async void ValidateResponsePayMedicalCenterCoordination(ResponsePayMedicalCenterCoordination response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);
            if (response.Success && response.StatusCode == 0)
            {
                IMedicalCenterCoordinationPageViewModel pageViewModel = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
                pageViewModel.CoordinationSelected = new CoordinationViewModel
                {
                    Address = responsePreConfirmation.PreConfirm.ClinicAddress,
                    AgendaName = ServiceSelected.Name,
                    ClinicCode = MedicalCenterSelected.ClinicCode,
                    ClinicName = MedicalCenterSelected.ClinicName,
                    Cost = responsePreConfirmation.PreConfirm.Price,
                    Date = ScheduleSelected.Date,
                    Document = PersonSelected.Document,
                    FullAddress = string.Format("{0} - {1}", MedicalCenterSelected.ClinicName, responsePreConfirmation.PreConfirm.ClinicAddress),
                    Hour = ScheduleSelected.Time,
                    Time = ScheduleSelected.Time,
                    IsVisiblePay = false,
                    IsVisibleRecommendation = true,
                    Latitude = double.Parse((responsePreConfirmation.PreConfirm.Latitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator))),
                    Longitude = double.Parse((responsePreConfirmation.PreConfirm.Longitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator))),
                    Names = PersonSelected.Names,
                    NameSpecialty = SpecialitySelected.Description,
                    Price = responsePreConfirmation.PreConfirm.Price,
                    RDACode = MedicalCenterSelected.RDACode,
                    Recommendations = response.Recommendations,
                    SpecialityCode = SpecialitySelected.Code,
                    YearMonthDay = ScheduleSelected.YearMonthDay
                };
                await navigationService.BackToRoot();
                await navigationService.Navigate(AppPages.MedicalCenterCoordinationDetailPage, true);
            }
        }
        #endregion

        #region Constructor
        public NewMedicalCenterCoordinationPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService, IPhoneService phoneService, IValidatorService validatorService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.phoneService = phoneService;
            this.validatorService = validatorService;
            HtmlCoordinationPayment = string.Empty;
            AllSchedules = new List<Schedule>();
            MedicalCenters = new ObservableCollection<MedicalCenter>();
            Services = new ObservableCollection<Service>();
            Schedules = new ObservableCollection<ScheduleViewModel>();
            Specialities = new ObservableCollection<Speciality>();
            PaymentMethods = new ObservableCollection<CoordinationPaymentMethodViewModel>();
        }
        #endregion
    }
}
