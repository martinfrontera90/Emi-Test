namespace Emi.Portal.Movil.Logic.ViewModels.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.LegalContent;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Microsoft.AppCenter.Analytics;
    using CommonServiceLocator;
    using Xamarin.Forms;

    public class RegisterPageViewModel : ViewModelBase, IRegisterPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IValidatorService validatorService;

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                    RaisePropertyChanged("BirthDate");
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

        private string codeVerification;
        public string CodeVerification
        {
            get { return codeVerification; }
            set
            {
                if (codeVerification != value)
                {
                    codeVerification = value;
                    RaisePropertyChanged("CodeVerification");
                }
            }
        }

        private string confirmationEmail;
        public string ConfirmationEmail
        {
            get { return confirmationEmail; }
            set
            {
                confirmationEmail = value;
                RaisePropertyChanged("ConfirmationEmail");
            }
        }

        private string confirmationPassword;
        public string ConfirmationPassword
        {
            get { return confirmationPassword; }
            set
            {
                if (confirmationPassword != value)
                {
                    confirmationPassword = value;
                    RaisePropertyChanged("ConfirmationPassword");
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
                    ChangeTypeKeyboard();
                }
            }
        }

        private string documentNumber;
        public string DocumentNumber
        {
            get { return documentNumber; }
            set
            {
                if (documentNumber != value)
                {
                    documentNumber = value;
                    RaisePropertyChanged("DocumentNumber");
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        private string errorName;
        public string ErrorName
        {
            get { return errorName; }
            set
            {
                if (errorName != value)
                {
                    errorName = value;
                    RaisePropertyChanged("ErrorName");
                }
            }
        }

        private string errorSurName;
        public string ErrorLastName
        {
            get { return errorSurName; }
            set
            {
                if (errorSurName != value)
                {
                    errorSurName = value;
                    RaisePropertyChanged("ErrorLastName");
                }
            }
        }

        private string errorPassword;
        public string ErrorPassword
        {
            get { return errorPassword; }
            set
            {
                if (ErrorPassword != value)
                {
                    errorPassword = value;
                    RaisePropertyChanged("ErrorPassword");
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
                };
            }
        }

        private string errorPhone;
        public string ErrorPhone
        {
            get { return errorPhone; }
            set
            {
                if (ErrorPhone != value)
                {
                    errorPhone = value;
                    RaisePropertyChanged("ErrorPhone");
                }
            }
        }


        private string errorConfirmationPassword;
        public string ErrorConfirmationPassword
        {
            get { return errorConfirmationPassword; }
            set
            {
                if (errorConfirmationPassword != value)
                {
                    errorConfirmationPassword = value;
                    RaisePropertyChanged("ErrorConfirmationPassword");
                }
            }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                RaisePropertyChanged("FullName");
            }
        }

        private bool isTermsAndConditions;
        public bool IsTermsAndConditions
        {
            get { return isTermsAndConditions; }
            set
            {
                isTermsAndConditions = value;
                RaisePropertyChanged("IsTermsAndConditions");
            }
        }

        private bool isEnabledEditName;
        public bool IsEnabledEditName
        {
            get { return isEnabledEditName; }
            set
            {
                if (isEnabledEditName != value)
                {
                    isEnabledEditName = value;
                    RaisePropertyChanged("IsEnabledEditName");
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

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    RaisePropertyChanged("Password");
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

        private string errorDocumentSelected;
        public string ErrorDocumentSelected
        {
            get { return errorDocumentSelected; }
            set
            {
                if (errorDocumentSelected != value)
                {
                    errorDocumentSelected = value;
                    RaisePropertyChanged("ErrorDocumentSelected");
                }
            }
        }

        private string errorDocumentNumber;
        public string ErrorDocumentNumber
        {
            get { return errorDocumentNumber; }
            set
            {
                if (errorDocumentNumber != value)
                {
                    errorDocumentNumber = value;
                    RaisePropertyChanged("ErrorDocumentNumber");
                }
            }
        }

        private string errorEmail;
        public string ErrorEmail
        {
            get { return errorEmail; }
            set
            {
                errorEmail = value;
                RaisePropertyChanged("ErrorEmail");
            }
        }

        private string errorConfirmationEmail;
        public string ErrorConfirmationEmail
        {
            get { return errorConfirmationEmail; }
            set
            {
                errorConfirmationEmail = value;
                RaisePropertyChanged("ErrorConfirmationEmail");
            }
        }

        private string typeKeyboard;
        public string TypeKeyboard
        {
            get { return typeKeyboard; }
            set
            {
                if (typeKeyboard != value)
                {
                    typeKeyboard = value;
                    RaisePropertyChanged("TypeKeyboard");
                }
            }
        }

        private string nameOne;
        public string NameOne
        {
            get { return nameOne; }
            set
            {
                if (nameOne != value)
                {
                    nameOne = value;
                    RaisePropertyChanged("NameOne");
                }
            }
        }

        private string nameTwo;
        public string NameTwo
        {
            get { return nameTwo; }
            set
            {
                if (nameTwo != value)
                {
                    nameTwo = value;
                    RaisePropertyChanged("NameTwo");
                }
            }
        }

        private string lastNameOne;
        public string LastNameOne
        {
            get { return lastNameOne; }
            set
            {

                if (lastNameOne != value)
                {
                    lastNameOne = value;
                    RaisePropertyChanged("LastNameOne");
                }
            }
        }

        private string lastNameTwo;
        public string LastNameTwo
        {
            get { return lastNameTwo; }
            set
            {

                if (lastNameTwo != value)
                {
                    lastNameTwo = value;
                    RaisePropertyChanged("LastNameTwo");
                }
            }
        }

        private string descriptionPage;
        public string DescriptionPage
        {
            get { return descriptionPage; }
            set
            {
                if (descriptionPage != value)
                {
                    descriptionPage = value;
                    RaisePropertyChanged("DescriptionPage");
                }
            }
        }

        private string tittlePage;
        public string TittlePage
        {
            get { return tittlePage; }
            set
            {
                if (tittlePage != value)
                {
                    tittlePage = value;
                    RaisePropertyChanged("TittlePage");
                }
            }
        }

        private bool userExist;
        public bool UserExist
        {
            get { return userExist; }
            set
            {
                if (userExist != value)
                {
                    userExist = value;
                    RaisePropertyChanged("UserExist");
                }
            }
        }

        private bool updateEmail;
        public bool UpdateEmail
        {
            get { return updateEmail; }
            set
            {
                if (updateEmail != value)
                {
                    updateEmail = value;
                    RaisePropertyChanged("UpdateEmail");
                }
            }
        }

        private ObservableCollection<Gender> genders;
        public ObservableCollection<Gender> Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                RaisePropertyChanged("Genders");
            }
        }

        private Gender genderSelected;

        public Gender GenderSelected
        {
            get { return genderSelected; }
            set
            {
                if (genderSelected != value)
                {
                    genderSelected = value;
                    RaisePropertyChanged("GenderSelected");
                }
            }
        }

        private string titleLegalContent;
        public string TitleLegalContent
        {
            get { return titleLegalContent; }
            set
            {
                if (titleLegalContent != value)
                {
                    titleLegalContent = value;
                    RaisePropertyChanged("TitleLegalContent");
                }
            }
        }

        private HtmlWebViewSource htmlSource;
        public HtmlWebViewSource HtmlSource
        {
            get { return htmlSource; }
            set
            {
                if (htmlSource != value)
                {
                    htmlSource = value;
                    RaisePropertyChanged("HtmlSource");
                }
            }
        }


        public ObservableCollection<Document> Documents { get; set; }

        private ObservableCollection<Departament> departaments;
        public ObservableCollection<Departament> Departaments
        {
            get { return departaments; }
            set
            {
                if (departaments != value)
                {
                    departaments = value;
                    RaisePropertyChanged("Departaments");
                }
            }
        }

        private Departament departamentSelected;
        public Departament DepartamentSelected
        {
            get { return departamentSelected; }
            set
            {
                if (departamentSelected != value)
                {
                    departamentSelected = value;
                    RaisePropertyChanged("DepartamentSelected");
                    if (DepartamentSelected != null)
                    {
                        LoadCities();
                    }

                    IsEnabledCities = DepartamentSelected != null && DepartamentSelected.Code != "009";
                }
            }
        }

        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities
        {
            get { return cities; }
            set
            {
                if (cities != value)
                {
                    cities = value;
                    RaisePropertyChanged("Cities");
                }
            }
        }

        private City citySelected;
        public City CitySelected
        {
            get { return citySelected; }
            set
            {
                if (citySelected != value)
                {
                    citySelected = value;
                    RaisePropertyChanged("CitySelected");
                }
            }
        }

        private bool isEnabledCities;
        public bool IsEnabledCities
        {
            get { return isEnabledCities; }
            set
            {
                if (isEnabledCities != value)
                {
                    isEnabledCities = value;
                    RaisePropertyChanged("IsEnabledCities");
                }
            }
        }

        private string errorDepartment;
        public string ErrorDepartment
        {
            get { return errorDepartment; }
            set
            {
                if (errorDepartment != value)
                {
                    errorDepartment = value;
                    RaisePropertyChanged("ErrorDepartment");
                }
            }
        }

        private string errorCity;
        public string ErrorCity
        {
            get { return errorCity; }
            set
            {
                if (errorCity != value)
                {
                    errorCity = value;
                    RaisePropertyChanged("ErrorCity");
                }
            }
        }

        #endregion

        #region Commands        
        public ICommand NextToDataPersonalCommand { get { return new RelayCommand(NextToDataPersonal); } }
        public ICommand NextToNameCommand { get { return new RelayCommand(NextToName); } }
        public ICommand NextToPasswordCommand { get { return new RelayCommand(NextToPassword); } }
        public ICommand CompleteCommand { get { return new RelayCommand(Complete); } }
        public ICommand SendVerificationCodeCommand { get { return new RelayCommand(SendVerificationCode); } }
        public ICommand VerificationCodeCommand { get { return new RelayCommand(VerificationCode); } }
        public ICommand TermsAndConditionsCommmand { get { return new RelayCommand(TermsAndConditions); } }
        public ICommand DataManagementPolicyCommand { get { return new RelayCommand(DataManagementPolicy); } }
        public ICommand InformationCommand { get { return new RelayCommand<string>(Information); } }
        public ICommand CancelCommand { get { return new RelayCommand(Cancel); } }

        public async void Cancel()
        {
            if (await dialogService.ShowConfirm("Registro", "¿Desea cancelar el registro?"))
            {
                if (DocumentSelected != null && !string.IsNullOrEmpty(DocumentNumber) && !string.IsNullOrEmpty(Email))
                {
                    RequestCancelPreRegister request = new RequestCancelPreRegister
                    {
                        DocumentType = DocumentSelected.Code,
                        Document = DocumentNumber,
                        Email = Email,
                        PhoneNumber = CellPhone
                    };
                    await apiService.CancelPreRegister(request); 
                }
                Analytics.TrackEvent(AppTrackEvent.CancelRegister);
                await navigationService.Navigate(AppPages.LoginPage);
            }
        }

        private async void Information(string option)
        {
            int value = int.Parse(option);
            switch (value)
            {
                case 1:
                    await dialogService.ShowMessage(string.Empty, "No se pueden registrar menores de edad.");
                    break;
                case 2:
                    await dialogService.ShowMessage(string.Empty, AppResources.CellPhoneConditions);
                    break;
                case 3:
                    await dialogService.ShowMessage(string.Empty, AppResources.CellConditions);
                    break;
                case 4:
                    await dialogService.ShowMessage(string.Empty, AppResources.EmailDescription);
                    break;
            }
        }
        #endregion

        #region Methods
        private async void DataManagementPolicy()
        {
            ChangeIconLegalContent();
            await LoadContentLegal(AppConfigurations.TagPPC);
            await navigationService.Navigate(AppPages.DataManagementPolicyPage);
        }

        private async void TermsAndConditions()
        {
            ChangeIconLegalContent();
            await LoadContentLegal(AppConfigurations.TagTYCC);
            await navigationService.Navigate(AppPages.TermsAndConditionsPage);
        }

        private void ChangeIconLegalContent()
        {
            ILegalContentPageViewModel legalContentPageViewModel = ServiceLocator.Current.GetInstance<ILegalContentPageViewModel>();
            legalContentPageViewModel.Icon = "ic_close";
            legalContentPageViewModel.FromRegister = true;
        }

        public async Task LoadContentLegal(string Tag)
        {
            dialogService.ShowProgress();
            HtmlSource.Html = string.Empty;            
            RequestLegalContent request = new RequestLegalContent
            {
                Tag = Tag
            };
            ResponseLegalContent response = await apiService.GetLegalContent(request);
            dialogService.HideProgress();
            if (response.Success && response.StatusCode == 0)
            {
                TitleLegalContent = Tag == AppConfigurations.TagPPC ? "Política de tratamiento de datos" : "Términos y condiciones de uso";                
            }
            else
            {
                await dialogService.ShowMessage(response.Title, response.Message);
                await navigationService.Navigate(AppPages.LandingPage);
            }
        }

        public async Task LoadDocuments()
        {
            dialogService.ShowProgress();
            ResponseDocumentRegister response = await apiService.GetDocumentTypesRegister(new RequestDocumentRegister());
            dialogService.HideProgress();
            Documents.Clear();

            if (response.Success == false || response.StatusCode != 0)
            {
                await dialogService.ShowMessage(response.Title, !string.IsNullOrEmpty(response.Message) ? response.Message : response.ErrorMessage);
                return;
            }

            foreach (Document item in response.Documents)
                Documents.Add(item);
        }
        private async void Complete()
        {
            ValidatePasswords();
            if (string.IsNullOrEmpty(ErrorPassword) && string.IsNullOrEmpty(ErrorConfirmationPassword))
            {
                dialogService.ShowProgress();
                Register register = new Register();
                ViewModelHelper.SetRegisterViemModelToRegister(this, register);
                RequestRegister request = new RequestRegister { Register = register };
                ResponsePreRegister response = await apiService.Register(request);
                dialogService.HideProgress();
                ValidateRegister(response);
            }
        }
        private async void ValidateRegister(ResponsePreRegister response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);
            if (response.Success && response.StatusCode == 0)
            {
                await navigationService.Navigate(AppPages.LoginPage);
            }
        }
        public async void NextToDataPersonal()
        {
            ValidateFullName();
            if (string.IsNullOrEmpty(ErrorName) && string.IsNullOrEmpty(ErrorLastName))
                await navigationService.Navigate(AppPages.RegisterDataPersonalPage);
        }
        private async void NextToName()
        {
            ValidateDocumenteSelected();
            ValidateDocumentNumber();
            ValidateEmails();

            if (string.IsNullOrEmpty(ErrorDocumentSelected) && string.IsNullOrEmpty(ErrorDocumentNumber) && string.IsNullOrEmpty(ErrorEmail) && string.IsNullOrEmpty(ErrorConfirmationEmail))
            {
                if (!IsTermsAndConditions)
                    await dialogService.ShowMessage(string.Empty, AppResources.AcceptTermsConditions);
                else
                    ValidateUserPLS();
            }
        }

        private async void NextToPassword()
        {
            ValidatePage();
            ValidateCellPhoneNumbers();
            ValidateGender();
            FullName = string.Format("{0} {1}", NameOne, LastNameOne);
            if (string.IsNullOrEmpty(ErrorCellPhone) && string.IsNullOrEmpty(ErrorPhone)
                && string.IsNullOrEmpty(ErrorDepartment) && string.IsNullOrEmpty(ErrorCity))
                await navigationService.Navigate(AppPages.RegisterPasswordPage);
        }

        private async void SendVerificationCode()
        {
            dialogService.ShowProgress();
            Register register = new Register();
            ViewModelHelper.SetRegisterViemModelToRegister(this, register);
            RequestSendVerificationCode request = new RequestSendVerificationCode { Register = register };
            ResponsePreRegister responsePreRegister = await apiService.SendVerificationCode(request);
            dialogService.HideProgress();
            ValidateSendVerificationCode(responsePreRegister);
        }

        private void ValidatePage()
        {
            ErrorDepartment = DepartamentSelected == null ? AppResources.DepartmentRequired : string.Empty;
            ErrorCity = CitySelected == null ? string.Format(AppResources.CityRequired, "ciudad") : string.Empty;
        }

        private void ValidateCellPhoneNumbers()
        {
            if (string.IsNullOrEmpty(Phone))
            {
                ErrorPhone = string.Empty;
            }
            else
            {
                ErrorPhone = ValidatorHelper.IsValidPhone(Phone) ? string.Empty : AppResources.InvalidPhone;
            }

            if (string.IsNullOrEmpty(CellPhone))
            {
                ErrorCellPhone = AppResources.CellPhoneRequired;
                return;
            }

            ErrorCellPhone = ValidatorHelper.IsValidCellPhone(CellPhone) ? string.Empty : AppResources.InvalidPhone;
        }
        private void ValidateDocumentNumber()
        {
            ErrorDocumentNumber = string.IsNullOrEmpty(DocumentNumber) ? AppResources.DocumentRequired : string.Empty;
            if (string.IsNullOrEmpty(ErrorDocumentNumber) && DocumentSelected != null && DocumentSelected.Name != "Pasaporte")
            {
                long result;
                ErrorDocumentNumber = long.TryParse(DocumentNumber, out result) ? string.Empty : "Número de documento no válido.";
            }
        }
        private void ValidateDocumenteSelected()
        {
            ErrorDocumentSelected = DocumentSelected != null ? string.Empty : AppResources.DocumentTypeRequired;
        }
        private async void ValidateGender()
        {
            if (GenderSelected == null)
                await dialogService.ShowMessage("error", "Seleccionar género");
        }
        private void ValidatePasswords()
        {
            ErrorPassword = ErrorConfirmationPassword = string.Empty;
            if (string.IsNullOrEmpty(Password))
            {
                ErrorPassword = AppResources.PasswordRequired;
                return;
            }

            if (Password.Length < 8)
            {
                ErrorPassword = AppResources.PasswordLength;
                return;
            }

            if (string.IsNullOrEmpty(ConfirmationPassword))
            {
                ErrorConfirmationPassword = AppResources.PasswordRequired;
                return;
            }
            ErrorConfirmationPassword = ValidatorHelper.IsEqualData(Password, ConfirmationPassword) ? string.Empty : AppResources.DifferentPassword;
        }
        private async void ValidateUserPLS()
        {
            dialogService.ShowProgress();
            Register register = new Register();

            ViewModelHelper.SetRegisterViemModelToRegister(this, register);
            RequestPreRegister request = new RequestPreRegister { Register = register };
            ResponsePreRegister responsePreRegister = await apiService.PreRegister(request);
            dialogService.HideProgress();
            ValidateResponsePLS(responsePreRegister);
        }
        private async void ValidateResponsePLS(ResponsePreRegister responsePreRegister)
        {
            if (responsePreRegister.StatusCode == 14)
            {
                LoadFullName(responsePreRegister.UserNames);
                await navigationService.Navigate(AppPages.RegisterUpdateCellPhonePage);
                return;
            }

            if (responsePreRegister.StatusCode != 0)
            {
                //Validacion usuario ya registrado y activo codigo 11 
                if (responsePreRegister.StatusCode == 11)
                {
                    bool result = await dialogService.ShowConfirm(responsePreRegister.Title, responsePreRegister.Message);

                    if (result)
                    {
                        IRememberPasswordPageViewModel rememberPassword = ServiceLocator.Current.GetInstance<IRememberPasswordPageViewModel>();
                        rememberPassword.Email = Email;
                        await rememberPassword.ValidateUser();
                    }

                }
                else
                {
                    await dialogService.ShowMessage(responsePreRegister.Title, responsePreRegister.Message);

                }


            }

            if (responsePreRegister.StatusCode == 0 && responsePreRegister.UserNames.DataLoaded)
            {
                LoadFullName(responsePreRegister.UserNames);
                TittlePage = "Verificar";
                DescriptionPage = string.Format("Para continuar tu registro, verifica datos de: {0} {1} {2} {3}", NameOne, NameTwo, LastNameOne, LastNameTwo);
                IsEnabledEditName = DocumentSelected.Name == "Pasaporte";
                await navigationService.Navigate(AppPages.RegisterNamePage);
                return;
            }

            if (responsePreRegister.StatusCode == 0 && !responsePreRegister.UserNames.DataLoaded)
            {
                TittlePage = "Registro";
                DescriptionPage = "Ingresa los siguientes datos para completar el registro.";
                IsEnabledEditName = true;
                await navigationService.Navigate(AppPages.RegisterNamePage);
                return;
            }

        }
        private void LoadFullName(UserNames userNames, bool register = false)
        {
            if (userNames != null)
            {
                NameOne = userNames.FirstName != null ? userNames.FirstName.ToString() : string.Empty;
                NameTwo = userNames.SecondName != null ? userNames.SecondName.ToString() : string.Empty;
                LastNameOne = userNames.FirstSurname != null ? userNames.FirstSurname.ToString() : string.Empty;
                LastNameTwo = userNames.SecondSurname != null ? userNames.SecondSurname.ToString() : string.Empty;
            }
        }
        private void ValidateEmails()
        {
            if (string.IsNullOrEmpty(Email))
            {
                ErrorEmail = AppResources.MailRequired;
                return;
            }

            ErrorEmail = ValidatorHelper.IsValidEmail(Email) ? string.Empty : AppResources.WriteValidEmail;

            if (string.IsNullOrEmpty(ConfirmationEmail))
            {
                ErrorConfirmationEmail = AppResources.MailRequired;
                return;
            }

            ErrorConfirmationEmail = ValidatorHelper.IsValidEmail(ConfirmationEmail) ? string.Empty : AppResources.WriteValidEmail;
            ErrorConfirmationEmail = ValidatorHelper.IsEqualData(Email, ConfirmationEmail) ? ErrorConfirmationEmail : AppResources.DifferentEmail;
        }
        private void ValidateFullName()
        {
            ErrorName = string.IsNullOrEmpty(NameOne) ? "El primer nombre es obligatorio." : string.Empty;
            ErrorLastName = string.IsNullOrEmpty(LastNameOne) ? "El primer apellido es obligatorio." : string.Empty;
        }
        private async void VerificationCode()
        {
            dialogService.ShowProgress();
            Register register = new Register();
            ViewModelHelper.SetRegisterViemModelToRegister(this, register);
            RequestRegisterUpdateEmail request = new RequestRegisterUpdateEmail { Register = register };
            ResponsePreRegister responsePreRegister = await apiService.RegisterUpdateEmail(request);
            dialogService.HideProgress();
            ValidateVerificationCode(responsePreRegister);
        }
        private async void ValidateVerificationCode(ResponsePreRegister responsePreRegister)
        {
            if (responsePreRegister.StatusCode == 9)
            {
                await dialogService.ShowMessage(responsePreRegister.Title, responsePreRegister.Message);
            }
            else
                await navigationService.Navigate(AppPages.RegisterNamePage);
        }
        private async void ValidateSendVerificationCode(ResponsePreRegister responsePreRegister)
        {
            UpdateEmail = false;
            if (responsePreRegister.Success && responsePreRegister.StatusCode == 0)
            {
                UpdateEmail = true;
                await navigationService.Navigate(AppPages.RegisterVerificationCodePage);
                return;
            }

            await dialogService.ShowMessage(responsePreRegister.Title, responsePreRegister.Message);
        }
        //TODO: NO esta funcionando el binding
        private void ChangeTypeKeyboard()
        {
            TypeKeyboard = KeyboardType.Numeric.ToString();
            if (DocumentSelected != null && DocumentSelected.Code == DocumentType.Passport.GetHashCode().ToString())
                TypeKeyboard = KeyboardType.Numeric.ToString();
        }
        public void Clean()
        {
            IsTermsAndConditions = false;
            BirthDate = DateTime.Now;
            ConfirmationEmail =
            ConfirmationPassword =
            DocumentNumber =
            CellPhone =
            Email =
            ErrorCellPhone =
            ErrorConfirmationEmail =
            ErrorConfirmationPassword =
            ErrorDocumentNumber =
            ErrorDocumentSelected =
            ErrorEmail =
            ErrorName =
            ErrorPassword =
            ErrorPhone =
            LastNameOne =
            LastNameTwo =
            NameOne =
            NameTwo =
            Password =
            string.Empty;

            GenderSelected = null;

            Departaments = new ObservableCollection<Departament>();
            Cities = new ObservableCollection<City>();
        }

        private async void ValidateResponseDepartments(ResponseDepartments response)
        {
            Departaments = new ObservableCollection<Departament>();

            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            Departaments = new ObservableCollection<Departament>(response.Departaments);
        }

        private async void LoadCities()
        {
            dialogService.ShowProgress();
            RequestCities request = new RequestCities
            {
                DepartamentCode = DepartamentSelected.Code
            };
            ResponseCities response = await apiService.GetCities(request);
            ValidateResponseCities(response);
            dialogService.HideProgress();
        }

        private async void ValidateResponseCities(ResponseCities response)
        {
            Cities = new ObservableCollection<City>();

            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            Cities = new ObservableCollection<City>(response.Cities);
        }

        #endregion

        #region Constructor
        public RegisterPageViewModel(INavigationService navigationService, IApiService apiService, IDialogService dialogService, IValidatorService validatorService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.validatorService = validatorService;
            TypeKeyboard = KeyboardType.Numeric.ToString();
            HtmlSource = new HtmlWebViewSource();

            DateTime currentDate = DateTime.Now;
            MaximumDate = currentDate.AddYears(-18);
            MinimumDate = new DateTime(1900, 1, 1);
            BirthDate = MaximumDate;
            Documents = new ObservableCollection<Document>();
            LoadGenders();
        }

        public async void LoadGenders()
        {
            try
            {
                var response = await apiService.GetGenders();
                if (response.Success)
                {
                    Genders = new ObservableCollection<Gender>(response.Genders);
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task LoadPage()
        {
            IsEnabledCities = false;
            ResponseDepartments response = await apiService.GetDepartments(new RequestDepartments());
            ValidateResponseDepartments(response);
        }

        #endregion
    }
}
