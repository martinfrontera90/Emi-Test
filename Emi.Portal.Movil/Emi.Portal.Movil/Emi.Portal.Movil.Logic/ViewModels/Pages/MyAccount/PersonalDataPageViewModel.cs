namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class PersonalDataPageViewModel : ViewModelBase, IPersonalDataPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IPhoneService phoneService;
        IValidatorService validatorService;

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

        private double dataCoveragePercentage;
        public double DataCoveragePercentage
        {
            get { return dataCoveragePercentage; }
            set
            {
                if (dataCoveragePercentage != value)
                {
                    dataCoveragePercentage = value;
                    RaisePropertyChanged("DataCoveragePercentage");
                }
            }
        }

        public ObservableCollection<Document> Documents { get; set; }

        private string document;
        public string Document
        {
            get { return document; }
            set
            {
                if (document != value)
                {
                    document = value;
                    RaisePropertyChanged("Document");
                }
            }
        }

        private Document documentSelected;
        public Document DocumentSelected
        {
            get { return documentSelected; }
            set
            {
                if (documentSelected != value)
                {
                    documentSelected = value;
                    RaisePropertyChanged("DocumentSelected");
                }
            }
        }

        private string documentType;
        public string DocumentType
        {
            get { return documentType; }
            set
            {
                if (documentType != value)
                {
                    documentType = value;
                    RaisePropertyChanged("DocumentType");
                }
            }
        }

        private string cellPhoneNumber;
        public string CellPhoneNumber
        {
            get { return cellPhoneNumber; }
            set
            {
                if (cellPhoneNumber != value)
                {
                    cellPhoneNumber = value;
                    RaisePropertyChanged("CellPhoneNumber");
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }

        private string errorCellPhone;
        public string ErrorCellPhone
        {
            get { return errorCellPhone; }
            set
            {
                if (errorCellPhone != value)
                {
                    errorCellPhone = value;
                    RaisePropertyChanged("ErrorCellPhone");
                }
            }
        }

        private string errorPhone;
        public string ErrorPhone
        {
            get { return errorPhone; }
            set
            {
                if (errorPhone != value)
                {
                    errorPhone = value;
                    RaisePropertyChanged("ErrorPhone");
                }
            }
        }

        private string errorEmail;
        public string ErrorEmail
        {
            get { return errorEmail; }
            set
            {
                if (errorEmail != value)
                {
                    errorEmail = value;
                    RaisePropertyChanged("ErrorEmail");
                }
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }

        private string firstSurname;
        public string FirstSurname
        {
            get { return firstSurname; }
            set
            {
                if (firstSurname != value)
                {
                    firstSurname = value;
                    RaisePropertyChanged("FirstSurname");
                }
            }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    RaisePropertyChanged("IsEnabled");
                }
            }
        }

        private double percentage;
        public double Percentage
        {
            get { return percentage; }
            set
            {
                if (percentage != value)
                {
                    percentage = value;
                    RaisePropertyChanged("Percentage");
                }
            }
        }

        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                if (secondName != value)
                {
                    secondName = value;
                    RaisePropertyChanged("SecondName");
                }
            }
        }

        private string secondSurname;
        public string SecondSurname
        {
            get { return secondSurname; }
            set
            {
                if (secondSurname != value)
                {
                    secondSurname = value;
                    RaisePropertyChanged("SecondSurname");
                }
            }
        }
        #endregion

        #region Commands

        public ICommand CancelUpdateCommand { get { return new RelayCommand(CancelUpdate); } }
        public ICommand InformationCommand { get { return new RelayCommand<string>(Information); } }
        public ICommand UpdateCommand { get { return new RelayCommand(Update); } }
        #endregion

        #region Methods
        private async void Information(string option)
        {

            int value = int.Parse(option);
            switch (value)
            {
                case 1:
                    await dialogService.ShowMessage(string.Empty, AppResources.CellPhoneConditions);
                    break;
                case 2:
                    await dialogService.ShowMessage(string.Empty, AppResources.EmailCondition);
                    break;
            }

        }

        public async void CancelUpdate()
        {
            await navigationService.Navigate(AppPages.LandingPage);
        }
        public async void Update()
        {
            if (ValidateData())
            {
                dialogService.ShowProgress();
                RequestUpdateAffiliate request = new RequestUpdateAffiliate
                {
                    CellPhoneNumber = CellPhoneNumber,
                    Email = Email,
                };
                ResponseUpdateAffiliate response = await apiService.UpdateAffiliate(request);
                dialogService.HideProgress();
                ValidateResponseUpdateAffilite(response);
            }
        }
        private bool ValidateData()
        {
            ErrorCellPhone = string.IsNullOrEmpty(CellPhoneNumber) ? AppResources.CellPhoneRequired : string.Empty;
            ErrorEmail = string.IsNullOrEmpty(Email) ? AppResources.MailRequired : string.Empty;
            ErrorPhone = string.IsNullOrEmpty(Email) ? AppResources.PhoneRequired : string.Empty;

            if (string.IsNullOrEmpty(ErrorCellPhone) && string.IsNullOrEmpty(ErrorEmail))
            {
                ErrorEmail = ValidatorHelper.IsValidEmail(Email) ? string.Empty : AppResources.WriteValidEmail;
                ErrorCellPhone = ValidatorHelper.IsValidCellPhone(CellPhoneNumber) ? string.Empty : AppResources.InvalidPhone;
                //ErrorPhone = ValidatorHelper.IsValidPhone(Phone) ? string.Empty : AppResources.InvalidPhone;
            }

            return string.IsNullOrEmpty(ErrorEmail) && string.IsNullOrEmpty(ErrorCellPhone);
        }
        private async void ValidateResponseUpdateAffilite(ResponseUpdateAffiliate response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);
            if (response.Success && response.StatusCode == 0)
            {
                await LoadPersonalData();
                return;
            }
        }
        public async Task LoadPersonalData()
        {
            ErrorEmail = ErrorCellPhone = string.Empty;
            dialogService.ShowProgress();
            RequestAffiliate request = new RequestAffiliate();
            ResponseAffiliate response = await apiService.GetAffiliate(request);

            if (await ValidateResponseAffiliate(response))
            {
                RequestDocument requestDocument = new RequestDocument();
                ResponseDocuments responseDocuments = await apiService.GetDocuments(requestDocument);
                ValidateResponseDocuments(responseDocuments);
                dialogService.HideProgress();
            }
            else
            {
                dialogService.HideProgress();
                await navigationService.Back();
            }

        }
        private async void ValidateResponseDocuments(ResponseDocuments response)
        {
            Documents.Clear();
            if (response.Success)
            {
                Documents.Add(response.Documents.Where(x => x.Code == DocumentType).FirstOrDefault());
                DocumentSelected = response.Documents.Where(x => x.Code == DocumentType).FirstOrDefault();
                return;
            }

            await dialogService.ShowMessage(response.Title, response.Message);
            await navigationService.Back();
        }
        private async Task<bool> ValidateResponseAffiliate(ResponseAffiliate response)
        {
            if (await validatorService.ValidateResponse(response) == false)
            {
                return false;
            }

            ViewModelHelper.SetAffiliateToPersonalDataViewModel(this, response.PersonalData);
            return true;
        }
        #endregion

        #region Constructor
        public PersonalDataPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService, IPhoneService phoneService, IValidatorService validatorService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.phoneService = phoneService;
            this.validatorService = validatorService;

            IsEnabled = false;

            Documents = new ObservableCollection<Document>();
        }
        #endregion

    }
}
