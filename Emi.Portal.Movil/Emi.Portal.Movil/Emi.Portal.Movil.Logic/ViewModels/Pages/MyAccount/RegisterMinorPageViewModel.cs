namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System;
    using GalaSoft.MvvmLight;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using GalaSoft.MvvmLight.Command;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using System.Collections.ObjectModel;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;

    public class RegisterMinorPageViewModel : ViewModelBase, IRegisterMinorPageViewModel
    {
        ILoginViewModel loginViewModel;
        IApiService apiService;
        IFileSelectService fileService;
        IDialogService dialogService;
        INavigationService navigationService;

        private string titlePage;
        public string TitlePage
        {
            get { return titlePage; }
            set
            {
                titlePage = value;
                RaisePropertyChanged(nameof(TitlePage));
            }
        }

        private string errorMinorSelected;
        public string ErrorMinorSelected
        {
            get { return errorMinorSelected; }
            set
            {
                errorMinorSelected = value;
                RaisePropertyChanged(nameof(ErrorMinorSelected));
            }
        }

        private string errorIdentityFile;
        public string ErrorIdentityFile
        {
            get { return errorIdentityFile; }
            set
            {
                errorIdentityFile = value;
                RaisePropertyChanged(nameof(ErrorIdentityFile));
            }
        }

        private string errorRegisterFile;
        public string ErrorRegisterFile
        {
            get { return errorRegisterFile; }
            set
            {
                errorRegisterFile = value;
                RaisePropertyChanged(nameof(ErrorRegisterFile));
            }
        }

        private FileSelected identityFile;
        public FileSelected IdentityFile
        {
            get { return identityFile; }
            set
            {
                identityFile = value;
                RaisePropertyChanged(nameof(IdentityFile));
            }
        }

        private FileSelected registerFile;
        public FileSelected RegisterFile
        {
            get { return registerFile; }
            set
            {
                registerFile = value;
                RaisePropertyChanged(nameof(RegisterFile));
            }
        }

        private ObservableCollection<YoungMember> members;
        public ObservableCollection<YoungMember> Members
        {
            get { return members; }
            set
            {
                members = value;
                RaisePropertyChanged(nameof(Members));
            }
        }
        private YoungMember memberSelected;
        public YoungMember MemberSelected
        {
            get { return memberSelected; }
            set
            {
                memberSelected = value;
                RaisePropertyChanged(nameof(MemberSelected));
            }
        }


        public ICommand AddFamilyCommand { get { return new RelayCommand(() => navigationService.Navigate(Enumerations.AppPages.AddFamilyPage)); } }
        public ICommand AddIdentityFileCommand { get { return new RelayCommand(async () => IdentityFile = await fileService.SelectFile()); } }
        public ICommand AddRegisterFileCommand { get { return new RelayCommand(async () => RegisterFile = await fileService.SelectFile()); } }
        public ICommand DeleteIdentityFileCommand { get { return new RelayCommand(() => IdentityFile = new FileSelected()); } }
        public ICommand DeleteRegisterFileCommand { get { return new RelayCommand(() => RegisterFile = new FileSelected()); } }
        public ICommand InformationCommand { get { return new RelayCommand(async () => await dialogService.ShowMessage("Registrar menor", "En caso de que en el anterior listado no encuentres a la persona que deseas registrar, puedes agregarla ingresando a la opción Mi cuenta / Mis familiares.")); } }

        public ICommand SendCommand { get { return new RelayCommand(Send); } }

        private async void Send()
        {
            try
            {
                if (ValidateData())
                {
                    dialogService.ShowProgress();
                    var request = new RequestRegisterMinor
                    {
                        Action = "GetRegistrationResponsible",
                        Controller = "Family",
                        Document = loginViewModel.User.Document,
                        DocumentType = loginViewModel.User.DocumentType,
                        StringMinorDocument = IdentityFile.File,
                        StringCivilRegistration = RegisterFile.File,
                        MinorDocumentType = MemberSelected.DocumentType,
                        MinorDocument = MemberSelected.Document,
                        MinorFullName = MemberSelected.FullNames,
                        ExtentionCivilRegistration = RegisterFile.Extension.ToUpper().Replace(".", ""),
                        ExtentionDocument = IdentityFile.Extension.ToUpper().Replace(".", ""),
                        MailResponsible = loginViewModel.User.UserName
                    };
                    var response = await apiService.RegisterMinor(request);
                    dialogService.HideProgress();
                    if (response.Success && response.StatusCode != -1)
                    {
                        await dialogService.AlertIcon("Tu solicitud fue recibida", $"Tu solicitud ha sido registrada exitosamente bajo el radicado Nº: {response.NumFnc}. En un plazo máximo de dos (2) días hábiles daremos respuesta a tu requerimiento a través del correo {loginViewModel.User.UserName}");
                        await navigationService.Back();
                    }
                    else
                    {
                        await dialogService.ShowMessage(response.Title, response.Message);
                    }
                }
                else
                {
                    await dialogService.ShowMessage("", "Todos los campos marcados con (*) son obligatorios");
                }
            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private bool ValidateData()
        {
            bool result = true;
            if (string.IsNullOrWhiteSpace(IdentityFile.Name))
            {
                result = false;
                ErrorIdentityFile = "El documento es requerido";
            }
            else
            {
                ErrorIdentityFile = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(RegisterFile.Name))
            {
                result = false;
                ErrorRegisterFile = "El documento es requerido";
            }
            else
            {
                ErrorRegisterFile = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(MemberSelected?.FullNames))
            {
                result = false;
                ErrorMinorSelected = "Selecciona al menor de edad";
            }
            else
            {
                ErrorMinorSelected = string.Empty;
            }
            return result;
        }

        private void CleanData()
        {
            Members = new ObservableCollection<YoungMember>();
            MemberSelected = new YoungMember();
            IdentityFile = new FileSelected();
            RegisterFile = new FileSelected();
            ErrorMinorSelected = string.Empty;
            ErrorIdentityFile = string.Empty;
            ErrorRegisterFile = string.Empty;
        }

        public async void LoadData()
        {
            try
            {
                dialogService.ShowProgress();
                CleanData();
                Members = new ObservableCollection<YoungMember>();
                var request = new Request
                {
                    Controller = "Family",
                    Action = "GetYoungMembers",
                    IdReference = loginViewModel.User.IdReference
                };
                var response = await apiService.GetYoungMembers(request);
                dialogService.HideProgress();
                if (response.Success && response.StatusCode != -1)
                {
                    await navigationService.Navigate(Enumerations.AppPages.RegisterMinorPage);
                    Members = new ObservableCollection<YoungMember>(response.Members);
                }
                else
                {
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        public RegisterMinorPageViewModel(ILoginViewModel loginViewModel, IApiService apiService, IDialogService dialogService, INavigationService navigationService, IFileSelectService fileService)
        {
            this.fileService = fileService;
            this.navigationService = navigationService;
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.loginViewModel = loginViewModel;
        }
    }
}
