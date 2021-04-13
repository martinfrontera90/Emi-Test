namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount.Cards
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class CardsPageViewModel : ViewModelBase, ICardsPageViewModel
    {
        IApiService apiService;
        INavigationService navigationService;
        ILoginViewModel loginViewModel;
        IDialogService dialogService;

        private ObservableCollection<MembershipCard> cards;
        public ObservableCollection<MembershipCard> Cards
        {
            get { return cards; }
            set
            {
                cards = value;
                RaisePropertyChanged(nameof(Cards));
            }
        }

        private MembershipCard cardSelected;
        public MembershipCard CardSelected
        {
            get { return cardSelected; }
            set
            {
                cardSelected = value;
                RaisePropertyChanged(nameof(CardSelected));
            }
        }

        public ICommand OpenCardCommand { get { return new RelayCommand<MembershipCard>(OpenCard); } }
        public ICommand CloseCommand { get { return new RelayCommand(Close); } }

        private void Close()
        {
            navigationService.Back();
        }

        public void OpenCard(MembershipCard card)
        {
            CardSelected = card;
            navigationService.Navigate(Enumerations.AppPages.CardDetailPage);
        }

        public async Task LoadData()
        {
            try
            {
                dialogService.ShowProgress();
                var request = new Request
                {
                    Document = loginViewModel.User.Document,
                    DocumentType = loginViewModel.User.DocumentType,
                    Action = "GetAffiliateCard",
                    Controller = "Affiliate"
                };
                var response = await apiService.GetAffiliateCard(request);
                if (response.Success)
                {
                    Cards = new ObservableCollection<MembershipCard>(response.MembershipCards);
                    await navigationService.Navigate(Enumerations.AppPages.CardsPage);
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

        public CardsPageViewModel(IApiService apiService, INavigationService navigationService, ILoginViewModel loginViewModel, IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.loginViewModel = loginViewModel;
            this.dialogService = dialogService;
        }
    }
}
