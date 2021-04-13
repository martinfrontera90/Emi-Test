namespace Emi.Portal.Movil.Logic.ViewModels.Pages.VideoCall
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.VideoCall;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Plugin.SimpleAudioPlayer;

    public class PrecallViewModel : ViewModelBase, IPrecallViewModel
    {
        readonly IDialogService _dialogService;

        readonly INavigationService _navigationService;

        readonly ISimpleAudioPlayer _player;

        readonly IApiService _apiService;

        RequestMedicalService _requestMedicalService;

        string _textValidation;

        double _valueProgress;

        string messageOpentok;

        public IServicesPageViewModel ServicesPage = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();

        public ICommand CallCategoryCommand
        {
            get
            {
                return new RelayCommand(CallCategory);
            }
        }

        IServicesPageViewModel IPrecallViewModel.ServicesPage => throw new NotImplementedException();

        public string TextValidation
        {
            get => _textValidation;
            set
            {
                _textValidation = value;
                RaisePropertyChanged();
            }
        }

        public double ValueProgress
        {
            get => _valueProgress;
            set
            {
                _valueProgress = value;
                RaisePropertyChanged();
            }
        }

        public string MessageOpentok
        {
            get { return messageOpentok; }
            set
            {
                if (messageOpentok != value)
                {
                    messageOpentok = value;
                    RaisePropertyChanged();
                }
            }
        }

        RequestMedicalCallOpentok _requestMedicalCall = new RequestMedicalCallOpentok();

        public PrecallViewModel(IDialogService dialogService,
            INavigationService navigationService,
            IApiService apiService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _apiService = apiService;
            _player = CrossSimpleAudioPlayer.Current;
        }

        public async Task ValidateConection()
        {
            ValueProgress = 0.2;
            TextValidation = AppResources.ValidatePermissions;
            await Task.Delay(2);

            if (!CrossOpenTok.Current.CheckPermissions())
            {
                await _navigationService.Back();
                await _navigationService.Navigate(AppPages.HomeMedicalCarePage);
                return;
            }

            ValueProgress = 0.4;

            TextValidation = AppResources.TestServiceOpenTok;
            ValueProgress = 0.6;
            await Task.Delay(1);

            var responseOpentok = await _apiService.GetOpenTokRoutedSession();
            ValueProgress = 0.8;

            if (responseOpentok.StatusCode == 0 && responseOpentok.ApiKey!=0)
            {
                CrossOpenTok.Current.ApiKey = responseOpentok.ApiKey.ToString(); // keys.ApiKey;
                CrossOpenTok.Current.SessionId = responseOpentok.IdSessionTokbox; // keys.SessionId;
                CrossOpenTok.Current.UserToken = responseOpentok.OpenTokToken; // keys.Token;
                if (!CrossOpenTok.Current.TryStartSession(false))
                {
                    await _navigationService.Back();
                    await _navigationService.Navigate(AppPages.HomeMedicalCarePage);
                    return;
                }

                ValueProgress = 1;

                IQueingFirebaseService queingFirebase = ServiceLocator.Current.GetInstance<IQueingFirebaseService>();
                queingFirebase.ExistsPatient = null;

                IQueuingViewModel queuingView = ServiceLocator.Current.GetInstance<IQueuingViewModel>();
                queuingView.LoadRequestQueuing(_requestMedicalService, _requestMedicalCall);
                queuingView.Qualify = false;
                queuingView.ValideDoctor = true;

                await _navigationService.Navigate(AppPages.Queuing);
            }
            else
            {
                await _dialogService.ShowMessage(AppResources.VideoCallTitle, responseOpentok.Message);
                await _navigationService.Back();
                await _navigationService.Navigate(AppPages.HomeMedicalCarePage);
            }
        }

        public async Task GoBackPage()
        {
            await _navigationService.BackToRoot();
            await _navigationService.Navigate(Enumerations.AppPages.ServicesPage);
        }

        void CallCategory()
        {
            ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
            callViewModel.CallCategory();
        }

        public void SendMessage()
        {
            _ = CrossOpenTok.Current.SendMessageAsync(MessageOpentok, "call", false);
        }

        public async Task VideoCallStatusAsync(int type, string doctorName)
        {
            Task.Delay(1);
        }

        public void LoadRequestCall(RequestMedicalService request, RequestMedicalCallOpentok requestMedicalCall)
        {
            _requestMedicalService = request;
            _requestMedicalCall = requestMedicalCall;
        }

        public void RemovePage() => _navigationService.RemovePage(1);
    }
}
