namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Controls;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class CertificateCardViewModel : ViewModelBase
    {

        IDialogService dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
        IApiService apiService = ServiceLocator.Current.GetInstance<IApiService>();
        INavigationService navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();

        public bool IsVisibleGenerate { get; set; }
        public string Document { get; set; }
        public string DocumentType { get; set; }
        public string Name { get; set; }
        public bool HasDebt { get; set; }
        public DateTime MinStartDate { get; set; } = DateTime.Now;
        public string GroupCode { get; set; }


        private DateTime startDateSelected = DateTime.Now;
        public DateTime StartDateSelected
        {
            get { return startDateSelected; }
            set
            {
                if (startDateSelected != value)
                {
                    startDateSelected = value;
                    RaisePropertyChanged(nameof(StartDateSelected));
                    MaxEndDate = StartDateSelected.AddMonths(3);
                    MinimumEndDate = StartDateSelected;
                }
            }
        }

        private ObservableCollection<SelectCitiesViewModel> citiesSelected;
        public ObservableCollection<SelectCitiesViewModel> CitiesSelected
        {
            get { return citiesSelected; }
            set
            {
                citiesSelected = value;
                RaisePropertyChanged(nameof(CitiesSelected));
            }
        }

        private ObservableCollection<SelectCitiesViewModel> citiesSave;
        public ObservableCollection<SelectCitiesViewModel> CitiesSave
        {
            get { return citiesSave; }
            set
            {
                citiesSave = value;
                RaisePropertyChanged(nameof(CitiesSave));
            }
        }

        string citySearch;
        public string CitySearch
        {
            get { return citySearch; }
            set
            {
                if (citySearch != value)
                {
                    citySearch = value;
                    RaisePropertyChanged("CardSearch");

                    if (string.IsNullOrEmpty(citySearch) && CitiesSave != null)
                    {
                        Cities = new ObservableCollection<SelectCitiesViewModel>(CitiesSave);
                    }
                    else
                    {
                        if (CitySearch.Length >= 3)
                        {
                            SearchCity();
                        }
                    }
                }
            }
        }

        private DateTime endDateSelected = DateTime.Now;
        public DateTime EndDateSelected
        {
            get { return endDateSelected; }
            set
            {
                endDateSelected = value;
                RaisePropertyChanged(nameof(EndDateSelected));
            }
        }

        private DateTime minimunEndDate = DateTime.Now;
        public DateTime MinimumEndDate
        {
            get { return minimunEndDate; }
            set
            {
                minimunEndDate = value;
                RaisePropertyChanged(nameof(MinimumEndDate));
            }
        }

        private DateTime maxEndDate;
        public DateTime MaxEndDate
        {
            get { return maxEndDate; }
            set
            {
                maxEndDate = value;
                RaisePropertyChanged(nameof(MaxEndDate));
            }
        }

        private string certificateName;
        public string CertificateName
        {
            get { return certificateName; }
            set
            {
                certificateName = value;
                RaisePropertyChanged(nameof(CertificateName));
            }
        }

        private bool errorCountry;
        public bool ErrorCountry
        {
            get { return errorCountry; }
            set
            {
                errorCountry = value;
                RaisePropertyChanged(nameof(ErrorCountry));
            }
        }

        private bool errorDate;
        public bool ErrorDate
        {
            get { return errorDate; }
            set
            {
                errorDate = value;
                RaisePropertyChanged(nameof(ErrorDate));
            }
        }

        private bool errorCity;
        public bool ErrorCity
        {
            get { return errorCity; }
            set
            {
                errorCity = value;
                RaisePropertyChanged(nameof(ErrorCity));
            }
        }

        private string certificateCode;
        public string CertificateCode
        {
            get { return certificateCode; }
            set
            {
                certificateCode = value;
                RaisePropertyChanged(nameof(CertificateCode));
            }
        }

        private string typeCertificate;
        public string TypeCertificate
        {
            get { return typeCertificate; }
            set
            {
                typeCertificate = value;
                RaisePropertyChanged(nameof(TypeCertificate));
            }
        }

        private ObservableCollection<ModelBase> countries;
        public ObservableCollection<ModelBase> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                RaisePropertyChanged(nameof(Countries));
            }
        }

        private ObservableCollection<string> dates;
        public ObservableCollection<string> Dates
        {
            get { return dates; }
            set
            {
                dates = value;
                RaisePropertyChanged(nameof(Dates));
            }
        }

        private string dateSelected;
        public string DateSelected
        {
            get { return dateSelected; }
            set
            {
                dateSelected = value;
                RaisePropertyChanged(nameof(DateSelected));
            }
        }

        private ObservableCollection<SelectCitiesViewModel> cities;
        public ObservableCollection<SelectCitiesViewModel> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                RaisePropertyChanged(nameof(Cities));
            }
        }

        private ModelBase countrySelected;
        public ModelBase CountrySelected
        {
            get { return countrySelected; }
            set
            {
                if (countrySelected != value)
                {
                    countrySelected = value;
                    RaisePropertyChanged(nameof(CountrySelected));
                    if (CountrySelected != null)
                        if (!string.IsNullOrWhiteSpace(CountrySelected.Code))
                        {
                            CitiesSelected = new ObservableCollection<SelectCitiesViewModel>();
                            LoadCities();
                        }
                }
            }
        }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                RaisePropertyChanged(nameof(fileName));
            }
        }

        public ICommand GeneratePdfCommand { get { return new RelayCommand<bool>(GeneratePdf); } }
        public ICommand OpenPdfCommand { get { return new RelayCommand<bool>(OpenPdf); } }
        public ICommand SelectCityCommand { get { return new RelayCommand<SelectCitiesViewModel>(SelectCity); } }
        public ICommand RemoveCityCommand { get { return new RelayCommand<SelectCitiesViewModel>(RemoveCity); } }

        private void SelectCity(SelectCitiesViewModel citySelected)
        {
            if (citySelected.IsSelected)
            {
                CitiesSelected.Remove(citySelected);
                foreach (var city in Cities)
                {
                    if (city == citySelected)
                        city.IsSelected = false;
                }
            }
            else
            {
                if (CitiesSelected.Count < 11)
                {
                    foreach (var city in Cities)
                    {
                        if (city == citySelected)
                            city.IsSelected = !city.IsSelected;
                    }

                    CitiesSelected.Insert(0, citySelected);
                }
            }
        }

        public void RemoveCity(SelectCitiesViewModel citySelected)
        {
            CitiesSelected.Remove(citySelected);
            foreach (var city in Cities)
            {
                if (city == citySelected)
                    city.IsSelected = false;
            }
        }

        void SearchCity()
        {
            Cities = new ObservableCollection<SelectCitiesViewModel>(
                CitiesSave.Where(n => n.Name.ToLower().Contains(CitySearch.ToLower())));
        }

        private  void GeneratePdf(bool email)
        {
            Clean();
            switch (certificateCode)
            {
                case "1":
                    OpenPdf(email);
                    break;
                case "5":
                    GenerateTripCertificate();
                    break;
            }
        }

        private void GenerateTripCertificate()
        {
            ICertificatesPageViewModel certificatesPageViewModel = ServiceLocator.Current.GetInstance<ICertificatesPageViewModel>();
            certificatesPageViewModel.CardSelected = this;
            GetCountry();
            navigationService.Navigate(Enumerations.AppPages.TripCertificatePage);
        }


        private async void GetCountry()
        {
            try
            {
                var response = await apiService.GetCountryRedSiem();
                if (response.Success)
                {
                    Countries = new ObservableCollection<ModelBase>();
                    foreach (var country in response.Countries)
                    {
                        Countries.Add(new ModelBase
                        {
                            Code = country.Code,
                            Name = country.Name,
                            Description = country.Description
                        });
                    }
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }


        private async void LoadCities()
        {
            try
            {
                dialogService.ShowProgress();
                var request = new RequestCitiesRedSiem
                {
                    Country = CountrySelected.Code,
                    Controller = AppConfigurations.DataListsController,
                    Action = "GetCitiesRedSiem"
                };
                var response = await apiService.GetCitiesRedSiem(request);
                if (response.Success)
                {
                    Cities = new ObservableCollection<SelectCitiesViewModel>();
                    foreach (var city in response.Cities)
                    {
                        Cities.Add(new SelectCitiesViewModel
                        {
                            Name = city.Name,
                            Code = city.Code,
                            Description = city.Description,
                            IsSelected = false
                        });
                    }
                    CitiesSave = new ObservableCollection<SelectCitiesViewModel>(Cities);
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
                dialogService.HideProgress();
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);

            }
        }

        private void Clean()
        {
            Cities = new ObservableCollection<SelectCitiesViewModel>();
            CountrySelected = null;
            CitiesSelected = new ObservableCollection<SelectCitiesViewModel>();
            MinimumEndDate = DateTime.Now;
            EndDateSelected = DateTime.Now;
            MinStartDate = DateTime.Now;
            StartDateSelected = DateTime.Now;
            DateSelected = string.Empty;
            CitiesSave = new ObservableCollection<SelectCitiesViewModel>();

        }

        private void CleanError()
        {
            ErrorCountry = false;
            ErrorCity = false;
            ErrorDate = false;
        }

        private bool ValidateFields()
        {
            bool result = true;
            if (certificateCode.Equals("5"))
            {
                if (CountrySelected == null)
                {
                    ErrorCountry = true;
                    result = false;
                }
                if (Cities.Count(x => x.IsSelected == true) < 1)
                {
                    ErrorCity = true;
                    result = false;
                }
            }
            if (CertificateCode.Equals("3"))
            {
                if (string.IsNullOrWhiteSpace(DateSelected))
                {
                    result = false;
                    ErrorDate = true;
                }
            }

            return result;
        }



        private async void OpenPdf(bool email = false)
        {
            try
            {
                var selectedCities = new List<CertificateCities>();
                CleanError();

                if (ValidateFields())
                {
                    if (CitiesSelected.Count > 0)
                    {
                        foreach (var city in CitiesSelected)
                            selectedCities.Add(
                                new CertificateCities
                                {
                                    Code = city.Code,
                                    Description = city.Description,
                                    Name = city.Name
                                });
                    }
                    dialogService.ShowProgress();
                    var request = new RequestDownloadCertificate
                    {
                        NameCountry = CountrySelected != null ? CountrySelected.Name : string.Empty,
                        CertificateCode = int.Parse(CertificateCode),
                        CodeCity = selectedCities,
                        Country = CountrySelected != null ? CountrySelected.Code : string.Empty,
                        Document = Document,
                        CertifiedYear = DateSelected,
                        DateEnd = CertificateCode.Equals("5") ? EndDateSelected.ToString("yyyy-MM-dd") : string.Empty,
                        DateStart = CertificateCode.Equals("5") ? StartDateSelected.ToString("yyyy-MM-dd") : string.Empty,
                        DocumentType = DocumentType,
                        FileName = FileName,
                        FullNameCertified = $"{loginViewModel.User.NameOne} {loginViewModel.User.NameTwo} {loginViewModel.User.LastNameOne} {loginViewModel.User.LastNameTwo}",
                        GroupCode = GroupCode,
                        Name = Name,
                        Mail = email ? loginViewModel.User.UserName : string.Empty,
                        RequestGroup = Name.Equals("Grupo Familiar"),
                        TypeCertificate = TypeCertificate,
                        Controller = "Affiliate",
                        Action = email ? "GetSendCertAffiliatedPayments" : "GetDownloadCertAffiliatedPayments"
                    };
                    if (email)
                    {
                        var response = await apiService.SendCertAffiliatedPayments(request);
                        dialogService.HideProgress();
                        if (response.Success)
                        {
                            await dialogService.ShowMessage("Proceso exitoso", $"Hemos enviado al correo electrónico {loginViewModel.User.UserName} el certificado seleccionado");
                        }
                        else
                        {
                            await dialogService.ShowMessage(response.Title, response.Message);
                        }
                    }
                    else
                    {
                        var response = await apiService.GetDownloadCertAffiliatedPayments(request);
                        dialogService.HideProgress();
                        if (response.Success)
                        {
                            IPdfPageViewModel pdfPage = ServiceLocator.Current.GetInstance<IPdfPageViewModel>();
                            var bytes = Convert.FromBase64String(response.Download.BitCertificate);
                            pdfPage.OpenPdf("", response.Download.NameCertificate, bytes, true, CertificateName);
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
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }

        }

    }
}
