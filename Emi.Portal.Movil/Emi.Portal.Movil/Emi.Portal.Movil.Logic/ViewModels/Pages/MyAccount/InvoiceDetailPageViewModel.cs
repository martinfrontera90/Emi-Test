namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class InvoiceDetailPageViewModel : ViewModelBase, IInvoiceDetailPageViewModel
    {
        #region Properties

        IApiService apiService;
        IDialogService dialogService;
        IInvoicesPageViewModel invoicePageViewModel;
        INavigationService navigationService;
        IValidatorService validatorService;
        IPhoneService phoneService;

        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set
            {
                if (invoiceNumber != value)
                {
                    invoiceNumber = value;
                    RaisePropertyChanged("InvoiceNumber");
                }
            }
        }

        private string generatedDate;
        public string GeneratedDate
        {
            get { return generatedDate; }
            set
            {
                if (generatedDate != value)
                {
                    generatedDate = value;
                    RaisePropertyChanged("GeneratedDate");
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

        private string paymentmode;
        public string Paymentmode
        {
            get { return paymentmode; }
            set
            {
                if (paymentmode != value)
                {
                    paymentmode = value;
                    RaisePropertyChanged("Paymentmode");
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

        private string companyPayment;
        public string CompanyPayment
        {
            get { return companyPayment; }
            set
            {
                if (companyPayment != value)
                {
                    companyPayment = value;
                    RaisePropertyChanged("CompanyPayment");
                }
            }
        }

        private string bankCardPayment;
        public string BankCardPayment
        {
            get { return bankCardPayment; }
            set
            {
                if (bankCardPayment != value)
                {
                    bankCardPayment = value;
                    RaisePropertyChanged("BankCardPayment");
                }
            }
        }

        private string addressCharge;
        public string AddressCharge
        {
            get { return addressCharge; }
            set
            {
                if (addressCharge != value)
                {
                    addressCharge = value;
                    RaisePropertyChanged("AddressCharge");
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

        private bool isVisibleAddressCharge;
        public bool IsVisibleAddressCharge
        {
            get { return isVisibleAddressCharge; }
            set
            {
                if (isVisibleAddressCharge != value)
                {
                    isVisibleAddressCharge = value;
                    RaisePropertyChanged("IsVisibleAddressCharge");
                }
            }
        }

        private bool isVisibleCompany;
        public bool IsVisibleCompany
        {
            get { return isVisibleCompany; }
            set
            {
                if (isVisibleCompany != value)
                {
                    isVisibleCompany = value;
                    RaisePropertyChanged("IsVisibleCompany");
                }
            }
        }

        private bool isVisibleBank;
        public bool IsVisibleBank
        {
            get { return isVisibleBank; }
            set
            {
                if (isVisibleBank != value)
                {
                    isVisibleBank = value;
                    RaisePropertyChanged("IsVisibleBank");
                }
            }
        }

        private bool isVisibleButtonPagar;
        public bool IsVisibleButtonPagar
        {
            get { return isVisibleButtonPagar; }
            set
            {
                if (isVisibleButtonPagar != value)
                {
                    isVisibleButtonPagar = value;
                    RaisePropertyChanged("IsVisibleButtonPagar");
                }
            }
        }

        private ObservableCollection<BeneficiaryAndProduct> beneficiaryAndProduct;
        public ObservableCollection<BeneficiaryAndProduct> BeneficiaryAndProduct
        {
            get { return beneficiaryAndProduct; }
            set
            {
                if (beneficiaryAndProduct != value)
                {
                    beneficiaryAndProduct = value;
                    RaisePropertyChanged("BeneficiaryAndProduct");
                }
            }
        }

        private string lineName;
        public string LineName
        {
            get { return lineName; }
            set
            {
                if (lineName != value)
                {
                    lineName = value;
                    RaisePropertyChanged("LineName");
                }
            }
        }

        private string productLine;
        public string ProductLine
        {
            get { return productLine; }
            set
            {
                if (productLine != value)
                {
                    productLine = value;
                    RaisePropertyChanged("ProductLine");
                }
            }
        }

        private int heightRequestList;
        public int HeightRequestList
        {
            get { return heightRequestList; }
            set
            {
                if (heightRequestList != value)
                {
                    heightRequestList = value;
                    RaisePropertyChanged("HeightRequestList");
                }
            }
        }

        private string iconPaymentMethod;
        public string IconPaymentMethod
        {
            get { return iconPaymentMethod; }
            set
            {
                if (iconPaymentMethod != value)
                {
                    iconPaymentMethod = value;
                    RaisePropertyChanged("IconPaymentMethod");
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
        public ICommand PayInvoiceCommand { get { return new RelayCommand(PayInvoice); } }
        #endregion

        #region Methods

        private void PayInvoice()
        {
            ILoginViewModel login = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            string url = $"{AppConfigurations.UrlPaymentMethodsApp}?document={login.User.Document}&documentType={login.User.DocumentType}&ProductName=Factura&NameOne={login.User.NameOne}&NameTwo={login.User.NameTwo}&LastNameOne={login.User.LastNameOne}&LastNameTwo={login.User.LastNameTwo}&userName={login.User.UserName}&CellPhone={login.User.CellPhone}&InvoiceTotal={Amount}&InternalSerie={invoicePageViewModel.InvoiceSelected.InternalSeries}&InternalNumber={invoicePageViewModel.InvoiceSelected.InternalNumber}&NumberInvoice={invoicePageViewModel.InvoiceSelected.Number.Replace(" ", "")}&anonymouspay=true";
            phoneService.OpenUrl(url);
        }

        public async Task GetInvoiceDetail()
        {
            dialogService.ShowProgress();
            RequestInvoiceDetail request = new RequestInvoiceDetail
            {
                InternalSerie = invoicePageViewModel.InvoiceSelected.InternalSeries,
                InternalNumber = invoicePageViewModel.InvoiceSelected.InternalNumber
            };
            ResponseInvoiceDetail response = await apiService.GetInvoiceDetail(request);
            dialogService.HideProgress();
            ValidateResponseInvoiceDetail(response);
        }

        private async void ValidateResponseInvoiceDetail(ResponseInvoiceDetail response)
        {

            if (await validatorService.ValidateResponse(response) == false)
            {
                await navigationService.Back();
                return;
            }

            InvoiceNumber = string.Format("{0} - {1}", response.DetailInvoicesResponse.Series, response.DetailInvoicesResponse.Number);
            GeneratedDate = response.DetailInvoicesResponse.GeneratedDate;
            ExpirationDate = response.DetailInvoicesResponse.ExpirationDate;
            Amount = response.DetailInvoicesResponse.State == PaymentState.Impaga.ToString() ? response.DetailInvoicesResponse.InvoiceBalance : response.DetailInvoicesResponse.Importe;
            LoadBeneficiaries(response);
            State = response.DetailInvoicesResponse.State;
            Paymentmode = response.DetailInvoicesResponse.Paymentmode == PaymentMode.Debito.ToString() ? "Débito" : response.DetailInvoicesResponse.Paymentmode;
            CompanyPayment = response.DetailInvoicesResponse.CompanyPayment;
            AddressCharge = response.DetailInvoicesResponse.AddressCharge;
            IsVisibleAddressCharge = response.DetailInvoicesResponse.Paymentmode == PaymentMode.Domicilio.ToString();
            BankCardPayment = response.DetailInvoicesResponse.BankCardPayment;
            IsVisibleCompany = response.DetailInvoicesResponse.Paymentmode == PaymentMode.Convenio.ToString();
            IsVisibleBank = response.DetailInvoicesResponse.Paymentmode == PaymentMode.Debito.ToString();
            IsVisibleButtonPagar = response.DetailInvoicesResponse.State == PaymentState.Impaga.ToString();
            AmountTitle = response.DetailInvoicesResponse.State == PaymentState.Impaga.ToString() ? "Saldo:" : "Importe:";
            HeightRequestList = (80 * response.DetailInvoicesResponse.BeneficiaryAndProduct.Count) + (10 * response.DetailInvoicesResponse.BeneficiaryAndProduct.Count);

            switch (response.DetailInvoicesResponse.Paymentmode.ToString())
            {
                case "Debito":
                    IconPaymentMethod = "ic_credit_card.png";
                    break;
                case "Domicilio":
                    IconPaymentMethod = "domain.png";
                    break;
                default:
                    IconPaymentMethod = "Group_23.png";
                    break;
            }
        }

        private async void LoadBeneficiaries(ResponseInvoiceDetail response)
        {
            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            if (response.DetailInvoicesResponse.BeneficiaryAndProduct != null && response.DetailInvoicesResponse.BeneficiaryAndProduct.Count > 0)
            {
                BeneficiaryAndProduct = new ObservableCollection<BeneficiaryAndProduct>();
                foreach (var item in response.DetailInvoicesResponse.BeneficiaryAndProduct)
                {
                    BeneficiaryAndProduct viewModel = new BeneficiaryAndProduct
                    {
                        LineName = item.LineName,
                        ProductLine = item.ProductLine,
                    };
                    BeneficiaryAndProduct.Add(viewModel);
                }
            }
            else
            {
                await dialogService.ShowMessage(AppResources.TitleServicesHistory, "No se han encontrado resultados para tu búsqueda.");
            }
        }
        #endregion

        #region Constructor
        public InvoiceDetailPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService, IValidatorService validatorService, IPhoneService phoneService)
        {
            IsCO = AppConfigurations.Brand == "emi";
            IsUY = AppConfigurations.Brand == "ucm";

            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.validatorService = validatorService;
            this.phoneService = phoneService;

            invoicePageViewModel = ServiceLocator.Current.GetInstance<IInvoicesPageViewModel>();
        }
        #endregion
    }
}
