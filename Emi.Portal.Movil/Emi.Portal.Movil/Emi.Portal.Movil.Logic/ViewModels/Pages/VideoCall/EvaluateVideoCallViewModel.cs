namespace Emi.Portal.Movil.Logic.ViewModels.Pages.VideoCall
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class EvaluateVideoCallViewModel : ViewModelBase, IEvaluateVideoCallViewModel
    {
        string _commentEvaluateVideoCall;

        int _ratingQ2;

        readonly INavigationService _navigationService;

        readonly IDialogService _dialogService;

        readonly IApiService _apiService;

        readonly IServicesPageViewModel _servicesPage = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();

        public ICommand CallCategoryCommand => new RelayCommand(CallCategory);

        public ICommand CancelEvaluateCommand => new RelayCommand(CancelEvaluate);

        public ICommand SendEvaluateCommand => new Command(SendEvaluate);

        public string CommentEvaluateVideoCall
        {
            get => _commentEvaluateVideoCall;
            set
            {
                _commentEvaluateVideoCall = value;
                RaisePropertyChanged();
            }
        }

        public int RatingQ2
        {
            get => _ratingQ2;
            set
            {
                _ratingQ2 = value;
                RaisePropertyChanged();
            }
        }

        public EvaluateVideoCallViewModel(IDialogService dialogService,
            INavigationService navigationService,
            IApiService apiService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;

            _navigationService.RemovePage(1);
        }

        void CallCategory()
        {
            ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
            callViewModel.CallCategory();
        }

        void CancelEvaluate()
        {
            GoToServicePage();
        }

        string errorStar;
        public string ErrorStar
        {
            get { return errorStar; }
            set
            {
                if (errorStar != value)
                {
                    errorStar = value;
                    RaisePropertyChanged();
                }
            }
        }

        void SendEvaluate()
        {
            ErrorStar = string.Empty;
            if (RatingQ2 == 0)
            {
                ErrorStar = AppResources.SelectStar;
                return;
            }

            var data = ServiceLocator.Current.GetInstance<IMedicalVideoCallViewModel>().MessagesOpentok;

            var request = new RequestSendRatingCall
            {
                CallHistoryId = data.CallHistoryId,
                Comments = CommentEvaluateVideoCall ?? string.Empty,
                RatingId = RatingQ2
            };

            _apiService.SendRatingCall(request);
            _commentEvaluateVideoCall = string.Empty;
            GoToServicePage();
        }

        public async Task GoToServicePage()
        {
            await _navigationService.BackToRoot();
            await _navigationService.Navigate(Enumerations.AppPages.ServicesPage);
        }

        public void RemovePage() => _navigationService.RemovePage(1);

    }
}
