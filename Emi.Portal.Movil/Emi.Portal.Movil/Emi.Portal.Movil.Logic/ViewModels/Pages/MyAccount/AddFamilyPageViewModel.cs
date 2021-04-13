namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class AddFamilyPageViewModel : ViewModelBase, IAddFamilyPageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;

        public Person Member { get; set; }

        private PersonViewModel newMember;
        public PersonViewModel NewMember
        {
            get { return newMember; }
            set
            {
                newMember = value;
                RaisePropertyChanged("NewMember");
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        public ICommand AddMemberCommand { get { return new RelayCommand(AddMember); } }        

        private async void AddMember()
        {
            if (IsValidMember())
            {
                dialogService.ShowProgress();
                RequestAddMember request = new RequestAddMember
                {
                    Document = NewMember.Document,
                    Email = NewMember.Email,
                    DocumentType = NewMember.DocumentType,
                    IdReference = NewMember.IdReference,
                    Names = NewMember.Names,
                    Phone = NewMember.Phone,
                    Surnames = NewMember.Surnames

                };
                ResponseAddMember response = await apiService.AddMember(request);
                dialogService.HideProgress();
                ValidateResponseAddMember(response);
            }
            else
                await dialogService.ShowMessage("Agregar familiar", AppResources.AllFieldsRequired);
        }

        private async void ValidateResponseAddMember(ResponseAddMember response)
        {
            if (response.Success && response.StatusCode == 0)
            {
                await dialogService.ShowMessage(response.Title, response.Message);
                IFamilyPageViewModel familyPage = ServiceLocator.Current.GetInstance<IFamilyPageViewModel>();
                await familyPage.LoadFamily();
                await navigationService.BackToRoot();
                return;
            }

            await dialogService.ShowMessage(response.Title, response.Message);
        }

        private bool IsValidMember()
        {
            bool isValid = true;

            NewMember.ErrorNames = string.IsNullOrEmpty(NewMember.Names) ? AppResources.NameRequired : string.Empty;
            NewMember.ErrorSurnames = string.IsNullOrEmpty(NewMember.Surnames) ? AppResources.SunamesRequired : string.Empty;

            if (NewMember.IsVisiblePersonalData)
            {
                if (string.IsNullOrEmpty(NewMember.Email))
                {
                    NewMember.ErrorEmail = AppResources.MailRequired;
                }
                else
                {
                    NewMember.ErrorEmail = ValidatorHelper.IsValidEmail(NewMember.Email) ? string.Empty : AppResources.WriteValidEmail;
                }

                if (string.IsNullOrEmpty(NewMember.Phone))
                {
                    NewMember.ErrorPhone = AppResources.CellPhoneRequired;
                }
                else
                {
                    NewMember.ErrorPhone = ValidatorHelper.IsValidCellPhone(NewMember.Phone) ? string.Empty : AppResources.InvalidPhone;
                }
                isValid = string.IsNullOrEmpty(NewMember.ErrorEmail) && string.IsNullOrEmpty(NewMember.ErrorPhone);
            }

            return isValid && string.IsNullOrEmpty(NewMember.ErrorNames) && string.IsNullOrEmpty(NewMember.ErrorSurnames);
        }

        public void ShowResult()
        {
            NewMember = new PersonViewModel();
            ViewModelHelper.SetPersonToPersonViewModel(NewMember, Member);

            if (string.IsNullOrEmpty(NewMember.IdReference))
            {
                NewMember.IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference;
            }

            NewMember.IsVisiblePersonalData = AppConfigurations.Brand == "ucm";
        }


        public AddFamilyPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            NewMember = new PersonViewModel();
        }
    }
}
