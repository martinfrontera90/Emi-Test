namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System.Collections.ObjectModel;
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
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class FamilyPageViewModel : ViewModelBase, IFamilyPageViewModel
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

        private ObservableCollection<PersonViewModel> members;
        public ObservableCollection<PersonViewModel> Members
        {
            get { return members; }
            set
            {
                if (members != value)
                {
                    members = value;
                    RaisePropertyChanged("Members");
                }
            }
        }

        private PersonViewModel memberSelected;
        public PersonViewModel MemberSelected
        {
            get { return memberSelected; }
            set
            {
                if (memberSelected != value)
                {
                    memberSelected = value;
                    RaisePropertyChanged("MemberSelected");
                }
            }
        }

        public ICommand AddMemberCommand { get { return new RelayCommand(AddMember); } }
        public ICommand InformationCommand { get { return new RelayCommand<string>(Information); } }
        public ICommand RefreshFamilyCommand { get { return new RelayCommand(RefreshFamily); } }

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

        private async void RefreshFamily()
        {
            await LoadFamily();
        }

        private async void AddMember()
        {
            ISearchFamilyPageViewModel searchFamilyPageViewModel = ServiceLocator.Current.GetInstance<ISearchFamilyPageViewModel>();
            searchFamilyPageViewModel.Document = string.Empty;
            await navigationService.Navigate(AppPages.SearchFamilyPage);
        }

        public FamilyPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService, IPhoneService phoneService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.phoneService = phoneService;

            Members = new ObservableCollection<PersonViewModel>();
        }

        public async Task LoadFamily()
        {
            dialogService.ShowProgress();
            IsRefreshing = false;
            RequestFamilyMembers requestFamilyMembers = new RequestFamilyMembers();
            ResponseFamilyMembers responseFamilyMembers = await apiService.GetFamilyMembers(requestFamilyMembers);
            dialogService.HideProgress();
            ValitateResponse(responseFamilyMembers);
        }

        private async void ValitateResponse(ResponseFamilyMembers responseFamilyMembers)
        {
            if (responseFamilyMembers.Success && responseFamilyMembers.StatusCode == 0)
            {
                if (responseFamilyMembers.Members != null)
                {
                    responseFamilyMembers.Members.Remove(responseFamilyMembers.Members[0]);
                }
                Members = new ObservableCollection<PersonViewModel>();
                foreach (Person member in responseFamilyMembers.Members)
                {
                    PersonViewModel personViewModel = new PersonViewModel();
                    ViewModelHelper.SetPersonToPersonViewModel(personViewModel, member);
                    Members.Add(personViewModel);
                }
            }
            else
            {
                await dialogService.ShowMessage(responseFamilyMembers.Title, responseFamilyMembers.Message);
                await navigationService.Back();
            }
        }
    }
}
