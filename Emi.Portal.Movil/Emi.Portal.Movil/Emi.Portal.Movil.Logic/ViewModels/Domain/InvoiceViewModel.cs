namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class InvoiceViewModel : ViewModelBase, IInvoiceViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        ILoginViewModel loginViewModel;
        IPhoneService phoneService;

        private string internalSeries;
        public string InternalSeries
        {
            get { return internalSeries; }
            set
            {
                if (internalSeries != value)
                {
                    internalSeries = value;
                    RaisePropertyChanged("InternalSeries");
                }
            }
        }

        private string internalNumber;
        public string InternalNumber
        {
            get { return internalNumber; }
            set
            {
                if (internalNumber != value)
                {
                    internalNumber = value;
                    RaisePropertyChanged("InternalNumber");
                }
            }
        }

        private string series;
        public string Series
        {
            get { return series; }
            set
            {
                if (series != value)
                {
                    series = value;
                    RaisePropertyChanged("Series");
                }
            }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set
            {
                if (number != value)
                {
                    number = value;
                    RaisePropertyChanged("Number");
                }
            }
        }

        public string NumberModified
        {
            get { return $"Factura No. {Series} - {Number}"; }
        }

        private string voucherType;
        public string VoucherType
        {
            get { return voucherType; }
            set
            {
                if (voucherType != value)
                {
                    voucherType = value;
                    RaisePropertyChanged("VoucherType");
                }
            }
        }

        private string currency;
        public string Currency
        {
            get { return currency; }
            set
            {
                if (currency != value)
                {
                    currency = value;
                    RaisePropertyChanged("Currency");
                }
            }
        }

        private string amount;
        public string Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    RaisePropertyChanged("Amount");
                }
            }
        }

        private string balanceInvoice;
        public string BalanceInvoice
        {
            get { return balanceInvoice; }
            set
            {
                if (balanceInvoice != value)
                {
                    balanceInvoice = value;
                    RaisePropertyChanged("BalanceInvoice");
                }
            }
        }

        private string state;
        public string State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    RaisePropertyChanged("State");
                }
            }
        }

        private string finalconsumer;
        public string Finalconsumer
        {
            get { return finalconsumer; }
            set
            {
                if (finalconsumer != value)
                {
                    finalconsumer = value;
                    RaisePropertyChanged("Finalconsumer");
                }
            }
        }

        private string taxedamount;
        public string Taxedamount
        {
            get { return taxedamount; }
            set
            {
                if (taxedamount != value)
                {
                    taxedamount = value;
                    RaisePropertyChanged("Taxedamount");
                }
            }
        }

        private string descriptionInvoice;
        public string DescriptionInvoice
        {
            get { return descriptionInvoice; }
            set
            {
                if (descriptionInvoice != value)
                {
                    descriptionInvoice = value;
                    RaisePropertyChanged("DescriptionInvoice");
                }
            }
        }

        private string broadcastDate;
        public string BroadcastDate
        {
            get { return broadcastDate; }
            set
            {
                if (broadcastDate != value)
                {
                    broadcastDate = value;
                    RaisePropertyChanged("BroadcastDate");
                }
            }
        }

        private string expirationDate;
        public string ExpirationDate
        {
            get { return expirationDate; }
            set
            {
                if (expirationDate != value)
                {
                    expirationDate = value;
                    RaisePropertyChanged("ExpirationDate");
                }
            }
        }

        private string amountTitle;
        public string AmountTitle
        {
            get { return amountTitle; }
            set
            {
                if (amountTitle != value)
                {
                    amountTitle = value;
                    RaisePropertyChanged("AmountTitle");
                }
            }
        }

        #endregion

        #region Commands
        public ICommand OptionsCommand { get { return new RelayCommand(Options); } }
        #endregion

        #region Methods
        private async void Options()
        {
            string option = await dialogService.Invoices(State);

            IInvoicesPageViewModel invoicesViewModel = ServiceLocator.Current.GetInstance<IInvoicesPageViewModel>();
            invoicesViewModel.InvoiceSelected = this;

            switch (option)
            {
                case "Ver detalle":
                    await invoicesViewModel.GoToDetails();
                    break;
                case "Pagar":
                    ILoginViewModel login = ServiceLocator.Current.GetInstance<ILoginViewModel>();
                    string url = $"{AppConfigurations.UrlPaymentMethodsApp}?document={login.User.Document}&documentType={login.User.DocumentType}&ProductName=Factura&NameOne={login.User.NameOne}&NameTwo={login.User.NameTwo}&LastNameOne={login.User.LastNameOne}&LastNameTwo={login.User.LastNameTwo}&userName={login.User.UserName}&CellPhone={login.User.CellPhone}&InvoiceTotal={Amount}&InternalSerie={InternalSeries}&InternalNumber={InternalNumber}&NumberInvoice={Number.Replace(" ", "")}&anonymouspay=true";
                    phoneService.OpenUrl(url);
                    break;                
            }
        }
        #endregion

        public InvoiceViewModel()
        {
            apiService = ServiceLocator.Current.GetInstance<IApiService>();
            dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            phoneService = ServiceLocator.Current.GetInstance<IPhoneService>();
        }
    }
}
