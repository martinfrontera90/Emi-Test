namespace Emi.Portal.Movil.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall;
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
    using Plugin.Geolocator.Abstractions;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Point = Models.Domain.Point;

    public class ServicesPageViewModel : ViewModelBase, IServicesPageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IValidatorService validatorService;
        ILoginViewModel loginViewModel;
        IPhoneService phoneService;
        IGeolocatorService geolocatorService;
        IPermissionService permissionService;



        public object MyProperty { get; set; }
        public ObservableCollection<PediatricAgendas> AgendasSaved;

        private bool isNavigating;
        public bool IsNavigating
        {
            get { return isNavigating; }
            set
            {
                if (isNavigating != value)
                {
                    isNavigating = value;
                    RaisePropertyChanged(nameof(IsNavigating));
                    IsVisibleLoading = IsNavigating;
                }
            }
        }

        private string urlChat = "http://www.google.com";
        public string UrlChat
        {
            get { return urlChat; }
            set
            {
                urlChat = value;
                RaisePropertyChanged(nameof(UrlChat));
            }
        }

        private bool isVisibleLoading;
        public bool IsVisibleLoading
        {
            get { return isVisibleLoading; }
            set
            {
                isVisibleLoading = value;
                RaisePropertyChanged(nameof(IsVisibleLoading));
            }
        }

        private string urlMap;
        public string UrlMap
        {
            get { return urlMap; }
            set
            {
                urlMap = value;
                RaisePropertyChanged(nameof(UrlMap));
            }
        }

        private string pediatricPhone;
        public string PediatricPhone
        {
            get { return pediatricPhone; }
            set
            {
                pediatricPhone = value;
                RaisePropertyChanged(nameof(PediatricPhone));
            }
        }

        private string errorPediatricPhone;
        public string ErrorPediatricPhone
        {
            get { return errorPediatricPhone; }
            set
            {
                errorPediatricPhone = value;
                RaisePropertyChanged(nameof(ErrorPediatricPhone));
            }
        }

        private string titlePage;
        public string TitlePage
        {
            get { return titlePage; }
            set
            {
                titlePage = value;
                RaisePropertyChanged(nameof(TitlePage));
            }
        }

        private string dynamicTitlePage;
        public string DynamicTitlePage
        {
            get { return dynamicTitlePage; }
            set
            {
                dynamicTitlePage = value;
                RaisePropertyChanged(nameof(DynamicTitlePage));
            }
        }

        private string otherSelected;
        public string OtherSelected
        {
            get { return otherSelected; }
            set
            {
                otherSelected = value;
                RaisePropertyChanged(nameof(OtherSelected));
            }
        }

        private bool isVisibleMap;
        public bool IsVisibleMap
        {
            get { return isVisibleMap; }
            set
            {
                isVisibleMap = value;
                RaisePropertyChanged(nameof(IsVisibleMap));
            }
        }

        private bool newAddressAdded;
        public bool NewAddressAdded
        {
            get { return newAddressAdded; }
            set
            {
                newAddressAdded = value;
                RaisePropertyChanged("NewAddressAdded");
            }
        }
        bool confirmAddress;
        public bool ConfirmAddress
        {
            get { return confirmAddress; }
            set
            {
                if (confirmAddress != value)
                {
                    confirmAddress = value;
                    RaisePropertyChanged("ConfirmAddress");
                }
            }
        }

        private bool readyLocation;
        public bool ReadyLocation
        {
            get { return readyLocation; }
            set
            {
                readyLocation = value;
                RaisePropertyChanged("ReadyLocation");
            }
        }

        private LocationViewModel currentLocation;
        public LocationViewModel CurrentLocation
        {
            get { return currentLocation; }
            set
            {
                currentLocation = value;
                RaisePropertyChanged("CurrentLocation");
            }
        }

        //private LocationViewModel addressLocation;
        //public LocationViewModel AddressLocation
        //{
        //    get { return addressLocation; }
        //    set
        //    {
        //        addressLocation = value;
        //        RaisePropertyChanged("AddressLocation");
        //    }
        //}


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
                    if (personSelected != null)
                    {
                        ValidateIsYoung();
                    }

                    IsVisibleListAddresses = value != null;
                }
            }
        }

        bool isVisibleListAddresses;
        public bool IsVisibleListAddresses
        {
            get { return isVisibleListAddresses; }
            set
            {
                if (isVisibleListAddresses != value)
                {
                    isVisibleListAddresses = value;
                    RaisePropertyChanged("IsVisibleListAddresses");
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

        private ServicesEnabledViewModel serviceSelected;
        public ServicesEnabledViewModel ServiceSelected
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

        private UserYoung userYoung;
        public UserYoung UserYoung
        {
            get { return userYoung; }
            set
            {
                if (userYoung != value)
                {
                    userYoung = value;
                    RaisePropertyChanged("UserYoung");
                }
            }
        }

        string serviceSelectedName;
        public string ServiceSelectedName
        {
            get { return serviceSelectedName; }
            set
            {
                serviceSelectedName = value;
                RaisePropertyChanged("ServiceSelectedName");
            }
        }

        private bool isToggledPhoneNumber;
        public bool IsToggledPhoneNumber
        {
            get { return isToggledPhoneNumber; }
            set
            {
                if (isToggledPhoneNumber != value)
                {
                    isToggledPhoneNumber = value;
                    RaisePropertyChanged("IsToggledPhoneNumber");
                }
                IsVisiblePatientData = !isToggledPhoneNumber;
            }
        }

        private bool isVisiblePatientData;
        public bool IsVisiblePatientData
        {
            get { return isVisiblePatientData; }
            set
            {
                if (isVisiblePatientData != value)
                {
                    isVisiblePatientData = value;
                    RaisePropertyChanged("IsVisiblePatientData");
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
                if (isCO != value)
                {
                    isUY = value;
                    RaisePropertyChanged("IsUY");
                }
            }
        }
        private string messageOpentok;
        public string MessageOpentok
        {
            get { return messageOpentok; }
            set
            {
                if (messageOpentok != value)
                {
                    messageOpentok = value;
                    RaisePropertyChanged("MessageOpentok");
                }
            }
        }

        private ObservableCollection<Quadrant> quadrants;
        public ObservableCollection<Quadrant> Quadrants
        {
            get { return quadrants; }
            set
            {
                if (quadrants != value)
                {
                    quadrants = value;
                    RaisePropertyChanged("Quadrants");
                }
            }
        }

        private ObservableCollection<Via> vias;
        public ObservableCollection<Via> Vias
        {
            get { return vias; }
            set
            {
                if (vias != value)
                {
                    vias = value;
                    RaisePropertyChanged("Vias");
                }
            }
        }


        private bool isInCoverage;
        public bool IsInCoverage
        {
            get { return isInCoverage; }
            set
            {
                if (isInCoverage != value)
                {
                    isInCoverage = value;
                    RaisePropertyChanged("IsInCoverage");
                }
            }
        }
        ResponseOpenTokDataForAffiliate responseOpentok = new ResponseOpenTokDataForAffiliate();
        private ObservableCollection<Departament> departaments;
        public ObservableCollection<Departament> Departaments
        {
            get { return departaments; }
            set
            {
                if (departaments != value)
                {
                    departaments = value;
                    RaisePropertyChanged("Departaments");
                }
            }
        }

        private Departament departamentSelected;
        public Departament DepartamentSelected
        {
            get { return departamentSelected; }
            set
            {
                if (departamentSelected != value)
                {
                    departamentSelected = value;
                    RaisePropertyChanged("DepartamentSelected");
                    if (DepartamentSelected != null)
                    {
                        LoadCities();
                    }

                    IsEnabledCities = DepartamentSelected != null && DepartamentSelected.Code != "009";
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
                    if (CitySelected != null)
                    {
                        LoadNeighborhoods();
                    }
                    IsEnabledNeighborhood = Neighborhoods != null && Neighborhoods.Count > 0 && CitySelected != null && CitySelected.Code != "009";
                    IsEnabledDetail = CitySelected != null && CitySelected.Code != "009";
                }
            }
        }

        //public List<Polygon> Coverages { get; set; }

        private string applicantCellPhone;
        public string ApplicantCellPhone
        {
            get { return applicantCellPhone; }
            set
            {
                if (applicantCellPhone != value)
                {
                    applicantCellPhone = value;
                    RaisePropertyChanged("ApplicantCellPhone");
                }
            }
        }

        private string patientCellPhone;
        public string PatientCellPhone
        {
            get { return patientCellPhone; }
            set
            {
                if (patientCellPhone != value)
                {
                    patientCellPhone = value;
                    RaisePropertyChanged("PatientCellPhone");
                }
            }
        }

        private string errorApplicantCellPhone;
        public string ErrorApplicantCellPhone
        {
            get { return errorApplicantCellPhone; }
            set
            {
                if (errorApplicantCellPhone != value)
                {
                    errorApplicantCellPhone = value;
                    RaisePropertyChanged("ErrorApplicantCellPhone");
                }
            }
        }

        private string errorPatientCellPhone;
        public string ErrorPatientCellPhone
        {
            get { return errorPatientCellPhone; }
            set
            {
                if (errorPatientCellPhone != value)
                {
                    errorPatientCellPhone = value;
                    RaisePropertyChanged("ErrorPatientCellPhone");
                }
            }
        }

        private ObservableCollection<ServicesEnabledViewModel> pediatricServices;
        public ObservableCollection<ServicesEnabledViewModel> PediatricServices
        {
            get { return pediatricServices; }
            set
            {
                pediatricServices = value;
                RaisePropertyChanged(nameof(PediatricServices));
            }
        }

        private string errorAddressSelected;
        public string ErrorAddressSelected
        {
            get { return errorAddressSelected; }
            set
            {
                if (errorAddressSelected != value)
                {
                    errorAddressSelected = value;
                    RaisePropertyChanged("ErrorAddressSelected");
                }
            }
        }

        public ObservableCollection<ClinicViewModel> Locations { get; set; }

        private async void LoadNeighborhoods()
        {
            dialogService.ShowProgress();
            RequestNeighborhoods request = new RequestNeighborhoods
            {
                CityCode = CitySelected.Code
            };
            ResponseNeighborhoods response = await apiService.GetNeighborhoods(request);
            ValidateResponseNeighborhoods(response);
            dialogService.HideProgress();
        }

        private async void ValidateResponseNeighborhoods(ResponseNeighborhoods response)
        {
            Neighborhoods = new ObservableCollection<Neighborhood>();

            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            foreach (Neighborhood item in response.Neighborhoods)
            {
                Neighborhoods.Add(item);
            }

            IsEnabledNeighborhood = Neighborhoods != null && Neighborhoods.Count > 0 && CitySelected != null && CitySelected.Code != "009";
        }

        private ObservableCollection<Neighborhood> neighborhoods;
        public ObservableCollection<Neighborhood> Neighborhoods
        {
            get { return neighborhoods; }
            set
            {
                if (neighborhoods != value)
                {
                    neighborhoods = value;
                    RaisePropertyChanged("Neighborhoods");
                }
            }
        }

        private Neighborhood neighborhoodSelected;
        public Neighborhood NeighborhoodSelected
        {
            get { return neighborhoodSelected; }
            set
            {
                if (neighborhoodSelected != value)
                {
                    neighborhoodSelected = value;
                    RaisePropertyChanged("NeighborhoodSelected");
                }
            }
        }

        private ObservableCollection<Models.Domain.Address> addresses;
        public ObservableCollection<Models.Domain.Address> Addresses
        {
            get { return addresses; }
            set
            {
                if (addresses != value)
                {
                    addresses = value;
                    RaisePropertyChanged("Addresses");
                }
            }
        }
        public Models.Domain.Address AddressCreated { get; set; }

        private Models.Domain.Address addressSelected;
        public Models.Domain.Address AddressSelected
        {
            get { return addressSelected; }
            set
            {
                if (addressSelected != value)
                {
                    addressSelected = value;
                    RaisePropertyChanged("AddressSelected");

                    if (value != null && value.Country != null)
                    {
                        
                        CreateFullLocation();
                    }
                    else
                    {
                        ServicesEnableds.Clear();
                    }
                }

                IsVisibleDetailAddress = AddressSelected != null;
            }
        }

        private bool isVisibleDetailAparment;
        public bool IsVisibleDetailAparment
        {
            get { return isVisibleDetailAparment; }
            set
            {
                if (isVisibleDetailAparment != value)
                {
                    isVisibleDetailAparment = value;
                    RaisePropertyChanged("IsVisibleDetailAparment");
                }
            }
        }

        private string fullCountry;
        public string FullCountry
        {
            get { return fullCountry; }
            set
            {
                if (fullCountry != value)
                {
                    fullCountry = value;
                    RaisePropertyChanged("FullCountry");
                }
            }
        }


        private AddressViewModel newAddress;
        public AddressViewModel NewAddress
        {
            get { return newAddress; }
            set
            {
                if (newAddress != value)
                {
                    newAddress = value;
                    RaisePropertyChanged("NewAddress");
                }
            }
        }

        private bool isVisibleDetailAddress;
        public bool IsVisibleDetailAddress
        {
            get { return isVisibleDetailAddress; }
            set
            {
                if (isVisibleDetailAddress != value)
                {
                    isVisibleDetailAddress = value;
                    RaisePropertyChanged("IsVisibleDetailAddress");
                }
            }
        }

        private ObservableCollection<Country> countries;
        public ObservableCollection<Country> Countries
        {
            get { return countries; }
            set
            {
                if (countries != value)
                {
                    countries = value;
                    RaisePropertyChanged("Countries");
                }
            }
        }

        private Country countrySelected;
        public Country CountrySelected
        {
            get { return countrySelected; }
            set
            {
                if (countrySelected != value)
                {
                    countrySelected = value;
                    RaisePropertyChanged("CountrySelected");
                }
                IsEnabledDepartments = CountrySelected != null && CountrySelected.Code != "-1" && CountrySelected.Code != "009";
            }
        }

        private bool isEnabledDepartments;
        public bool IsEnabledDepartments
        {
            get { return isEnabledDepartments; }
            set
            {
                if (isEnabledDepartments != value)
                {
                    isEnabledDepartments = value;
                    RaisePropertyChanged("IsEnabledDepartments");
                }
            }
        }

        private bool isEnabledCities;
        public bool IsEnabledCities
        {
            get { return isEnabledCities; }
            set
            {
                if (isEnabledCities != value)
                {
                    isEnabledCities = value;
                    RaisePropertyChanged("IsEnabledCities");
                }
            }
        }

        private bool isEnabledNeighborhood;
        public bool IsEnabledNeighborhood
        {
            get { return isEnabledNeighborhood; }
            set
            {
                if (isEnabledNeighborhood != value)
                {
                    isEnabledNeighborhood = value;
                    RaisePropertyChanged("IsEnabledNeighborhood");
                }
            }
        }

        private bool isEnabledDetail;
        public bool IsEnabledDetail
        {
            get { return isEnabledDetail; }
            set
            {
                isEnabledDetail = value;
                RaisePropertyChanged("IsEnabledDetail");
            }
        }

        private bool isVisibleMoreOptions;
        public bool IsVisibleMoreOptions
        {
            get { return isVisibleMoreOptions; }
            set
            {
                if (isVisibleMoreOptions != value)
                {
                    isVisibleMoreOptions = value;
                    RaisePropertyChanged("IsVisibleMoreOptions");
                }
            }
        }

        private bool isVisibleAddNewAddress;
        public bool IsVisibleAddNewAddress
        {
            get { return isVisibleAddNewAddress; }
            set
            {
                if (isVisibleAddNewAddress != value)
                {
                    isVisibleAddNewAddress = value;
                    RaisePropertyChanged("IsVisibleAddNewAddress");
                }
            }
        }

        private UrlWebViewSource htmlSource;
        public UrlWebViewSource HtmlSource
        {
            get { return htmlSource; }
            set
            {
                if (htmlSource != value)
                {
                    htmlSource = value;
                    RaisePropertyChanged("HtmlSource");
                }
            }
        }


        private string aditionlPhone;
        public string AditionlPhone
        {
            get { return aditionlPhone; }
            set
            {
                if (aditionlPhone != value)
                {
                    aditionlPhone = value;
                    RaisePropertyChanged("AditionlPhone");
                }
            }
        }

        private ObservableCollection<PediatricAgendas> agendas;
        public ObservableCollection<PediatricAgendas> Agendas
        {
            get { return agendas; }
            set
            {
                if (agendas != value)
                {
                    agendas = value;
                    RaisePropertyChanged(nameof(Agendas));

                }

            }
        }

        private DateTime earliestDate = DateTime.Now;
        public DateTime EarliestDate
        {
            get { return earliestDate; }
            set
            {
                earliestDate = value;
                RaisePropertyChanged(nameof(EarliestDate));
            }
        }

        private DateTime latestDate = DateTime.Now;
        public DateTime LatestDate
        {
            get { return latestDate; }
            set
            {
                latestDate = value;
                RaisePropertyChanged(nameof(LatestDate));
            }
        }

        private DateTime dateSelected = DateTime.Now;
        public DateTime DateSelected
        {
            get { return dateSelected; }
            set
            {
                if (DateSelected != value)
                {
                    dateSelected = value;
                    RaisePropertyChanged(nameof(DateSelected));
                    SearchAgenda();
                }

            }
        }

        private string aditionalEmail;
        public string AditionalEmail
        {
            get { return aditionalEmail; }
            set
            {
                if (aditionalEmail != value)
                {
                    aditionalEmail = value;
                    RaisePropertyChanged("AditionalEmail");
                }
            }
        }

        private string errorAditionlPhone;
        public string ErrorAditionlPhone
        {
            get { return errorAditionlPhone; }
            set
            {
                if (errorAditionlPhone != value)
                {
                    errorAditionlPhone = value;
                    RaisePropertyChanged("ErrorAditionlPhone");
                }
            }
        }

        private string errorAditionalEmail;
        public string ErrorAditionalEmail
        {
            get { return errorAditionalEmail; }
            set
            {
                if (errorAditionalEmail != value)
                {
                    errorAditionalEmail = value;
                    RaisePropertyChanged("ErrorAditionalEmail");
                }
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

        private bool isShowingUser;
        public bool IsShowingUser
        {
            get { return isShowingUser; }
            set
            {
                if (isShowingUser != value)
                {
                    isShowingUser = value;
                    RaisePropertyChanged("IsShowingUser");
                }
            }
        }

        public bool ReloadData { get; set; }

        public async void CreateFullLocation()
        {
            try
            {
                
                RequestNewCoverage request = new RequestNewCoverage
                {
                    Action = "GetCoverage",
                    Controller = "Coverage",
                    Address = string.IsNullOrWhiteSpace(AddressSelected.StandardizedAddress) ? "" : $"{AddressSelected.StandardizedAddress} {AddressSelected.DoorNumber} {AddressSelected.Bis}",
                    Municipality = string.IsNullOrWhiteSpace(AddressSelected.CityCode) ? "" : AddressSelected.CityCode,
                    List = false
                };
                dialogService.ShowProgress();
                var response = await apiService.GetNewCoverage(request);
                dialogService.HideProgress();
                if (response.Success && response.StatusCode !=-1)
                {
                    LoadServicesEnabled();
                    UrlMap = string.IsNullOrWhiteSpace(response.Coverage.Link) ? "https://www.google.com/" : response.Coverage.Link;
                    IsVisibleMap = response.Coverage.HasCoverage;
                    FullCountry = string.Format("{0} {1} {2} {3}", AddressSelected.Neighborhood, AddressSelected.City, AddressSelected.Department, AddressSelected.Country).Trim();
                    string[] ArrayFullCountry = FullCountry.Split(' ').ToArray();
                    FullCountry = ArrayFullCountry.Contains("Otro") ? "Sin Cobertura" : FullCountry;
                    IsVisibleDetailAparment = string.IsNullOrEmpty(AddressSelected.Apartment) == false && string.IsNullOrEmpty(AddressSelected.AddressDetails) == false;
                    
                }
                else
                {
                    
                    await dialogService.ShowMessage("", "Se ha producido un error al validar tu cobertura, por favor inténtalo más tarde.");
                }
            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }

        }



        private async void ValidateCoverageLatLong()
        {
            try
            {
                RequestCoverageLatLong request = new RequestCoverageLatLong
                {
                    Action = "ValidateCoverageLatLong",
                    Controller = "Coverage",
                    Latitude = CurrentLocation.Latitude.ToString().Replace(",", "."),
                    Longitude = currentLocation.Longitude.ToString().Replace(",", ".")
                };
                var response = await apiService.GetCoverageLatLong(request);
                if (response.Success)
                {
                    UrlMap = response.Coverage.Link;
                    IsVisibleMap = true;
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private async void LoadCities()
        {
            dialogService.ShowProgress();
            RequestCities request = new RequestCities
            {
                DepartamentCode = DepartamentSelected.Code
            };
            ResponseCities response = await apiService.GetCities(request);
            ValidateResponseCities(response);
            dialogService.HideProgress();
        }

        private async void ValidateResponseCities(ResponseCities response)
        {
            Cities = new ObservableCollection<City>();
            Neighborhoods = new ObservableCollection<Neighborhood>();

            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            foreach (City item in response.Cities)
            {
                Cities.Add(item);
            }
        }

        private async void LoadAddresses()
        {
            dialogService.ShowProgress();
            //RequestCoverage request = new RequestCoverage
            //{
            //    Code = AppConfigurations.CoverageCode
            //};
            //ResponseCoverage response = await apiService.GetCoverage(request);
            //ValidateResponseCoverage(response);

            RequestLocationInformationService requestLocation = new RequestLocationInformationService
            {
                AppliantDocument = loginViewModel.User.Document,
                AppliantDocumentType = loginViewModel.User.DocumentType,
                PatientDocument = PersonSelected.Document,
                PatientDocumentType = PersonSelected.DocumentType,
            };

            ResponseLocationInformationService responseLocation = await apiService.GetLocationInformationService(requestLocation);
            ValidateResponseLocationInformationService(responseLocation);
            dialogService.HideProgress();
        }

        private async void ValidateResponseLocationInformationService(ResponseLocationInformationService responseLocation)
        {
            if (await validatorService.ValidateResponse(responseLocation) == false)
            {
                return;
            }

            dialogService.ShowProgress();

            Addresses = new ObservableCollection<Models.Domain.Address>();

            if (responseLocation.AddressesPhonesService != null && responseLocation.AddressesPhonesService.ContactPhoneService != null)
            {
                ApplicantCellPhone = responseLocation.AddressesPhonesService.ContactPhoneService.ApplicantCellPhone;
                PatientCellPhone = responseLocation.AddressesPhonesService.ContactPhoneService.PatientCellPhone;
            }

            Countries.Clear();

            if (responseLocation.AddressesPhonesService != null && responseLocation.AddressesPhonesService.Countries != null)
            {

                foreach (Country item in responseLocation.AddressesPhonesService.Countries)
                {
                    Countries.Add(item);
                }
            }

            IsToggledPhoneNumber = true;

            foreach (Models.Domain.Address item in responseLocation.AddressesPhonesService.Addresses)
            {
                if (!string.IsNullOrWhiteSpace(item.StandardizedAddress))
                    Addresses.Add(item);
            }

            IsShowingUser = geolocatorService.IsGeolocationAvailable && geolocatorService.IsGeolocationEnabled;

            Position position = await geolocatorService.GetPositionAsync();

            CurrentLocation = new LocationViewModel { Latitude = position.Latitude, Longitude = position.Longitude };

            dialogService.HideProgress();
        }

        //private async void ValidateResponseCoverage(ResponseCoverage response)
        //{
        //    if (await validatorService.ValidateResponse(response) == false)
        //    {
        //        return;
        //    }

        //    Coverages = new List<Polygon>();

        //    foreach (Polygon Polygon in response.Coverages)
        //    {
        //        ;
        //        Polygon polygon = new Polygon();
        //        polygon.CoverageCode = Polygon.CoverageCode;

        //        foreach (Point item in Polygon.Points.OrderBy(x => x.Position).ToList())
        //        {
        //            Point position = new Point(
        //                double.Parse(item.Latitude.ToString().Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)),
        //                double.Parse(item.Longitude.ToString().Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator))
        //                );
        //            polygon.Points.Add(position);
        //        }
        //        Coverages.Add(polygon);
        //    }
        //}

        private async void ValidateIsYoung()
        {
            dialogService.ShowProgress();
            RequestValidateUserYoung request = new RequestValidateUserYoung
            {
                Document = PersonSelected.Document,
                DocumentType = PersonSelected.DocumentType,
            };
            ResponseValidateUserYoung response = await apiService.ValidateUserYoung(request);
            dialogService.HideProgress();
            ValidateResponseValidateUserYoung(response);
        }

        private async void ValidateResponseValidateUserYoung(ResponseValidateUserYoung response)
        {
            UserYoung = new UserYoung();

            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            UserYoung.resultUserYoung = response.resultUserYoung;

            LoadAddresses();
        }

        private async void LoadServicesEnabled()
        {
            
            RequestServicesEnabled request = new RequestServicesEnabled
            {
                Document = PersonSelected.Document,
                DocumentType = PersonSelected.DocumentType,
                Latitude = AddressSelected.Latitude.Replace(",", "."),
                Longitude = AddressSelected.Longitude.Replace(",", ".")
            };
            dialogService.ShowProgress();
            ResponseServicesEnabled response = await apiService.GetServicesEnabled(request);
            dialogService.HideProgress();
            await ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>().LoadClinincs();
            ValidateResponseServicesEnabled(response);
        }

        private async void ValidateResponseServicesEnabled(ResponseServicesEnabled response)
        {
            LoadPediatricServices();
            ServicesEnableds = new ObservableCollection<ServicesEnabledViewModel>();
            var Clinics = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>().Clinics;

            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            if (addressSelected != null && string.IsNullOrEmpty(addressSelected.Country) == false
                && addressSelected.Country.Equals("Otro"))
            {
                response.EnabledServices = response.EnabledServices.Where(x => x.Code != "02").ToList();
            }

            foreach (EnabledService service in response.EnabledServices)
            {
                if (service.Code.Equals("08") && Clinics.Count>0)
                {
                    service.EstimatedTime = UserYoung.resultUserYoung ? Clinics.First().PediatricTime : Clinics.First().AdultTime;
                }
                ServicesEnabledViewModel servicesEnabledViewModel = new ServicesEnabledViewModel();
                ViewModelHelper.SetServiceEnabledToServiceEnabledViewModel(service, servicesEnabledViewModel);
                
                ServicesEnableds.Add(servicesEnabledViewModel);
            }
        }

        public ICommand InformationCommand { get { return new RelayCommand<string>(Information); } }
        public ICommand CheckCoverageCommand { get { return new RelayCommand(CheckNewCoverage); } }
        public ICommand OptionsCommand { get { return new RelayCommand(AddNewAddress); } }
        public ICommand ConfirmateServiceCommand { get { return new RelayCommand(ConfirmateService); } }
        public ICommand CancelNewAddressCommand { get { return new RelayCommand(CancelNewAddress); } }
        public ICommand CancelChatCommand { get { return new RelayCommand(CancelChat); } }
        public ICommand ExitChatCommand { get { return new RelayCommand(ExitChat); } }
        public ICommand AditionalDataContinueCommand { get { return new RelayCommand(AditionalDataContinue); } }
        public ICommand BackCommand { get { return new RelayCommand(Back); } }
        public ICommand CallCategoryCommand { get { return new RelayCommand(CallCategory); } }
        public ICommand ConfirmAddressCommand { get { return new RelayCommand(SaveAddress); } }
        public ICommand ShopOnlineCommand { get { return new RelayCommand(ShopOnline); } }
        public ICommand AdvanceLocationCommand { get { return new RelayCommand(AdvanceLocation); } }
        public ICommand ScheduleCommand { get { return new RelayCommand<PediatricAgendas>(Schedule); } }
        public ICommand NavigatingCommand { get { return new RelayCommand(() => IsNavigating = true); } }
        public ICommand NavigatedCommand { get { return new RelayCommand(() => IsNavigating = false); } }
        public ICommand CloseVCCommand { get { return new RelayCommand(async () => await navigationService.Navigate(AppPages.LandingPage)); } }

        private async void Schedule(PediatricAgendas agenda)
        {
            try
            {
                ErrorPediatricPhone = ValidatorHelper.IsValidCellPhone(PediatricPhone) ? string.Empty : "Por favor ingrese un número de contacto.";
                if (string.IsNullOrEmpty(ErrorPediatricPhone))
                {
                    if (await dialogService.ShowConfirm("Confirmación", $"¿Estás seguro que deseas seleccionar la cita para el {agenda.Day} {agenda.Date}, a las {agenda.Time}?"))
                    {
                        dialogService.ShowProgress();
                        var request = new RequestPediatricAttention
                        {
                            Action = "CreateAttention",
                            AddressPetition = new PediatricAddressPetition
                            {
                                Apto = AddressSelected.Apartment,
                                Bis = AddressSelected.Bis,
                                City = AddressSelected.CityCode,
                                Corner = AddressSelected.Corner,
                                Country = AddressSelected.Country,
                                Department = AddressSelected.DepartmentCode,
                                Latitude = AddressSelected.Latitude,
                                longitude = AddressSelected.Longitude,
                                Neighborhood = AddressSelected.NeighborhoodCode,
                                NumberStreet = AddressSelected.DoorNumber,
                                Reference = AddressSelected.AddressDetails,
                                Street = AddressSelected.StandardizedAddress,
                                CoverageZona = AddressSelected.Coverage
                            },
                            Phone = PediatricPhone,
                            Classification = "33",
                            Controller = "Services",
                            Document = PersonSelected.Document,
                            DocumentType = personSelected.DocumentType,
                            IdSchedule = agenda.IdAgenda,
                            ProgramedDate = $"{agenda.Date} {agenda.Time}"
                        };

                        var response = await apiService.CreatePediatricPetition(request);
                        dialogService.HideProgress();
                        if (response.Success && response.CreateAttentionResult != null && response.StatusCode != -1)
                        {
                            await dialogService.ShowMessage("", response.CreateAttentionResult.Message);
                            if (response.CreateAttentionResult.Success)
                                await navigationService.Navigate(AppPages.LandingPage);
                        }
                        else
                        {
                            await dialogService.ShowMessage(response.Title, response.Message);
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private void SearchAgenda()
        {
            string date = DateSelected.ToString("dd/MM/yyy");
            Agendas = new ObservableCollection<PediatricAgendas>(AgendasSaved.Where(x => x.Date.Contains(date)));
        }

        private async void AdvanceLocation()
        {
            await navigationService.Navigate(AppPages.AdvanceLocationPage);
        }

        private void ShopOnline()
        {
            phoneService.OpenUrl("https://shop.ucm.com.uy/");
        }

        private void CallCategory()
        {
            ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
            callViewModel.CallCategory();
        }

        private async void Back()
        {
            await navigationService.Back();
        }

        private async void AditionalDataContinue()
        {
            ValidateAditionalData();
            if (string.IsNullOrEmpty(ErrorAditionalEmail) && string.IsNullOrEmpty(ErrorAditionlPhone))
            {
                PersonSelected.CellPhone = AditionlPhone;
                PersonSelected.Email = AditionalEmail;
                PatientCellPhone = AditionlPhone;
                await navigationService.Navigate(AppPages.MedicalCenterCoordinationPage);
            }
        }

        private void ValidateAditionalData()
        {
            ErrorAditionalEmail = string.IsNullOrEmpty(AditionalEmail) ? AppResources.MailRequired : string.Empty;
            ErrorAditionlPhone = string.IsNullOrEmpty(AditionlPhone) ? AppResources.CellPhoneRequired : string.Empty;

            if (string.IsNullOrEmpty(ErrorAditionalEmail))
            {
                ErrorAditionalEmail = ValidatorHelper.IsValidEmail(AditionalEmail) ? string.Empty : AppResources.EmailCondition;
            }

            if (string.IsNullOrEmpty(ErrorAditionlPhone))
            {
                ErrorAditionlPhone = ValidatorHelper.IsValidCellPhone(AditionlPhone) ? string.Empty : AppResources.CellPhoneConditions;
            }
        }

        private async void ExitChat()
        {
            if (await dialogService.ShowConfirm(AppResources.TitleRequestRervice, AppResources.ExitChat))
            {
                await navigationService.ClosedModal();
            }
        }

        private async void CancelChat()
        {
            if (await dialogService.ShowConfirm(AppResources.TitleRequestRervice, AppResources.CancelChat))
            {
                await navigationService.ClosedModal();
                await LoadPersons();
            }
        }

        private async void ValidateVideoChat(RequestMedicalService request)
        {
            ServiceLocator.Current.GetInstance<IPrecallViewModel>().LoadRequestCall(request, requestMedicalCall);

            await navigationService.Navigate(AppPages.Precall);
        }

        private void CreateNewAddress()
        {
            string standardizedAddress = NewAddress.Direction;

            if (string.IsNullOrEmpty(standardizedAddress))
            {
                standardizedAddress = CountrySelected.Name;
                if (DepartamentSelected != null)
                {
                    standardizedAddress += $", {DepartamentSelected.Name}";

                    if (CitySelected != null)
                    {
                        standardizedAddress += $", {CitySelected.Name}";
                    }
                }
            }

            AddressCreated = new Models.Domain.Address
            {
                AddressDetails = NewAddress.AddressDetails,
                Apartment = NewAddress.Apartment,
                Bis = NewAddress.Bis,
                City = CitySelected != null ? CitySelected.Name : string.Empty,
                CityCode = CitySelected != null ? CitySelected.Code : string.Empty,
                Corner = NewAddress.Corner,
                Country = CountrySelected != null ? CountrySelected.Name : AppConfigurations.Country,
                Coverage = true,
                Department = DepartamentSelected != null ? DepartamentSelected.Name : string.Empty,
                DepartmentCode = DepartamentSelected != null ? DepartamentSelected.Code : string.Empty,
                Direction = NewAddress.Direction,
                DoorNumber = NewAddress.DoorNumber,
                IsNewAdress = true,
                Latitude = NewAddress.Latitude.Replace(',', '.'),
                Longitude = NewAddress.Longitude.Replace(',', '.'),
                Neighborhood = NeighborhoodSelected != null ? NeighborhoodSelected.Name : string.Empty,
                NeighborhoodCode = NeighborhoodSelected != null ? NeighborhoodSelected.Code : "1000",
                StandardizedAddress = NewAddress.Street,
                Street = NewAddress.Street ?? string.Empty,
                StreetSO = NewAddress.StreetSO ?? string.Empty,
                NumberApto = NewAddress.NumberApto
            };
        }

        private async void CancelNewAddress()
        {
            AddressSelected = null;
            NewAddress = new AddressViewModel();
            //Direction = string.Empty;
            ConfirmAddress = false;

            await navigationService.Back();
        }

        RequestMedicalCallOpentok requestMedicalCall = new RequestMedicalCallOpentok();

        public async void LoadAgendas()
        {
            try
            {
                dialogService.ShowProgress();
                PediatricPhone = loginViewModel.User.CellPhone;
                var request = new RequestPediatricAgendas
                {
                    IdClasification = 33,
                    Controller = "Services",
                    Action = "PediatricAgendas"
                };
                var response = await apiService.GetPediatricAgendas(request);
                if (response.Success && response?.Agendas?.Count > 0)
                {
                    Agendas = new ObservableCollection<PediatricAgendas>(response.Agendas);
                    EarliestDate = Agendas.Min(x => DateTime.ParseExact(x.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    LatestDate = Agendas.Max(x => DateTime.ParseExact(x.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                    AgendasSaved = Agendas;
                    dialogService.HideProgress();
                    await navigationService.Navigate(AppPages.SchedulePediatricPage);
                }
                else
                {
                    dialogService.HideProgress();
                    await dialogService.ShowMessage(response.Title, response.Message);
                }

            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private async void ConfirmateService()
        {
            ValidateData();

            if (string.IsNullOrEmpty(ErrorAddressSelected) == false)
            {
                await dialogService.ShowMessage(AppResources.TitleRequestRervice, ErrorAddressSelected);
                return;
            }

            if (string.IsNullOrEmpty(ErrorApplicantCellPhone) == false || string.IsNullOrEmpty(ErrorPatientCellPhone) == false)
            {
                await dialogService.ShowMessage(AppResources.TitleRequestRervice, AppResources.CompleteData);
                return;
            }

            dialogService.ShowProgress();

            IsInCoverage = AddressSelected.Coverage || addressSelected.IsNewAdress && IsInCoverage;

            RequestMedicalService request = new RequestMedicalService
            {
                Address = IsUY ? AddressSelected.Street : AddressSelected.Direction,
                AddressDetails = AddressSelected.AddressDetails,
                Apartment = AddressSelected.Apartment,
                AppliantDocument = loginViewModel.User.Document,
                AppliantDocumentType = loginViewModel.User.DocumentType,
                AppliantName = PersonSelected.FullNames,
                PatientFullNames = PersonSelected.FullNames,
                ApplicantCellPhone = ApplicantCellPhone,
                Bis = AddressSelected.Bis,
                City = AddressSelected.CityCode,
                Corner = AddressSelected.Corner,
                Country = AddressSelected.Country,
                Coverage = AddressSelected.Coverage.ToString(),
                Department = AddressSelected.Department,
                DoorNumber = AddressSelected.DoorNumber,
                Latitude = AddressSelected.Latitude,
                Longitude = AddressSelected.Longitude,
                Neighborhood = AddressSelected.NeighborhoodCode,
                PatientCellPhone = PatientCellPhone,
                PatientDocument = PersonSelected.Document,
                PatientDocumentType = PersonSelected.DocumentType,
                ServiceType = ((int)ServiceSelected.ServiceType).ToString(),
                Street = AddressSelected.Street ?? string.Empty,
                StreetSO = AddressSelected.Street ?? string.Empty,
                AffiliateUserName = loginViewModel.User.UserName
            };
            ServiceLocator.Current.GetInstance<IMedicalVideoCallViewModel>().UserName = PersonSelected.FullNames;
            requestMedicalCall.info = request;
            ResponseMedicalService response = new ResponseMedicalService();



            switch (ServiceSelected.ServiceType)
            {

                
                    //request.Action = AppConfigurations.MedicalOrientation;
                    //response = await apiService.MedicalOrientation(request);
                    //if (ServiceSelected.ServiceType != ServiceType.VideoCall)
                    //{
                    //    ValidateResponseMedicalService(response);
                    //}
                    //break;
                //request.Action = AppConfigurations.HomeHealthCare;
                //response = await apiService.HomeHealthCare(request);
                //break;
                case ServiceType.MedicalOrientationCall:
                case ServiceType.HomeCare:
                case ServiceType.VideoCall:
                case ServiceType.PediatricVideoCall:
                case ServiceType.MedicalOrientationChat:
                    NewVideocall();
                    break;
            }


        }

        private void LoadPediatricServices(string time = "")
        {
            try
            {
                PediatricServices = new ObservableCollection<ServicesEnabledViewModel>
                {
                    new ServicesEnabledViewModel
                    {
                        ServiceType = ServiceType.PediatricVideoCall,
                        Name = "Videoconsulta de Orientación Pediátrica Prioritaria",
                        Icon = "IconWatchP.png",
                        EstimatedTime = time
                    },
                    new ServicesEnabledViewModel
                    {
                        ServiceType = ServiceType.SchedulePediatricVideoCall,
                        Name = "Videoconsulta de Orientación Pediátrica Programada",
                        Icon = "IconCalendarP.png"
                    }
                };
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private string ValidateServiceType()
        { 
            if (ServiceSelected.ServiceType == ServiceType.PediatricVideoCall)
                return "14";
            if (UserYoung.resultUserYoung && ServiceSelected.ServiceType == ServiceType.MedicalOrientationChat)
                return "29";
            return ((int)ServiceSelected.ServiceType).ToString();
        }

        private async void NewVideocall()
        {
            try
            {
                dialogService.ShowProgress();
                var response = new ResponseNewVideocall();

                var request = new RequestNewVideoCall
                {
                    Type = ValidateServiceType(),
                    Document = personSelected.Document,
                    DocumentType = personSelected.DocumentType,
                    Country = AddressSelected.Country,
                    Department = AddressSelected.DepartmentCode,
                    City = AddressSelected.CityCode,
                    Neighborhood = AddressSelected.NeighborhoodCode,
                    Street = AddressSelected.StandardizedAddress,
                    NumberStreet = AddressSelected.DoorNumber,
                    Bis = AddressSelected.Bis,
                    Apto = AddressSelected.Apartment,
                    Corner = AddressSelected.Corner,
                    Latitude = AddressSelected.Latitude,
                    Longitude = AddressSelected.Longitude,
                    Controller = "VideoCall",
                    Cellphone = ApplicantCellPhone,
                    Action = "Petition",
                    Reference = AddressSelected.AddressDetails,
                    CoverageZona = AddressSelected.Coverage
                };
                response = await apiService.NewVideocallPetition(request);
                dialogService.HideProgress();
                if (response.Success && response.StatusCode != -1)
                {

                    if (response.Petition.Return != null)
                    {
                        switch (ServiceSelected.ServiceType)
                        {
                            case ServiceType.MedicalOrientationCall:
                                await dialogService.AlertIcon("", response.Petition.Message);
                                break;


                            case ServiceType.VideoCall:
                            case ServiceType.PediatricVideoCall:
                                await Launcher.OpenAsync(response.Petition.Return.Url);
                                break;

                            case ServiceType.HomeCare:
                            case ServiceType.MedicalOrientationChat:
                                UrlChat = response.Petition.Return.Url;
                                await navigationService.ShowModal(AppPages.ChatMedicalServicePage);
                                break;
                        }

                        
                        await navigationService.Navigate(AppPages.LandingPage);
                    }
                    else
                    {
                        await dialogService.ShowMessage("", response.Petition.Message);
                    }
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }


            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private async void ValidateResponseMedicalService(ResponseMedicalService response)
        {
            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            if (response.ChatAgent.HasChat)
            {

                if (ServiceSelected.ServiceType == ServiceType.HomeCare)
                {
                    await dialogService.ShowMessage(!UserYoung.resultUserYoung ? ServiceSelected.Name : $"{ServiceSelected.Name} pediátrica", "Por favor espera a que se abra el chat para darnos la información médica del paciente pues de lo contrario no podremos tomar el servicio que solicitas.");
                }

                phoneService.DeleteCookie();
                string url = string.Format("{0}?name={1}", response.ChatAgent.UrlChatApp, string.Format("{0}({1}-{2})", PersonSelected.FullNames, PersonSelected.DocumentTypeShort, PersonSelected.Document));

                if (string.IsNullOrEmpty(PersonSelected.Email) == false)
                {
                    url += $"&email={PersonSelected.Email}";
                }

                if (isToggledPhoneNumber == false)
                {
                    if (string.IsNullOrEmpty(PatientCellPhone) == false || string.IsNullOrEmpty(PatientCellPhone) == false)
                    {
                        url += $"&phone={PatientCellPhone}";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(ApplicantCellPhone) == false || string.IsNullOrEmpty(ApplicantCellPhone) == false)
                    {
                        url += $"&phone={ApplicantCellPhone}";
                    }
                }

                url += $"&urlService={AppConfigurations.UrlBaseMiddleware}/{AppConfigurations.ServicesController}/{AppConfigurations.ServiceChatAgent}";
                url += $"&urlServiceApiKey={AppConfigurations.Subscriptionkey}";
                url += $"&serviceType={(int)serviceSelected.ServiceType}";
                url += string.Format("&coverage={0}", IsInCoverage ? "1" : "2");
                url += $"&environment={AppConfigurations.Environment}";

                byte[] utf8bytes = Encoding.UTF8.GetBytes(url);
                string r = Encoding.UTF8.GetString(utf8bytes, 0, utf8bytes.Length);

                HtmlSource.Url = r;

                dialogService.HideProgress();
                await navigationService.BackToRoot();
                await navigationService.ShowModal(AppPages.ChatMedicalServicePage);
                return;
            }

            dialogService.HideProgress();
            await dialogService.ShowMessage(response.Title, response.Message);
            await navigationService.BackToRoot();
        }

        private void ValidateData()
        {
            if (string.IsNullOrEmpty(ApplicantCellPhone))
            {
                ErrorApplicantCellPhone = AppResources.CellPhoneRequired;
            }
            else
            {
                ErrorApplicantCellPhone = ValidatorHelper.IsValidCellPhone(ApplicantCellPhone) ? string.Empty : AppResources.CellPhoneConditions;
            }

            if (isToggledPhoneNumber == false)
            {
                if (string.IsNullOrEmpty(PatientCellPhone))
                {
                    ErrorPatientCellPhone = AppResources.CellPhoneRequired;
                }

                if (string.IsNullOrEmpty(PatientCellPhone) == false)
                {
                    ErrorPatientCellPhone = ValidatorHelper.IsValidCellPhone(PatientCellPhone) ? string.Empty : AppResources.CellPhoneConditions;
                }
            }
        }

        private string AddressSaveSucesfull()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (Device.RuntimePlatform == Device.iOS)
            {
                stringBuilder.AppendLine();
            }

            stringBuilder.Append($"{NewAddress.StandardizedAddress}");

            if (string.IsNullOrEmpty(NewAddress.DoorNumber) == false)
            {
                stringBuilder.Append($" {NewAddress.DoorNumber}");
            }

            if (string.IsNullOrEmpty(NewAddress.Bis) == false)
            {
                stringBuilder.Append($" {NewAddress.Bis}");
            }

            if (string.IsNullOrEmpty(NewAddress.Corner) == false)
            {
                stringBuilder.Append($" {NewAddress.Corner}");
            }

            stringBuilder.Append("\n");

            if (string.IsNullOrEmpty(NewAddress.NumberApto) == false)
            {
                stringBuilder.AppendLine($"Apartamento {NewAddress.NumberApto} ");
            }

            if (NeighborhoodSelected != null && string.IsNullOrEmpty(NeighborhoodSelected.Name) == false)
            {

                stringBuilder.AppendLine($"{NeighborhoodSelected.Name} ");
            }

            if (CitySelected != null && string.IsNullOrEmpty(CitySelected.Name) == false)
            {
                stringBuilder.AppendLine($"{CitySelected.Name} ");
            }

            if (DepartamentSelected != null && string.IsNullOrEmpty(DepartamentSelected.Name) == false)
            {
                stringBuilder.AppendLine($"{DepartamentSelected.Name} ");
            }

            if (CountrySelected != null && string.IsNullOrEmpty(CountrySelected.Name) == false)
            {
                stringBuilder.AppendLine($"{CountrySelected.Name}");
            }

            return stringBuilder.ToString();
        }



        public void OtherText()
        {
            try
            {
                if (CountrySelected?.Code == "-1")
                {
                    OtherSelected = "Seleccionaste país Otro";
                    return;
                }
                if (DepartamentSelected?.Code?.Equals("009") == true)
                {
                    OtherSelected = "Seleccionaste departamento Otro";
                    return;
                }
                if (CitySelected?.Code?.Equals("009") == true)
                {
                    OtherSelected = "Seleccionaste localidad Otro";
                    return;
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }


        }
        public void OtherTextCard()
        {
            try
            {
                OtherSelected = string.Empty;
                if (AddressSelected.Country.ToLower().Equals("Otro".ToLower()))
                {
                    OtherSelected = "Seleccionaste país Otro";
                    return;
                }
                if (AddressSelected?.DepartmentCode?.Equals("009") == true)
                {
                    OtherSelected = "Seleccionaste departamento Otro";
                    return;
                }
                if (AddressSelected?.CityCode?.Equals("009") == true)
                {
                    OtherSelected = "Seleccionaste localidad Otro";
                    return;
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }

        }

        private async void CheckNewCoverage()
        {
            try
            {
                if (ValidateNewAddress())
                {
                    dialogService.ShowProgress();
                    RequestNewCoverage request = new RequestNewCoverage
                    {
                        Action = "GetCoverage",
                        Controller = "Coverage",
                        Address = $"{NewAddress.Street} {NewAddress.DoorNumber} {NewAddress.Bis}",
                        Municipality = CitySelected == null ? "" : CitySelected.Code,
                        List = false
                    };

                    var response = await apiService.GetNewCoverage(request);
                    if (response.Success&& response.StatusCode!=-1)
                    {
                        dialogService.HideProgress();
                        IsInCoverage = response.Coverage.HasCoverage;

                        NewAddress.StandardizedAddress = NewAddress.Street;
                        NewAddress.Latitude = response.Coverage.HasCoverage ? response.Coverage.Latitude : "0";
                        NewAddress.Longitude = response.Coverage.HasCoverage ? response.Coverage.Longitude : "0";
                        bool addAddress = true;

                        if (response.Coverage != null && response.Coverage.HasCoverage == false)
                        {
                            addAddress = await dialogService.ShowConfirm(AppResources.TitleRequestRervice, "La ubicación que estas ingresando está fuera de nuestra zona de cobertura. \n ¿Deseas continuar?");
                        }

                        if (addAddress)
                        {
                            CreateNewAddress();
                            AddressSelected = AddressCreated;
                            string address = $"{(CitySelected != null ? CitySelected.Name : string.Empty)} {(DepartamentSelected != null ? DepartamentSelected.Name : string.Empty)}";
                            ConfirmAddress = true;
                            if (CountrySelected.Code != "-1" && DepartamentSelected.Code != "009" && CitySelected.Code != "009")
                            {
                                await dialogService.AlertIcon("", "Dirección guardada con éxito \nPuedes validar tu dirección en el mapa y proceder a confirmar");
                            }
                            else
                            {
                                OtherText();
                            }
                            UrlMap = string.IsNullOrWhiteSpace(response.Coverage.Link) ? "https://www.google.com/" : response.Coverage.Link;
                            IsVisibleMap = response.Coverage.HasCoverage;
                        }
                    }
                    else
                    {
                        dialogService.HideProgress();
                        await dialogService.ShowMessage("", "Se ha producido un error al validar tu cobertura, por favor inténtalo más tarde.");
                    }
                }
                else
                {
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Por favor completa los datos");
                }

            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        //private async void CheckCoverage()
        //{
        //    if (ValidateNewAddress())
        //    {
        //        dialogService.ShowProgress();
        //        RequestValidateCoverage request = new RequestValidateCoverage
        //        {
        //            Code = AppConfigurations.CoverageCode,
        //            Address = string.Format("{0}, {1}, {2}",
        //                                    IsUY ? NewAddress.Street : NewAddress.Direction,
        //                                    CitySelected != null ? CitySelected.Name : string.Empty,
        //                                    DepartamentSelected != null ? DepartamentSelected.Name : string.Empty)
        //        };

        //        ResponseValidateCoverage response = await apiService.ValidateCoverage(request);
        //        response.NameLocation = IsUY ? NewAddress.Street : NewAddress.Direction;
        //        dialogService.HideProgress();

        //        ValidateResponseValidateCoverage(response);

        //        if (response.Coverage != null)
        //        {
        //            NewAddress.Latitude = response.Coverage.Latitude.ToString();
        //            NewAddress.Longitude = response.Coverage.Longitude.ToString();
        //            NewAddress.StandardizedAddress = response.NameLocation;
        //            IsInCoverage = response.Coverage.InCoverage;

        //            double.TryParse(NewAddress.Latitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator), out double latitude);
        //            double.TryParse(NewAddress.Longitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator), out double longitude);

        //            bool addAddress = true;

        //            if (response.Coverage != null && response.Coverage.InCoverage == false)
        //            {
        //                addAddress = await dialogService.ShowConfirm(AppResources.TitleRequestRervice, "La ubicación que estas ingresando está fuera de nuestra zona de cobertura. \n ¿Deseas continuar?");
        //            }

        //            if (addAddress)
        //            {
        //                CreateNewAddress();
        //                AddressSelected = AddressCreated;
        //                string address = $"{(CitySelected != null ? CitySelected.Name : string.Empty)} {(DepartamentSelected != null ? DepartamentSelected.Name : string.Empty)}";
        //                AddressLocation = new LocationViewModel { Latitude = latitude, Longitude = longitude, Address = address, Name = NewAddress.StandardizedAddress ?? " " };
        //                ConfirmAddress = true;
        //                await dialogService.AlertIcon("", "Dirección guardada con éxito \nPuedes validar tu dirección en el mapa y proceder a confirmar");
        //            }
        //        }            
        //        return;
        //    }

        //    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Por favor completa los datos");
        //}

        //private async void ValidateResponseValidateCoverage(ResponseValidateCoverage response)
        //{
        //    Locations = new ObservableCollection<ClinicViewModel>();

        //    if (await validatorService.ValidateResponse(response) == false)
        //    {
        //        return;
        //    }

        //    if (response.Coverage != null)
        //    {
        //        ClinicViewModel location = new ClinicViewModel();
        //        location.Icon = "gps";
        //        location.Name = response.NameLocation;
        //        location.Latitude = response.Coverage.Latitude;
        //        location.Longitude = response.Coverage.Longitude;
        //        location.HasInteraction = false;

        //        if (AddressSelected == null)
        //        {
        //            AddressSelected = new Models.Domain.Address();
        //        }

        //        AddressSelected.Latitude = response.Coverage.Latitude.ToString();
        //        AddressSelected.Longitude = response.Coverage.Longitude.ToString();
        //        AddressSelected.Coverage = response.Coverage.InCoverage;

        //        AddressLocation = new LocationViewModel() { Latitude = response.Coverage.Latitude, Longitude = response.Coverage.Longitude, Name = (AddressSelected.StandardizedAddress ?? location.Name) ?? " " };

        //        Locations.Add(location);
        //    }
        //}

        private async void SaveAddress()
        {
            var ad = string.Format("{0}, {1}, {2}",
                                            IsUY ? NewAddress.Street : NewAddress.Direction,
                                            CitySelected != null ? CitySelected.Name : string.Empty,
                                            DepartamentSelected != null ? DepartamentSelected.Name : string.Empty);
            AddressSelected = AddressCreated;
            var com = string.Format("{0}, {1}, {2}", AddressSelected.Street, AddressSelected.City, AddressSelected.Department);

            if (ad.ToLower().Trim().Equals(com.ToLower().Trim()))
            {
                Addresses.Add(AddressSelected);
                NewAddressAdded = true;
                await dialogService.AlertIcon("Dirección guardada con éxito.", AddressSaveSucesfull());
                await navigationService.Back();
            }
            else
            {
                CheckNewCoverage();

            }
            ConfirmAddress = false;
        }

        private bool ValidateNewAddress()
        {
            ClearErrorsNewAddress();

            NewAddress.ErrorCountry = CountrySelected == null ? "El país es obligatorio" : string.Empty;

            if (CountrySelected != null && CountrySelected.Code != "-1" && CountrySelected.Code != "009")
            {
                NewAddress.ErrorDepartment = DepartamentSelected == null ? AppResources.DepartmentRequired : string.Empty;
                if (DepartamentSelected != null && DepartamentSelected.Code != "009")
                {
                    NewAddress.ErrorCity = CitySelected == null ? string.Format(AppResources.CityRequired, "ciudad") : string.Empty;
                    if (CitySelected != null && CitySelected.Code != "009")
                    {
                        NewAddress.ErrorStreet = string.IsNullOrEmpty(NewAddress.Street) ? AppResources.StreetRequired : string.Empty;
                    }
                }
            }

            return string.IsNullOrEmpty(NewAddress.ErrorCountry) && string.IsNullOrEmpty(NewAddress.ErrorDepartment) && string.IsNullOrEmpty(NewAddress.ErrorDirection) && string.IsNullOrEmpty(NewAddress.ErrorCity) && string.IsNullOrEmpty(NewAddress.ErrorNeighborhood) && string.IsNullOrEmpty(NewAddress.ErrorMainRoadType) && string.IsNullOrEmpty(NewAddress.ErrorStreet);
        }

        private void ClearErrorsNewAddress()
        {
            NewAddress.ErrorCountry =
            NewAddress.ErrorDepartment =
            NewAddress.ErrorDirection =
            NewAddress.ErrorCity =
            NewAddress.ErrorNeighborhood =
            NewAddress.ErrorMainRoadType =
            NewAddress.ErrorStreet = string.Empty;
        }

        private async void Information(string option)
        {
            switch (option)
            {
                case "1":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, AppResources.AddFamiliyServicePage);
                    break;
                case "2":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, AppResources.CoverageMessage);
                    break;
                case "3":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, AppResources.CellPhoneConditions);
                    break;
                case "4":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, AppResources.EmailCondition);
                    break;
                case "5":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Una vía puede ser clasificada de acuerdo a su orientación y diseño. Ejm: SUR, ESTE, OESTE, NORTE.");
                    break;
                case "6":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Valor numérico o nombre común que identifica la vía, en este caso la vía principal, por lo general las avenidas o vías principales tienen asociado un nombre común.");
                    break;
                case "7":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Campo alfabético, sirve para diferenciar las vías internas, generalmente siguen un orden lógico, ya sea alfabético o numérico o sea combinación de ambas.");
                    break;
                case "8":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Es el eje vial de menor denominación numérica que tiene intersección con la vía principal, y se emplea para generar nomenclatura domiciliaria.");
                    break;
                case "9":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Campo numérico, sirve para diferenciar las vías internas, generalmente siguen un orden lógico, ya sea alfabético o numérico o sea combinación de ambas.");
                    break;
                case "10":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Campo alfabético, sirve para diferenciar las vías internas, generalmente siguen un orden lógico, ya sea alfabético o numérico o sea combinación de ambas.");
                    break;
                case "11":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Campo que indica el cuadrante al que pertenece en este caso la vía generadora, solo toma uno de los siguientes valores: SUR, ESTE, OESTE, NORTE.");
                    break;
                case "12":
                    await dialogService.ShowMessage(AppResources.TitleRequestRervice, "Valor numérico o nombre común que identifica la vía, en este caso la vía principal, por lo general las avenidas o vías principales tienen asociado un nombre común.");
                    break;
            }

        }
        private async void AddNewAddress()
        {
            if (CurrentLocation == null)
            {
                OtherSelected = string.Empty;
                if (await permissionService.CheckPermissions(Plugin.Permissions.Abstractions.Permission.Location) == false)
                {
                    await dialogService.ShowMessage("ucm", "Requiere permisos localización");
                    return;
                }

                if (geolocatorService.IsGeolocationAvailable == false || geolocatorService.IsGeolocationEnabled == false)
                {
                    await dialogService.ShowMessage("ucm", "Localización no habilitada para esta aplicación");
                    return;
                }

                IsShowingUser = geolocatorService.IsGeolocationAvailable && geolocatorService.IsGeolocationEnabled;

                Plugin.Geolocator.Abstractions.Position position = await geolocatorService.GetPositionAsync();

                CurrentLocation = new LocationViewModel { Latitude = position.Latitude, Longitude = position.Longitude };
            }
            dialogService.ShowProgress();
            confirmAddress = false;
            ValidateCoverageLatLong();
            ClearNewAddress();
            AddressSelected = null;
            RequestDepartments request = new RequestDepartments();
            ResponseDepartments response = await apiService.GetDepartments(request);
            dialogService.HideProgress();
            ValidateResponseDepartments(response);
        }

        private void ClearNewAddress()
        {
            AddressCreated = null;
            AddressCreated = null;
            DepartamentSelected = null;
            CitySelected = null;
            CountrySelected = null;
            IsEnabledDepartments = false;

            NeighborhoodSelected = null;

            NewAddress = new AddressViewModel();

            if (IsCO)
            {
                NewAddress.TitleCity = "Ciudad";
                NewAddress.Neighborhood = "(*) Barrio";
            }
            else
            {
                NewAddress.TitleCity = "Localidad";
                NewAddress.Neighborhood = "Barrio";
            }

            IsInCoverage = false;
        }

        private async void ValidateResponseDepartments(ResponseDepartments response)
        {
            Departaments = new ObservableCollection<Departament>();

            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            foreach (Departament item in response.Departaments)
            {
                Departaments.Add(item);
            }

            await navigationService.Navigate(AppPages.AddNewAddressPage);
        }

        public async Task LoadPersons()
        {
            if (await permissionService.CheckPermissions(Plugin.Permissions.Abstractions.Permission.Location) == false)
            {
                await dialogService.ShowMessage(AppConfigurations.Brand, "Requiere permisos localización.");
                return;
            }

            if (geolocatorService.IsGeolocationAvailable == false || geolocatorService.IsGeolocationEnabled == false)
            {
                await dialogService.ShowMessage(AppConfigurations.Brand, "Localización no habilitada.");
                return;
            }

            dialogService.ShowProgress();

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

            await navigationService.Navigate(AppPages.ServicesPage);
        }

        public ServicesPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService, IValidatorService validatorService, ILoginViewModel loginViewModel, IPhoneService phoneService, IGeolocatorService geolocatorService, IPermissionService permissionService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.validatorService = validatorService;
            this.loginViewModel = loginViewModel;
            this.phoneService = phoneService;
            this.geolocatorService = geolocatorService;
            this.permissionService = permissionService;

            IsShowingUser = geolocatorService.IsGeolocationAvailable && geolocatorService.IsGeolocationEnabled;

            IsCO = AppConfigurations.Brand == "emi";
            IsUY = !IsCO;
            OtherSelected = string.Empty;
            IsToggledPhoneNumber = true;
            //Coverages = new List<Polygon>();
            Departaments = new ObservableCollection<Departament>();
            Cities = new ObservableCollection<City>();
            Neighborhoods = new ObservableCollection<Neighborhood>();
            People = new ObservableCollection<PersonViewModel>();
            Quadrants = new ObservableCollection<Quadrant>();
            ServicesEnableds = new ObservableCollection<ServicesEnabledViewModel>();
            Vias = new ObservableCollection<Via>();
            Addresses = new ObservableCollection<Models.Domain.Address>();
            NewAddress = new AddressViewModel();
            Countries = new ObservableCollection<Country>();

            HtmlSource = new UrlWebViewSource();

            ApplicantCellPhone = string.Empty;
            PatientCellPhone = string.Empty;

            IsEnabledCities = false;
            IsEnabledDepartments = false;
            IsEnabledDetail = false;
            IsEnabledNeighborhood = false;

            NewAddress.TitleCity = "Localidad";
            NewAddress.Neighborhood = "Barrio";
        }
    }
}
