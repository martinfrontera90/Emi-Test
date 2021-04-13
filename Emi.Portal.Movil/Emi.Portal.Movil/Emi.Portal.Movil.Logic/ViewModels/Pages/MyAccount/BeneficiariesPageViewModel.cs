namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class BeneficiariesPageViewModel : ViewModelBase, IBeneficiariesPageViewModel
    {
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

        private ObservableCollection<Person> beneficiaries;

        public ObservableCollection<Person> Beneficiaries
        {
            get { return beneficiaries; }
            set
            {
                if (beneficiaries != value)
                {
                    beneficiaries = value;
                    RaisePropertyChanged("Beneficiaries");
                }
            }
        }
        public ICommand RefreshBeneficiariesCommand { get { return new RelayCommand(RefreshBeneficiaries); } }

        private void RefreshBeneficiaries()
        {
            LoadBeneficiaries();
        }

        private async void LoadBeneficiaries()
        {
            dialogService.ShowProgress();
            IsRefreshing = false;
            ILoginViewModel login = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            RequestBeneficiaries request = new RequestBeneficiaries
            {
                Document = login.User.Document,
                DocumentType = login.User.DocumentType
            };
            ResponseBeneficiaries response = await apiService.GetBeneficiaries(request);
            dialogService.HideProgress();
            ValidateResponseBeneficiaries(response);
        }

        private async void ValidateResponseBeneficiaries(ResponseBeneficiaries response)
        {
            Beneficiaries = new ObservableCollection<Person>();
            if (response.Success && response.StatusCode == 0)
            {
                foreach (Person beneficiary in response.Beneficiaries)
                {
                    Beneficiaries.Add(beneficiary);
                }
                return;
            }

            await dialogService.ShowMessage(response.Title, response.Message);
            await navigationService.Back();
        }

        public BeneficiariesPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService, IPhoneService phoneService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.phoneService = phoneService;

            Beneficiaries = new ObservableCollection<Person>();
            LoadBeneficiaries();
        }
    }
}
