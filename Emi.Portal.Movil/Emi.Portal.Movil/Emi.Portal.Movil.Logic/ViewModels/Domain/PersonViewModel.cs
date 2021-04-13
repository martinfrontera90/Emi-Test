namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class PersonViewModel : ViewModelBase, IPersonViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;

        #region Properties
        private bool active;
        public bool Active
        {
            get { return active; }
            set
            {
                if (active != value)
                {
                    active = value;
                    RaisePropertyChanged("Active");
                }
            }
        }

        private AffiliateType affiliateType;
        public AffiliateType AffiliateType
        {
            get { return affiliateType; }
            set
            {
                if (affiliateType != value)
                {
                    affiliateType = value;
                    RaisePropertyChanged("AffiliateType");
                }
            }
        }

        private string fullDocument;

        public string FullDocument
        {
            get { return fullDocument; }
            set
            {
                if (fullDocument != value)
                {
                    fullDocument = value;
                    RaisePropertyChanged("FullDocument");
                }
            }
        }


        private bool beneficiary;
        public bool Beneficiary
        {
            get { return beneficiary; }
            set
            {
                if (beneficiary != value)
                {
                    beneficiary = value;
                    RaisePropertyChanged("Beneficiary");
                }
            }
        }

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

        private string documentTypeShort;
        public string DocumentTypeShort
        {
            get { return documentTypeShort; }
            set
            {
                if (documentTypeShort != value)
                {
                    documentTypeShort = value;
                    RaisePropertyChanged("DocumentTypeShort");
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

        private string errorNames;
        public string ErrorNames
        {
            get { return errorNames; }
            set
            {
                if (errorNames != value)
                {
                    errorNames = value;
                    RaisePropertyChanged("ErrorNames");
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

        private string errorSurnames;
        public string ErrorSurnames
        {
            get { return errorSurnames; }
            set
            {
                if (errorSurnames != value)
                {
                    errorSurnames = value;
                    RaisePropertyChanged("ErrorSurnames");
                }
            }
        }

        private string fullNames;
        public string FullNames
        {
            get { return fullNames; }
            set
            {
                if (fullNames != value)
                {
                    fullNames = value;
                    RaisePropertyChanged("FullNames");
                }
            }
        }


        private string idReference;
        public string IdReference
        {
            get { return idReference; }
            set
            {
                if (idReference != value)
                {
                    idReference = value;
                    RaisePropertyChanged("IdReference");
                }
            }
        }

        private bool isVisiblePersonalData;
        public bool IsVisiblePersonalData
        {
            get { return isVisiblePersonalData; }
            set
            {
                if (isVisiblePersonalData != value)
                {
                    isVisiblePersonalData = value;
                    RaisePropertyChanged("IsVisiblePersonalData");
                }
            }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set
            {
                if (names != value)
                {
                    names = value;
                    RaisePropertyChanged("Names");
                }
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                if (phone != value)
                {
                    phone = value;
                    RaisePropertyChanged("Phone");
                }
            }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }

        private string surnames;
        public string Surnames
        {
            get { return surnames; }
            set
            {
                if (surnames != value)
                {
                    surnames = value;
                    RaisePropertyChanged("Surnames");
                }
            }
        }

        private string cellPhone;
        public string CellPhone
        {
            get { return cellPhone; }
            set
            {
                if (cellPhone != value)
                {
                    cellPhone = value;
                    RaisePropertyChanged("CellPhone");
                }
            }
        }

        #endregion

        public ICommand InformationCommand { get { return new RelayCommand<string>(Information); } }
        public ICommand OptionsCommand { get { return new RelayCommand(Options); } }
        public ICommand UpdateMemberCommand { get { return new RelayCommand(UpdateMember); } }

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
        private async void UpdateMember()
        {
            if (ValidatePerson())
            {
                dialogService.ShowProgress();
                RequestUpdateMember requestUpdateMember = new RequestUpdateMember
                {
                    Document = Document,
                    DocumentType = DocumentType,
                    Email = Email,
                    Names = Names,
                    Phone = Phone,
                    Surnames = Surnames
                };
                ResponseUpdateMember responseUpdateMember = await apiService.UpdateMember(requestUpdateMember);
                ValidateResponseUpdateMember(responseUpdateMember);
                return;
            }
        }

        private bool ValidatePerson()
        {
            ErrorNames = string.IsNullOrEmpty(Names) ? AppResources.NameRequired : string.Empty;
            ErrorSurnames = string.IsNullOrEmpty(Surnames) ? AppResources.SunamesRequired : string.Empty;

            if (IsVisiblePersonalData)
            {
                ErrorEmail = ValidatorHelper.IsValidEmail(Email) ? string.Empty : AppResources.WriteValidEmail;
                ErrorPhone = ValidatorHelper.IsValidCellPhone(Phone) ? string.Empty : AppResources.InvalidPhone;
            }

            return string.IsNullOrEmpty(ErrorNames) && string.IsNullOrEmpty(ErrorSurnames) && string.IsNullOrEmpty(ErrorPhone) && string.IsNullOrEmpty(ErrorEmail);
        }

        private async void ValidateResponseUpdateMember(ResponseUpdateMember response)
        {
            dialogService.HideProgress();
            if (response.Success && response.StatusCode == 0)
            {
                IFamilyPageViewModel familyPageViewModel = ServiceLocator.Current.GetInstance<IFamilyPageViewModel>();
                await familyPageViewModel.LoadFamily();
                await navigationService.Back();
            }

            await dialogService.ShowMessage(response.Title, response.Message);
        }

        private async void Options()
        {
            string result = await dialogService.Family();

            if (result == AppResources.DeleteText)
            {
                if (await dialogService.ShowConfirm(AppResources.DeleteFamilyMemberTitle, AppResources.DeleteFamilyMemberMessage))
                {
                    dialogService.ShowProgress();
                    RequestRemoveMember requestRemoveMember = new RequestRemoveMember();
                    requestRemoveMember.Document = Document;
                    requestRemoveMember.DocumentType = DocumentType;
                    ResponseRemoveMember responseRemoveMember = await apiService.RemoveMember(requestRemoveMember);
                    ValidateResponseRemoveMember(responseRemoveMember);
                }
                return;
            }

            if (result == AppResources.EditText)
            {
                if (Active)
                {
                    await dialogService.ShowMessage(AppResources.TitleFamily, AppResources.DontEditFamilyMember);
                    return;
                }
                else
                {
                    IFamilyPageViewModel familyPageViewModel = ServiceLocator.Current.GetInstance<IFamilyPageViewModel>();
                    familyPageViewModel.MemberSelected = this;
                    await navigationService.Navigate(AppPages.EditFamilyPage);
                    return;
                }
            }
        }

        private async void ValidateResponseRemoveMember(ResponseRemoveMember response)
        {
            dialogService.HideProgress();
            if (response.Success && response.StatusCode == 0)
            {
                IFamilyPageViewModel familyPageViewModel = ServiceLocator.Current.GetInstance<IFamilyPageViewModel>();
                await familyPageViewModel.LoadFamily();
            }

            await dialogService.ShowMessage(response.Title, response.Message);
        }

        public PersonViewModel()
        {
            apiService = ServiceLocator.Current.GetInstance<IApiService>();
            dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }
    }
}
