namespace Emi.Portal.Movil.Logic.ViewModels.Pages
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class MedicalCenterCoordinationPageViewModel : ViewModelBase, IMedicalCenterCoordinationPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IPhoneService phoneService;

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

        public List<CoordinationViewModel> AllCoordinations { get; set; }
        public CoordinationViewModel CoordinationSelected { get; set; }

        private ObservableCollection<CoordinationViewModel> coordinations;
        public ObservableCollection<CoordinationViewModel> Coordinations
        {
            get { return coordinations; }
            set
            {
                if (coordinations != value)
                {
                    coordinations = value;
                    RaisePropertyChanged("Coordinations");
                }
            }
        }

        private bool isVisiblePay;
        public bool IsVisiblePay
        {
            get { return isVisiblePay; }
            set
            {
                if (isVisiblePay != value)
                {
                    isVisiblePay = value;
                    RaisePropertyChanged("IsVisiblePay");
                }
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (IsRefreshing != value)
                {
                    isRefreshing = value;
                    RaisePropertyChanged("IsRefreshing");
                }
            }
        }

        private bool isVisibleCoordination;
        public bool IsVisibleCoordination
        {
            get { return isVisibleCoordination; }
            set
            {
                isVisibleCoordination = value;
                RaisePropertyChanged("IsVisibleCoordination");
            }
        }

        #endregion

        #region Constructor
        public MedicalCenterCoordinationPageViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IPhoneService phoneService,
            INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.phoneService = phoneService;
            this.navigationService = navigationService;

            IsVisiblePay = false;

            AllCoordinations = new List<CoordinationViewModel>();
            Coordinations = new ObservableCollection<CoordinationViewModel>();
            CoordinationSelected = new CoordinationViewModel();
        }
        #endregion

        #region Commands      
        public ICommand CallMedicalCenterCommand { get { return new RelayCommand(CallMedicalCenterLine); } }
        public ICommand NewCoordinationCommand { get { return new RelayCommand(NewCoordination); } }
        public ICommand CoordinationCommand { get { return new RelayCommand(Coordination); } }
        public ICommand RefreshPendingCoordinationsCommand { get { return new RelayCommand(RefreshPendingCoordinations); } }
        #endregion

        #region Methods
        private async void RefreshPendingCoordinations()
        {
            await LoadCoordinations();
        }
        private void CallMedicalCenterLine()
        {
            ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
            callViewModel.CallCategory();
        }
        private async void NewCoordination()
        {
            await navigationService.Navigate(AppPages.NewMedicalCenterCoordinationPage);
        }
        private async void Coordination()
        {
            INewMedicalCenterCoordinationPageViewModel newMedicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
            newMedicalCenterCoordinationPageViewModel.PersonSelected = null;
            await navigationService.Navigate(AppPages.MedicalCenterCoordinationPage);
        }

        public async Task LoadCoordinations()
        {
            dialogService.ShowProgress();
            isRefreshing = false;
            RequestPendingCoordinations request = new RequestPendingCoordinations();
            ResponsePendingCoordinations responsePendingCoordinations = await apiService.GetPendingCoordinations(request);
            dialogService.HideProgress();
            if (responsePendingCoordinations.Success && responsePendingCoordinations.StatusCode == 0)
            {
                LoadPendingCoordinations(responsePendingCoordinations);
            }
            else
            {
                await dialogService.ShowMessage(responsePendingCoordinations.Title, responsePendingCoordinations.Message);
                await navigationService.Back();
            }
        }

        private void LoadPendingCoordinations(ResponsePendingCoordinations responsePendingCoordinations)
        {
            Coordinations = new ObservableCollection<CoordinationViewModel>();
            foreach (PendingCoordination coordination in responsePendingCoordinations.PendingCoordinations)
            {
                CoordinationViewModel coordinationViewModel = new CoordinationViewModel();
                ViewModelHelper.SetCoordinationToCoordinationViewModel(coordinationViewModel, coordination, phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                Coordinations.Add(coordinationViewModel);
            }
        }
        #endregion
    }
}
