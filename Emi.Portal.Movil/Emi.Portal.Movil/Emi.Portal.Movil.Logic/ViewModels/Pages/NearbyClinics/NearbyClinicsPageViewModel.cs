namespace Emi.Portal.Movil.Logic.ViewModels.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Plugin.Geolocator.Abstractions;

    public class NearbyClinicsPageViewModel : ViewModelBase, INearbyClinicsPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IPhoneService phoneService;
        IServicesPageViewModel servicesPageViewModel;


        public bool IsEmergency { get; set; }
        public ObservableCollection<ClinicViewModel> Clinics { get; set; }
        public ClinicViewModel ClinicSelected { get; set; }

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

        Position currentLocation;
        public Position CurrentLocation 
        { 
            get { return currentLocation; }
            set
            {
                currentLocation = value;
                RaisePropertyChanged("CurrentLocation");
            }
        }

        private bool isReady;

        public bool IsReady
        {
            get { return isReady; }
            set
            {
                isReady = value;
                RaisePropertyChanged("IsReady");
            }
        }
        #endregion

        #region Constructor
        public NearbyClinicsPageViewModel(INavigationService navigationService, IDialogService dialogService, IApiService apiService, IPhoneService phoneService)
        {
            servicesPageViewModel = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();
            Clinics = new ObservableCollection<ClinicViewModel>();
            ClinicSelected = new ClinicViewModel();
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.phoneService = phoneService;
        }
        #endregion

        #region Methods
        private void LoadNearbyClinics(ResponseNearbyClinics responseNearbyClinics)
        {
            try
            {
                Clinics.Clear();
                IList<Clinic> nearbyClinics = new List<Clinic>();
                foreach (Clinic item in responseNearbyClinics.Clinics)
                {
                    if (CurrentLocation != null)
                    {
                        item.Distance = ViewModelHelper.Distance(CurrentLocation.Latitude, CurrentLocation.Longitude,
                            double.Parse(item.Latitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)),
                            double.Parse(item.Longitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)));
                    }

                    nearbyClinics.Add(item);
                }

                IList<Clinic> clinicsOrdered = new List<Clinic>();

                if (CurrentLocation != null)
                {
                    clinicsOrdered = nearbyClinics.OrderBy(x => x.Distance).ToList();
                }
                else
                {
                    clinicsOrdered = nearbyClinics.OrderBy(x => x.Name).ToList();
                }

                Clinics.Clear();

                foreach (var item in clinicsOrdered)
                {
                    ClinicViewModel clinicViewModel = new ClinicViewModel();
                    ViewModelHelper.SetClinicToClinicViewModel(clinicViewModel, item, phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                    Clinics.Add(clinicViewModel);
                }
            }
            catch(Exception e)
            {

            }
        }

        public async Task LoadClinincs()
        {
            Clinics.Clear();
            
            if (IsEmergency)
            {
                double latitude = 0;
                double longitude = 0;
            
                try
                {
                    double.TryParse(servicesPageViewModel.AddressSelected.Latitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator), out latitude);
                    double.TryParse(servicesPageViewModel.AddressSelected.Longitude.Replace(".", phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator), out longitude);
                }
                catch
                {
                    latitude = servicesPageViewModel.CurrentLocation.Latitude;
                    longitude = servicesPageViewModel.CurrentLocation.Longitude;
                }
            
                CurrentLocation = new Position
                {
                    Latitude = latitude,
                    Longitude = longitude,
                };
            }
            
            dialogService.ShowProgress();
            RequestClinics request = new RequestClinics();
            ResponseNearbyClinics responseNearbyClinics = await apiService.GetClinics(request);
            dialogService.HideProgress();
            if (responseNearbyClinics.Success)
                LoadNearbyClinics(responseNearbyClinics);
            else
            {
                await dialogService.ShowMessage(AppResources.TitleWithoutConnection, AppResources.WithoutConnection);
                await navigationService.Back();
            }
        }        
        private void CallCategory()
        {
            ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
            callViewModel.CallCategory();
        }
        #endregion

        #region Commands        
        public ICommand CallCategoryCommand { get { return new RelayCommand(CallCategory); } }
        #endregion
    }
}
