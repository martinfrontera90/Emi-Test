namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyHealth
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Contracts.Views;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class ScheduledServicesPageViewModel : ViewModelBase, IScheduledServicesPageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        ILoginViewModel loginViewModel;
        ICurrentServiceViewViewModel currentServiceViewViewModel;

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


        private ObservableCollection<ServiceHistoryViewModel> scheduledServices;
        public ObservableCollection<ServiceHistoryViewModel> ScheduledServices
        {
            get { return scheduledServices; }
            set
            {
                if (scheduledServices != value)
                {
                    scheduledServices = value;
                    RaisePropertyChanged("ScheduledServices");
                }
            }
        }

        public ICommand RefreshScheduledServicesCommand { get { return new RelayCommand(RefreshScheduledServices); } }

        private void RefreshScheduledServices()
        {
            IsRefreshing = false;
            LoadScheduledServices();
        }

        public ScheduledServicesPageViewModel(IApiService apiService, IDialogService dialogService, 
            ILoginViewModel loginViewModel,
            ICurrentServiceViewViewModel currentServiceViewViewModel)

        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.loginViewModel = loginViewModel;
            this.currentServiceViewViewModel = currentServiceViewViewModel;

            IsRefreshing = false;

            ScheduledServices = new ObservableCollection<ServiceHistoryViewModel>();
        }

        public async void LoadScheduledServices()
        {
            RequestSheduledServices request = new RequestSheduledServices
            {
                Document = loginViewModel.User.Document,
                DocumentType = loginViewModel.User.DocumentType,
                IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference,
            };
            dialogService.ShowProgress();
            ResponseSheduledServices response = await apiService.GetSheduledServices(request);
            dialogService.HideProgress();
            ValidateResponseSheduledServices(response);
        }

        private async void ValidateResponseSheduledServices(ResponseSheduledServices response)
        {
            ScheduledServices = new ObservableCollection<ServiceHistoryViewModel>();
            if (response.Success)
            {
                if (response.StatusCode == 0)
                {
                    if (response.ServiceHistory.Count > 0)
                    {
                        foreach (ServiceHistory service in response.ServiceHistory)
                        {
                            ServiceHistoryViewModel serviceHistoryViewModel = new ServiceHistoryViewModel();
                            ViewModelHelper.SetServiceHistoryToServiceHistoryViewModel(serviceHistoryViewModel, service);
                            ScheduledServices.Add(serviceHistoryViewModel);
                        }
                    }
                    else
                    {
                        await dialogService.ShowMessage(AppResources.TitleSheduledServices, "No se encontraron servicios programados.");
                    }
                    await currentServiceViewViewModel.GetMedicalHomeService();
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
            }
        }
    }
}
