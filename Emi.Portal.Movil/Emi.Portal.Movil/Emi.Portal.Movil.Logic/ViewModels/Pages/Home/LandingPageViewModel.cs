namespace Emi.Portal.Movil.Logic.ViewModels.Pages
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Contracts.Views;
    using Emi.Portal.Movil.Logic.Enumerations;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class LandingPageViewModel : ViewModelBase, ILandingPageViewModel
    {
        #region Properties
        IDialogService dialogService;
        INavigationService navigationService;
        IPhoneService phoneService;
        IApiService apiService;
        ILoginViewModel loginViewModel;
        ICurrentServiceViewViewModel currentServiceViewViewModel;

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

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    RaisePropertyChanged("IsRefreshing");
                }
            }
        }

        ObservableCollection<string> items;
        public ObservableCollection<string> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged("Items");
            }
        }
        #endregion

        #region Commands
        public ICommand ServicesCommand { get { return new RelayCommand(Services); } }
        public ICommand ScheduledServicesCommand { get { return new RelayCommand(NearbyClinicsPage); } }
        public ICommand RefreshHomeCommand { get { return new RelayCommand(RefreshHome); } }
        #endregion

        #region Methods        

        private async void NearbyClinicsPage()
        {
            INearbyClinicsPageViewModel nearbyClinicsPageViewModel = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
            nearbyClinicsPageViewModel.CurrentLocation = null;

            IMedicalCenterCoordinationPageViewModel medicalCenter = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
            medicalCenter.IsVisibleCoordination = true;

            await navigationService.Navigate(AppPages.NearbyClinicsPage);        
        }

        private async void Services()
        {
            IServicesPageViewModel servicesPageViewModel = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();
            await servicesPageViewModel.LoadPersons();
        }

        public async void RefreshHome()
        {
            IsRefreshing = true;
            await currentServiceViewViewModel.GetMedicalHomeService();
            IsRefreshing = false;
        }
        #endregion

        #region Constructor
        public LandingPageViewModel(INavigationService navigationService, IDialogService dialogService, IPhoneService phoneService, IApiService apiService, ICurrentServiceViewViewModel currentServiceViewViewModel)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.phoneService = phoneService;
            this.apiService = apiService;
            this.currentServiceViewViewModel = currentServiceViewViewModel;

            loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();

            UserName = loginViewModel.User.NameOne;

            Items = new ObservableCollection<string>();
            Items.Add(string.Empty);
        }
        #endregion
        
    }
}
