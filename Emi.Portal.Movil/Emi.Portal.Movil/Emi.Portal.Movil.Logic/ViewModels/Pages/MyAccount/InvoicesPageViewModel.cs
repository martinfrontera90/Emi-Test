namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class InvoicesPageViewModel : ViewModelBase, IInvoicesPageViewModel
    {
        #region Properties
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

        private DateTime minimumDate;
        public DateTime MinimumDate
        {
            get { return minimumDate; }
            set
            {
                if (minimumDate != value)
                {
                    minimumDate = value;
                    RaisePropertyChanged("MinimumDate");
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

        private ObservableCollection<StatusInvoice> statusInvoices;
        public ObservableCollection<StatusInvoice> StatusInvoices
        {
            get { return statusInvoices; }
            set
            {
                if (statusInvoices != value)
                {
                    statusInvoices = value;
                    RaisePropertyChanged("StatusInvoices");
                }
            }
        }

        private StatusInvoice statusInvoiceSelected;
        public StatusInvoice StatusInvoiceSelected
        {
            get { return statusInvoiceSelected; }
            set
            {
                if (statusInvoiceSelected != value)
                {
                    statusInvoiceSelected = value;
                    RaisePropertyChanged("StatusInvoiceSelected");
                }
            }
        }

        private InvoiceViewModel invoiceSelected;
        public InvoiceViewModel InvoiceSelected
        {
            get { return invoiceSelected; }
            set
            {
                if (invoiceSelected != value)
                {
                    invoiceSelected = value;
                    RaisePropertyChanged("InvoiceSelected");
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

        private ObservableCollection<InvoiceViewModel> invoices;
        public ObservableCollection<InvoiceViewModel> Invoices
        {
            get { return invoices; }
            set
            {
                if (invoices != value)
                {
                    invoices = value;
                    RaisePropertyChanged("Invoices");
                }
            }
        }
        #endregion

        #region Commands
        public ICommand SearchInvoicesCommand { get { return new RelayCommand(SearchInvoices); } }
        #endregion

        #region Methods
        public async Task GoToDetails()
        {
            await navigationService.Navigate(AppPages.InvoiceDetailPage);
        }

        public async Task LoadData()
        {            
            dialogService.ShowProgress();
            RequestStatusInvoice request = new RequestStatusInvoice
            {
                Document = loginViewModel.User.Document,
                DocumentType = loginViewModel.User.DocumentType
            };
            ResponseStatusInvoices response = await apiService.GetStatusInvoices(request);
            ValidateResponseStatusInvoicesLists(response);
            dialogService.HideProgress();
            RefreshDates();
        }

        private void ValidateResponseStatusInvoicesLists(ResponseStatusInvoices response)
        {
            if (response.Success && response.StatusCode == 0)
            {
                StatusInvoices = new ObservableCollection<StatusInvoice>();
                StatusInvoices.Add(new StatusInvoice { Code = "-1", Name = " " });
                foreach (StatusInvoice statusInvoice in response.StatusInvoices)
                {
                    if (!string.IsNullOrEmpty(statusInvoice.Code) && !string.IsNullOrEmpty(statusInvoice.Name))
                    {                        
                        StatusInvoices.Add(statusInvoice);                        
                    }
                }                
            }
        }

        private async void SearchInvoices()
        {
            if (InitDate > EndDate)
            {
                await dialogService.ValidateDates();
                return;
            }

            dialogService.ShowProgress();
            RequestInvoices request = new RequestInvoices
            {
                Document = loginViewModel.User.Document,
                DocumentType = loginViewModel.User.DocumentType,
                EndDate = EndDate.ToString("yyyyMMdd"),
                InitDate = InitDate.ToString("yyyyMMdd"),
                IdStatusInvoice = StatusInvoiceSelected != null && StatusInvoiceSelected.Code != "-1" ? StatusInvoiceSelected.Code : string.Empty,
            };

            ResponseInvoices response = await apiService.GetInvoices(request);
            dialogService.HideProgress();
            ValidateResponseInvoices(response);
        }

        private async void ValidateResponseInvoices(ResponseInvoices response)
        {
            if (response.Success && response.StatusCode == 0)
            {
                if (response.InvoicesResponse.Invoices != null && response.InvoicesResponse.Invoices.Count > 0)
                {
                    Invoices = new ObservableCollection<InvoiceViewModel>();
                    foreach (Invoice invoice in response.InvoicesResponse.Invoices)
                    {
                        InvoiceViewModel viewModel = new InvoiceViewModel
                        {
                            Amount = invoice.State == PaymentState.Impaga.ToString() ? invoice.BalanceInvoice : invoice.Amount,
                            BalanceInvoice = invoice.BalanceInvoice,
                            BroadcastDate = invoice.BroadcastDate,
                            Currency = invoice.Currency,
                            DescriptionInvoice = invoice.DescriptionInvoice,
                            ExpirationDate = invoice.ExpirationDate,
                            Finalconsumer = invoice.Finalconsumer,
                            InternalNumber = invoice.InternalNumber,
                            InternalSeries = invoice.InternalSeries,
                            Number = invoice.Number,
                            AmountTitle = invoice.State == PaymentState.Impaga.ToString() ? "Saldo:" : "Importe:",
                            Series = invoice.Series,
                            State = invoice.State,                                                                                    
                            Taxedamount = invoice.Taxedamount,
                            VoucherType = invoice.VoucherType,
                        };
                        Invoices.Add(viewModel);
                    }
                    await navigationService.Navigate(Enumerations.AppPages.InvoicesPage);
                }
                else
                {
                    await dialogService.ShowMessage(AppResources.TitleMyInvoices, "No se han encontrado resultados para tu búsqueda.");
                }
            }
            else
            {
                await dialogService.ShowMessage(response.Title, response.Message);
            }
        }                

        private void RefreshDates()
        {
            EndDate = DateTime.Now;
            InitDate = EndDate.AddYears(-1);
            MinimumDate = EndDate.AddYears(-1);
            MaximumDate = DateTime.Now;
        }
        #endregion

        #region Constructor
        public InvoicesPageViewModel(IApiService apiService, IDialogService dialogService, ILoginViewModel loginViewModel, INavigationService navigationService, IPhoneService phoneService, IValidatorService validatorService, IPermissionService permissionService)
        {
            RefreshDates();
            IsCO = AppConfigurations.Brand == "emi";
            IsUY = AppConfigurations.Brand == "ucm";
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.loginViewModel = loginViewModel;
            this.navigationService = navigationService;
            this.phoneService = phoneService;
            this.validatorService = validatorService;
            this.permissionService = permissionService;
        }
        #endregion

    }
}
