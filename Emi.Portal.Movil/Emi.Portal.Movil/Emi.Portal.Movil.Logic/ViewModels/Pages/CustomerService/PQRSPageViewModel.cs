namespace Emi.Portal.Movil.Logic.ViewModels.Pages.CustomerService
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System.Linq;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Helpers;

    public class PQRSPageViewModel : ViewModelBase, IPQRSPageViewModel
    {

        INavigationService navigationService;
        IApiService apiService;
        IDialogService dialogService;
        ILoginViewModel loginViewModel;


        public ResponsePQRSValidateUser PQRSUser { get; set; }

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

        private ObservableCollection<EventType> eventType;
        public ObservableCollection<EventType> EventType
        {
            get { return eventType; }
            set
            {
                eventType = value;
                RaisePropertyChanged(nameof(EventType));
            }
        }

        private string titlePQRS;
        public string TitlePQRS
        {
            get { return titlePQRS; }
            set
            {
                titlePQRS = value;
                RaisePropertyChanged(nameof(TitlePQRS));
            }
        }

        private bool isVisibleHelpText;
        public bool IsVisibleHelpText
        {
            get { return isVisibleHelpText; }
            set
            {
                isVisibleHelpText = value;
                RaisePropertyChanged(nameof(IsVisibleHelpText));
            }
        }

        private string helpText;
        public string HelpText
        {
            get { return helpText; }
            set
            {
                helpText = value;
                RaisePropertyChanged(nameof(HelpText));
            }
        }

        private bool informationFile;
        public bool InformationFile
        {
            get { return informationFile; }
            set
            {
                informationFile = value;
                RaisePropertyChanged(nameof(InformationFile));
            }
        }

        private bool hasBankAccount;
        public bool HasBankAccount
        {
            get { return hasBankAccount; }
            set
            {
                hasBankAccount = value;
                RaisePropertyChanged(nameof(HasBankAccount));
            }
        }

        private string termsAndConditionsText;
        public string TermsAndConditionsText
        {
            get { return termsAndConditionsText; }
            set
            {
                termsAndConditionsText = value;
                RaisePropertyChanged(nameof(TermsAndConditionsText));
            }
        }

        private ObservableCollection<TracingPQR> pQRSInCourse;
        public ObservableCollection<TracingPQR> PQRSInCourse
        {
            get { return pQRSInCourse; }
            set
            {
                pQRSInCourse = value;
                RaisePropertyChanged(nameof(PQRSInCourse));
            }
        }

        private ObservableCollection<DocumentRequiredViewModel> documentsRequired;
        public ObservableCollection<DocumentRequiredViewModel> DocumentsRequired
        {
            get { return documentsRequired; }
            set
            {
                documentsRequired = value;
                RaisePropertyChanged(nameof(DocumentsRequired));
            }
        }

        private ObservableCollection<RefundType> refundMotive;
        public ObservableCollection<RefundType> RefundMotive
        {
            get { return refundMotive; }
            set
            {
                refundMotive = value;
                RaisePropertyChanged(nameof(RefundMotive));
            }
        }

        private bool isVisibleInformationDate = false;
        public bool IsVisibleInformationDate
        {
            get { return isVisibleInformationDate; }
            set
            {
                isVisibleInformationDate = value;
                RaisePropertyChanged(nameof(IsVisibleInformationDate));
            }
        }

        private string errorOfficial;
        public string ErrorOfficial
        {
            get { return errorOfficial; }
            set
            {
                errorOfficial = value;
                RaisePropertyChanged(nameof(ErrorOfficial));
            }
        }

        private bool isVisibleDecease;
        public bool IsVisibleDecease
        {
            get { return isVisibleDecease; }
            set
            {
                isVisibleDecease = value;
                RaisePropertyChanged(nameof(IsVisibleDecease));
            }
        }

        private int charactersAvailable = 1000;
        public int CharactersAvailable
        {
            get { return charactersAvailable; }
            set
            {
                charactersAvailable = value;
                RaisePropertyChanged(nameof(CharactersAvailable));
            }
        }

        private ObservableCollection<Area> areas;
        public ObservableCollection<Area> Areas
        {
            get { return areas; }
            set
            {
                areas = value;
                RaisePropertyChanged(nameof(Areas));
            }
        }


        private bool isVisibleWho = false;
        public bool IsVisibleWho
        {
            get { return isVisibleWho; }
            set
            {
                isVisibleWho = value;
                RaisePropertyChanged(nameof(IsVisibleWho));
            }
        }

        private DateTime eventDate;
        public DateTime EventDate
        {
            get { return eventDate; }
            set
            {
                eventDate = value;
                RaisePropertyChanged(nameof(EventDate));
            }
        }

        private bool termsAndConditions = false;
        public bool TermsAndConditions
        {
            get { return termsAndConditions; }
            set
            {
                termsAndConditions = value;
                RaisePropertyChanged(nameof(TermsAndConditions));
            }
        }



        private string errorRefundMotive;
        public string ErrorRefundMotive
        {
            get { return errorRefundMotive; }
            set
            {
                errorRefundMotive = value;
                RaisePropertyChanged(nameof(ErrorRefundMotive));
            }
        }

        private string errorResponsableDecease;
        public string ErrorResponsableDecease
        {
            get { return errorResponsableDecease; }
            set
            {
                errorResponsableDecease = value;
                RaisePropertyChanged(nameof(ErrorResponsableDecease));
            }
        }

        private string errorComment;
        public string ErrorComment
        {
            get { return errorComment; }
            set
            {
                errorComment = value;
                RaisePropertyChanged(nameof(ErrorComment));
            }
        }

        private DateTime maximumDate;
        public DateTime MaximumDate
        {
            get { return maximumDate; }
            set
            {
                maximumDate = value;
                RaisePropertyChanged(nameof(MaximumDate));
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    RaisePropertyChanged(nameof(Comment));
                    if (!string.IsNullOrWhiteSpace(Comment))
                    {
                        CharactersAvailable = 1000 - Comment.Length;
                    }
                }
            }
        }

        private DateTime minimumDate;
        public DateTime MinimumDate
        {
            get { return minimumDate; }
            set
            {

                minimumDate = value;
                RaisePropertyChanged(nameof(MinimumDate));
            }
        }

        private bool isVisibleRefundMotive = false;
        public bool IsVisibleRefundMotive
        {
            get { return isVisibleRefundMotive; }
            set
            {
                isVisibleRefundMotive = value;
                RaisePropertyChanged(nameof(IsVisibleRefundMotive));
            }
        }

        private string nameOfficial;
        public string NameOfficial
        {
            get { return nameOfficial; }
            set
            {
                nameOfficial = value;
                RaisePropertyChanged(nameof(NameOfficial));
            }
        }





        private RefundType refundMotiveSelected;
        public RefundType RefundMotiveSelected
        {
            get { return refundMotiveSelected; }
            set
            {
                if (refundMotiveSelected != value)
                {
                    refundMotiveSelected = value;
                    RaisePropertyChanged(nameof(RefundMotiveSelected));

                    if (RefundMotiveSelected.Code == 2)
                    {
                        LoadResponsableDeceaseRequiredDocuments();
                    }
                    else
                    {
                        LoadRequiredDocuments();
                    }


                }
            }
        }



        private string errorArea;
        public string ErrorArea
        {
            get { return errorArea; }
            set
            {
                errorArea = value;
                RaisePropertyChanged(nameof(ErrorArea));
            }
        }

        private Area areaSelected;
        public Area AreaSelected
        {
            get { return areaSelected; }
            set
            {
                areaSelected = value;
                RaisePropertyChanged(nameof(AreaSelected));
            }
        }

        private ObservableCollection<string> whoPicker;
        public ObservableCollection<string> WhoPicker
        {
            get { return whoPicker; }
            set
            {
                whoPicker = value;
                RaisePropertyChanged(nameof(WhoPicker));
            }
        }

        private ObservableCollection<Document> documents;
        public ObservableCollection<Document> Documents
        {
            get { return documents; }
            set
            {
                documents = value;
                RaisePropertyChanged(nameof(Documents));
            }
        }

        private bool isVisibleThirdParty;
        public bool IsVisibleThirdParty
        {
            get { return isVisibleThirdParty; }
            set
            {
                isVisibleThirdParty = value;
                RaisePropertyChanged(nameof(IsVisibleThirdParty));
            }
        }

        private string document;
        public string Document
        {
            get { return document; }
            set
            {
                document = value;
                RaisePropertyChanged(nameof(Document));
            }
        }

        private string errorDepartment;
        public string ErrorDepartment
        {
            get { return errorDepartment; }
            set
            {
                errorDepartment = value;
                RaisePropertyChanged(nameof(ErrorDepartment));
            }
        }

        private Document documentSelected;
        public Document DocumentSelected
        {
            get { return documentSelected; }
            set
            {
                documentSelected = value;
                RaisePropertyChanged(nameof(DocumentSelected));
            }
        }

        private string errorDocument;
        public string ErrorDocument
        {
            get { return errorDocument; }
            set
            {
                errorDocument = value;
                RaisePropertyChanged(nameof(ErrorDocument));
            }
        }

        private string errorDocumentType;
        public string ErrorDocumentType
        {
            get { return errorDocumentType; }
            set
            {
                errorDocumentType = value;
                RaisePropertyChanged(nameof(ErrorDocumentType));
            }
        }

        private string whoSelected;
        public string WhoSelected
        {
            get { return whoSelected; }
            set
            {
                if (whoSelected != value)
                {
                    whoSelected = value;
                    RaisePropertyChanged(nameof(WhoSelected));
                    if (!string.IsNullOrWhiteSpace(WhoSelected))
                    {
                        LoadForm();
                    }
                }
            }
        }

        private EventType eventTypeSelected;
        public EventType EventTypeSelected
        {
            get { return eventTypeSelected; }
            set
            {
                if (EventTypeSelected != value)
                {
                    eventTypeSelected = value;
                    RaisePropertyChanged(nameof(EventTypeSelected));
                    if (EventTypeSelected != null)
                    {
                        IsVisibleWho = true;
                    }
                }
            }
        }

        public ICommand SearchCommand { get { return new RelayCommand(Search); } }
        public ICommand ContinueCommand { get { return new RelayCommand(Continue); } }
        public ICommand TermsAndConditionsCommand { get { return new RelayCommand<string>(TermsAndConditionsAccepted); } }
        public ICommand InformationFileCommand { get { return new RelayCommand<string>(EnableFileInformation); } }
        public ICommand InformationDateCommand { get { return new RelayCommand(EnableInformationDate); } }

        private void EnableInformationDate()
        {
            IsVisibleInformationDate = !IsVisibleInformationDate;
        }
        private void EnableFileInformation(string op)
        {
            switch (op)
            {
                case "1":
                    InformationFile = !InformationFile;
                    break;
                case "2":
                    IsVisibleHelpText = !IsVisibleHelpText;
                    break;
                default:
                    break;
            }
        }


        private void TermsAndConditionsAccepted(string option)
        {
            if (option.Equals("1"))
            {
                SavePQRS();
            }

            navigationService.BackModal();
        }

        private async void Continue()
        {
            try
            {
                dialogService.ShowProgress();
                if (string.IsNullOrEmpty(TermsAndConditionsText))
                {
                    var response = await apiService.GetPQRSTermsConditions();
                    if (response.Success)
                    {
                        termsAndConditionsText = response.TermConditions.Message;
                    }
                    else
                    {
                        await dialogService.ShowMessage(response.Title, response.Message);
                    }

                }
                CleanError();
                if (ValidateFields())
                {
                    await navigationService.ShowModal(AppPages.TermsAndConditionsPQRSPage);
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                dialogService.HideProgress();
            }

        }

        private async void LoadRefundMotive(bool responsable)
        {
            try
            {
                RefundTypeRequest request = new RefundTypeRequest
                {
                    Responsable = responsable
                };
                RefundTypeResponse response = await apiService.GetRefundType(request);
                if (response.Success)
                {
                    IsVisibleRefundMotive = true;
                    RefundMotive = new ObservableCollection<RefundType>(response.RefundTypes);
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message) ;
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }

        }

        private void LoadDocuments()
        {
            switch (EventTypeSelected.EventTypesId)
            {
                case "4":
                    DocumentsRequired = new ObservableCollection<DocumentRequiredViewModel>
                    {
                        new DocumentRequiredViewModel{
                            Name="Adjuntar documento",
                            IsVisibleFileList=true,
                            MaxFiles=1,
                            InformationFile="Puedes agregar un documento de máximo 2MB. Los formatos permitidos son PDF, jpeg y png."
                        }
                    };
                    break;
                default:
                    DocumentsRequired = new ObservableCollection<DocumentRequiredViewModel>();
                    break;
            }
        }

        private void LoadResponsableDeceaseRequiredDocuments()
        {
            try
            {
                HelpText = "Podrás solicitar el reintegro de la/s factura/s cobrada/s después del fallecimiento del responsable de pago/beneficiario, únicamente si eres su cónyuge. De no cumplir correctamente con alguno de estos requisitos, tu solicitud no será gestionada. Los formatos permitidos son PDF, jpeg y png, de máximo 2MB cada documento. Para dicho trámite debes agregar los siguientes documentos:";
                IsVisibleHelpText = true;
                DocumentsRequired = new ObservableCollection<DocumentRequiredViewModel>
                {

                    new DocumentRequiredViewModel
                    {
                        Name = "(*) Certificado de defunción del responsable de pago/beneficiario",
                        IsVisibleAddFile = true,
                        IsRequired = true,
                        Code=1
                    }
                };
                DocumentsRequired.Add(new DocumentRequiredViewModel
                {
                    Name = "(*) Registro civil de matrimonio o certificado notarial",
                    IsVisibleAddFile = true,
                    InformationFile = "Fecha de expedición no mayor a 3 meses",
                    IsRequired = true,
                    Code = 2
                });

                if (DocumentsRequired.Count > 0)
                    DocumentsRequired.Add(new DocumentRequiredViewModel
                    {
                        Name = "(*) Estado de cuenta o certificación bancaria",
                        IsVisibleAddFile = true,
                        InformationFile = "Emitida por tu banco y en la cual conste tipo y número de cuenta existente. La cuenta debe estar a nombre de la persona que solicita el reembolso.",
                        IsRequired = true,
                        Code = 6
                    });
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private void LoadRequiredDocuments()
        {
            try
            {
                HelpText = string.Empty;
                IsVisibleHelpText = false;
                switch (RefundMotiveSelected.Code)
                {
                    case 1:
                        HelpText = "Como responsable de pago podrás solicitar el reintegro de la/s factura/s cobrada/s después del fallecimiento del beneficiario. Para dicho trámite cumplir con los siguientes requisitos, de lo contrario tu solicitud no será gestionada:";
                        IsVisibleHelpText = true;
                        DocumentsRequired = new ObservableCollection<DocumentRequiredViewModel>
                    {
                        new DocumentRequiredViewModel
                        {
                            Name="(*) Certificado de defunción del beneficiario",
                            IsVisibleAddFile=true,
                            IsRequired=true,
                            Code=7

                        }
                    };
                        break;
                    case 2:
                        DocumentsRequired = new ObservableCollection<DocumentRequiredViewModel>();
                        HelpText = "Podrás solicitar el reintegro de la/s factura/s cobrada/s después del fallecimiento del responsable de pago/beneficiario, únicamente si eres su cónyuge. De no cumplir correctamente con alguno de estos requisitos, tu solicitud no será gestionada. Los formatos permitidos son PDF, jpeg y png, de máximo 2MB cada documento. Para dicho trámite debes agregar los siguientes documentos:";
                        break;
                    case 3:
                        DocumentsRequired = new ObservableCollection<DocumentRequiredViewModel>
                    {
                        new DocumentRequiredViewModel
                        {
                            Name="(*) Extracto bancario o documentación justificante de solicitud de reembolso",
                            IsVisibleAddFile=true,
                            InformationFile="Debes cumplir con los siguientes requisitos, de lo contrario tu solicitud no será gestionada. Los formatos permitidos para los documentos que debes cargar son PDF, jpeg y png, de máximo 2MB cada documento",
                            IsRequired=true,
                            Code=8
                        }
                    };
                        break;
                    default:
                        DocumentsRequired = new ObservableCollection<DocumentRequiredViewModel>();
                        break;
                }
                if (DocumentsRequired != null && !HasBankAccount)
                {
                    DocumentsRequired.Add(
                        new DocumentRequiredViewModel
                        {
                            Name = "(*) Cuenta bancaria",
                            IsVisibleAddFile = true,
                            IsRequired = true,
                            InformationFile = "Debido a que el pago de nuestros servicios lo realizas por un medio diferente a débito de una cuenta bancaria, debes adjuntar Certificación bancaria emitida por tu banco y en la cual conste tipo y número de cuenta existente. La cuenta debe estar a tu nombre como persona quien solicita el reembolso.",
                            Code = 9
                        });
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        public List<FileSelected> GetFile(int code)
        {
            var file = DocumentsRequired.Where(x => x.Code == code).FirstOrDefault();

            List<FileSelected> fileReady = new List<FileSelected>();

            if (file != null)
            {

                fileReady.Add(  new FileSelected
                {
                    File = file.Base64,
                    Name = file.NameDocument,
                    Extension = file.Extension
                });
            }

            return fileReady;
        }

        public async void SavePQRS()
        {
            try
            {
                dialogService.ShowProgress();
                string documentType = WhoSelected.Equals("A un tercero") ? DocumentSelected.Code : loginViewModel.User.DocumentType;
                string applicantDocument = WhoSelected.Equals("A un tercero") ? Document : loginViewModel.User.Document;
                int subject = whoSelected.Equals("A un tercero") ? 02 : 01;
                List<FileSelected> thanks = new List<FileSelected>();

                if (DocumentsRequired.First() != null && EventTypeSelected.EventTypesId.Equals("4"))
                {
                    if (!string.IsNullOrWhiteSpace(DocumentsRequired.First().FilesSelected.First().NameDocument))
                        thanks.Add( new FileSelected
                        {
                            File = DocumentsRequired.First().FilesSelected.First().Base64,
                            Extension = DocumentsRequired.First().FilesSelected.First().Extension,
                            Name = DocumentsRequired.First().FilesSelected.First().NameDocument
                        });
                }


                List<FileSelected> complains = new List<FileSelected>();
                if (EventTypeSelected.EventTypesId.Equals("1"))
                    foreach (var doc in DocumentsRequired.First().FilesSelected)
                    {

                        complains.Add(new FileSelected
                        {
                            Extension = doc.Extension,
                            Name = doc.NameDocument,
                            File = doc.Base64
                        });
                    }

                List<FileSelected> titularDeath = GetFile(1);
                List<FileSelected> civilRegistrationMarriage = GetFile(2);
                List<FileSelected> birthCertificate = GetFile(3);
                List<FileSelected> spouseDeath = GetFile(4);
                List<FileSelected> extraJudgement = GetFile(5);
                List<FileSelected> bankAccountCertificate = GetFile(6);
                List<FileSelected> beneficiaryDeath = GetFile(7);
                List<FileSelected> payroll = GetFile(8);
                List<FileSelected> bankAccount = GetFile(9);

                var request = new RequestCreatePQRS
                {

                    EventType = EventTypeSelected.EventTypesId,
                    SubjectOfTheEvent = subject.ToString(),
                    ApplicantDocumentType = loginViewModel.User.DocumentType,
                    ApplicantDocument = loginViewModel.User.Document,
                    ThirdDocument = applicantDocument,
                    ThirdDocumentType = documentType,
                    BankAccountUser = HasBankAccount,
                    RelatedArea = areaSelected.Code.ToString(),
                    NamesOfficial = NameOfficial ?? String.Empty,
                    EventDate = EventDate.ToString("dd/MM/yyyy"),
                    EventComment = Comment,
                    AcceptTermsAndConditions = true,
                    ReasonsForReimbursement = RefundMotiveSelected?.Code.ToString() ?? string.Empty,
                    SendFileThanksAndCongratulations = thanks ?? new List<FileSelected>(),
                    SendFilesComplaintsAndClaims = complains ?? new List<FileSelected>(),
                    SendBeneficiaryDeathCertificate = beneficiaryDeath ?? new List<FileSelected>(),
                    SendFileBankAccountCertificate = bankAccountCertificate ?? new List<FileSelected>(),
                    SendFileTitularDeathCertificate = titularDeath ?? new List<FileSelected>(),
                    SendFilesCivilRegistrationMarriage = civilRegistrationMarriage ?? new List<FileSelected>(),
                    SendFileBirthCertificateSon = birthCertificate ?? new List<FileSelected>(),
                    SendFileSpouseDeathCertificate = spouseDeath ?? new List<FileSelected>(),
                    SendFileExtraJudgmentStatement = extraJudgement ?? new List<FileSelected>(),
                    SendFileBankAccount = bankAccount ?? new List<FileSelected>(),
                    SendFileBankStatementOrRemovablePayroll = payroll ?? new List<FileSelected>(),
                    EventCodeDepartment = PQRSUser.ResponseUser.CodeState,
                    EventCodeCity = PQRSUser.ResponseUser.CodeCity,
                    Action = "PostCreatePqrs",
                    Controller = "pqrs"
                };



                //var request = new RequestCreatePQRS();
                dialogService.ShowProgress();
                var response = await apiService.PostPQRS(request);
                
                dialogService.HideProgress();
                if (response.Success)
                {
                    await dialogService.ShowMessage("", response.Message + " " + response.SettledNumber);
                    await navigationService.BackToRoot();
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
                

            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                dialogService.HideProgress();
            }
        }

        public void CleanFirstForm()
        {
            EventTypeSelected = null;
            WhoSelected = null;
            IsVisibleThirdParty = false;
            IsVisibleWho = false;
            DocumentSelected = null;
            Document = null;
        }

        private bool ValidateFields()
        {
            try
            {
                bool result = true;
                if (AreaSelected == null)
                {
                    ErrorArea = "Es requerido seleccionar el area médica";
                    result = false;
                }
                if (!string.IsNullOrWhiteSpace(NameOfficial))
                    if (!ValidatorHelper.IsValidName(NameOfficial))
                    {
                        ErrorOfficial = "Ingrese un nombre válido";
                        result = false;
                    }
                if (Comment.Length < 40)
                {
                    ErrorComment = "Mínimo 40 caracteres";
                    result = false;
                }
                if (RefundMotiveSelected == null && IsVisibleRefundMotive)
                {
                    ErrorRefundMotive = "El motivo del reembolso es requerido.";
                    result = false;
                }
                if (documentsRequired != null)
                    foreach (var doc in DocumentsRequired)
                        doc.IsVisibleError = doc.IsRequired && string.IsNullOrWhiteSpace(doc.NameDocument);

                var asd = DocumentsRequired.Any(x => x.IsVisibleError == true);
                if (asd)
                    result = false;

                if (!result)
                    dialogService.ShowMessage("", "Por favor diligencia correctamente todos los campos");

                return result;
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
                return false;
            }

        }

        public async void LoadNewPQRS()
        {
            try
            {
                dialogService.ShowProgress();
                CleanError();
                LoadDocuments();
                ResponseAreas responseAreas = await apiService.GetAreas();
                if (responseAreas.Success)
                {
                    Areas = new ObservableCollection<Area>(responseAreas.Areas);
                    MinimumDate = DateTime.Now.AddMonths(-3);
                    maximumDate = DateTime.Now;
                    await navigationService.Navigate(AppPages.NewPQRSPage);
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                dialogService.HideProgress();
            }
        }

        public async void Search()
        {
            try
            {
                ValidateDocument();
                if (string.IsNullOrEmpty(ErrorDocument) && string.IsNullOrEmpty(ErrorDocumentType))
                {
                    RequestPQRSUser request = new RequestPQRSUser
                    {
                        Document = Document,
                        DocumentType = DocumentSelected.Code,
                        Controller = "pqrs",
                        Action = "ValidateActive"
                    };
                    if (await ValidateActiveUser(request))
                        LoadNewPQRS();
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }

        }

        public void ValidateDocument()
        {
            ErrorDocument = string.IsNullOrEmpty(Document) ? AppResources.DocumentRequired : string.Empty;
            ErrorDocumentType = DocumentSelected == null ? AppResources.DocumentTypeRequired : string.Empty;
        }

        public void Clean()
        {
            try
            {
                IsVisibleHelpText = false;
                HelpText = string.Empty;
                TitlePQRS = EventTypeSelected.Name;
                IsVisibleInformationDate = false;
                HasBankAccount = false;
                IsVisibleThirdParty = false;
                NameOfficial = string.Empty;
                Comment = string.Empty;
                EventDate = DateTime.Now;
                AreaSelected = null;
                RefundMotiveSelected = null;
                DocumentsRequired = new ObservableCollection<DocumentRequiredViewModel>();
                CharactersAvailable = 1000;
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }

        }

        public async void LoadTracingPQRS()
        {
            try
            {
                var request = new Request
                {
                    Document = loginViewModel.User.Document,
                    DocumentType = loginViewModel.User.Document,
                    Controller = "pqrs",
                    Action = "TracingPqrs"
                };
                var response = await apiService.GetTracingPQRS(request);
                PQRSInCourse = new ObservableCollection<TracingPQR>(response.TracingPqrs);
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        public void CleanError()
        {
            ErrorArea = string.Empty;
            ErrorDepartment = string.Empty;
            ErrorOfficial = string.Empty;
            ErrorComment = string.Empty;
            ErrorResponsableDecease = string.Empty;
            ErrorRefundMotive = string.Empty;
        }

        public async void LoadData()
        {
            try
            {
                dialogService.ShowProgress();
                WhoSelected = string.Empty;
                EventTypeSelected = null;
                IsVisibleWho = false;
                IsVisibleThirdParty = false;
                EventType = null;
                LoadTracingPQRS();
                var response = await apiService.GetEventType();
                if (response.Success && response.EventTypes.Count > 0)
                    EventType = new ObservableCollection<EventType>(response.EventTypes);


                if (WhoPicker == null)
                    WhoPicker = new ObservableCollection<string>
                {
                    "Directamente a mí",
                    "A un tercero"
                };
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                dialogService.HideProgress();
            }
        }

        public async Task<bool> ValidateActiveUser(RequestPQRSUser request)
        {
            try
            {
                dialogService.ShowProgress();
                PQRSUser = await apiService.GetPQRSUser(request);
                dialogService.HideProgress();
                if (!PQRSUser.ResponseUser.ActiveUser)
                    await dialogService.ShowMessage("", PQRSUser.Message);

                if (EventTypeSelected.EventTypesId.Equals("2"))
                {
                    HasBankAccount = PQRSUser.ResponseUser.BankAccountUser;
                    LoadRefundMotive(PQRSUser.ResponseUser.ActiveUserResponsible);

                }
                else
                {
                    IsVisibleRefundMotive = false;
                }


                return PQRSUser.ResponseUser.ActiveUser;
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
                return false;
            }
            finally
            {
                dialogService.HideProgress();
            }


        }

        public async void LoadForm()
        {
            try
            {
                Clean();
                dialogService.ShowProgress();
                if (whoSelected.Trim().ToLower().Equals("A un tercero".Trim().ToLower()))
                {
                    RequestDocument request = new RequestDocument();
                    var response = await apiService.GetDocuments(request);
                    if (response.Success && response.Documents != null)
                    {
                        Documents = new ObservableCollection<Document>(response.Documents);
                        IsVisibleThirdParty = true;
                    }

                }
                else
                {
                    RequestPQRSUser request = new RequestPQRSUser
                    {
                        Document = loginViewModel.User.Document,
                        DocumentType = loginViewModel.User.DocumentType,
                        Controller = "pqrs",
                        Action = "ValidateActive"

                    };
                    IsVisibleThirdParty = false;
                    if (await ValidateActiveUser(request))
                        LoadNewPQRS();
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                dialogService.HideProgress();
            }

        }



        public PQRSPageViewModel(INavigationService navigationService, IApiService apiService, IDialogService dialogService, IValidatorService validatorService, ILoginViewModel loginViewModel)
        {

            this.dialogService = dialogService;
            this.apiService = apiService;
            this.loginViewModel = loginViewModel;
            documentsRequired = new ObservableCollection<DocumentRequiredViewModel>();
            this.navigationService = navigationService;

        }
    }
}
