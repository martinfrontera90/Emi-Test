namespace Emi.Portal.Movil.Logic
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System;
    using Emi.Portal.Movil.Logic.VideoCall;
    using System.IO;
    using Emi.Portal.Movil.Logic.Resources;
    using Xamarin.Forms;
    using Plugin.SimpleAudioPlayer;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Xamarin.Essentials;

    public class VideoCallPageViewModel : ViewModelBase, IMedicalVideoCallViewModel
    {
        IDialogService dialogService;

        INavigationService navigationService;

        ISimpleAudioPlayer player;

        IFileSelectService fileSelectService = ServiceLocator.Current.GetInstance<IFileSelectService>();

        IApiService apiService;

        MessagesOpentok _dataMessagesOpenTok;

        public string DoctorName { get; set; }

        public string UserName { get; set; }

        public string UrlFile { get; set; }

        public string Type { get; set; }

        public bool IsChat { get; set; }

        public string ServiceDate { get; set; }

        public string ServiceNumber { get; set; }

        private string textToSend;
        public string TextToSend
        {
            get { return textToSend; }
            set
            {
                textToSend = value;
                RaisePropertyChanged("TextToSend");
            }
        }

        private bool enableChat = false;
        public bool EnableChat
        {
            get { return enableChat; }
            set
            {
                enableChat = value;
                RaisePropertyChanged("EnableChat");
            }
        }

        private string urlImage;
        public string UrlImage
        {
            get { return urlImage; }
            set
            {
                urlImage = value;
                RaisePropertyChanged("UrlImage");
            }
        }

        private bool isVisibleImage;
        public bool IsVisibleImage
        {
            get { return isVisibleImage; }
            set
            {
                isVisibleImage = value;
                RaisePropertyChanged("IsVisibleImage");
            }
        }

        private bool isVisiblePdf;
        public bool IsVisiblePdf
        {
            get { return isVisiblePdf; }
            set
            {
                isVisiblePdf = value;
                RaisePropertyChanged("IsVisiblePdf");
            }
        }

        private bool close;
        public bool Close
        {
            get { return close; }
            set
            {
                close = value;
                RaisePropertyChanged("Close");
            }
        }

        private bool enableUpload;
        public bool EnableUpload
        {
            get { return enableUpload; }
            set
            {
                enableUpload = value;
                RaisePropertyChanged("EnableUpload");
            }
        }

        private string urlPdf;
        public string UrlPdf
        {
            get { return urlPdf; }
            set
            {
                if (urlPdf != value)
                {
                    urlPdf = value;
                    RaisePropertyChanged("UrlPdf");
                }
            }
        }

        private ObservableCollection<MessageChatViewModel> messages;
        public ObservableCollection<MessageChatViewModel> Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                RaisePropertyChanged("Messages");
            }
        }

        private bool publisherIsVisible;
        public bool PublisherIsVisible
        {
            get { return publisherIsVisible; }
            set
            {
                publisherIsVisible = value;
                RaisePropertyChanged("PublisherIsVisible");
            }
        }

        private bool subscriberIsVisible;
        public bool SubscriberIsVisible
        {
            get { return subscriberIsVisible; }
            set
            {
                subscriberIsVisible = value;
                RaisePropertyChanged("SubscriberIsVisible");
            }
        }

        private bool iSmessageCalled;
        public bool IsMessageCalled
        {
            get { return iSmessageCalled; }
            set
            {
                iSmessageCalled = value;
                RaisePropertyChanged("IsMessageCalled");
            }
        }

        private bool iSacceptcall;
        public bool IsAcceptcall
        {
            get { return iSacceptcall; }
            set
            {
                iSacceptcall = value;
                RaisePropertyChanged("IsAcceptcall");
            }
        }

        private bool iSrejectedCall;
        public bool IsRejectedCall
        {
            get { return iSrejectedCall; }
            set
            {
                iSrejectedCall = value;
                RaisePropertyChanged("IsRejectedCall");
            }
        }

        private bool iScallIcon;
        public bool IsCallIcon
        {
            get { return iScallIcon; }
            set
            {
                iScallIcon = value;
                RaisePropertyChanged("IsCallIcon");
            }
        }

        private string vidoCallText;
        public string VidoCallText
        {
            get { return vidoCallText; }
            set
            {
                vidoCallText = value;
                RaisePropertyChanged("VidoCallText");
            }
        }

        private string callWarningMessage;
        public string CallWarningMessage
        {
            get { return callWarningMessage; }
            set
            {
                callWarningMessage = value;
                RaisePropertyChanged("callWarningMessage");
            }
        }

        private WebView htmlSourceText;
        public WebView HtmlSourceText
        {
            get { return htmlSourceText; }
            set
            {
                if (htmlSourceText != value)
                {
                    htmlSourceText = value;
                    RaisePropertyChanged("HtmlSourceText");
                }
            }
        }

        public Stream ringtone;
        public Stream Ringtone
        {
            get { return ringtone; }
            set
            {
                ringtone = value;

                if (value != null)
                    player.Load(value);

                RaisePropertyChanged("Ringtone");
            }
        }

        private bool iSplayerSound;
        public bool IsPlayerSound
        {
            get { return iSplayerSound; }
            set
            {
                iSplayerSound = value;
                RaisePropertyChanged("IsPlayerSound");
            }
        }

        private bool iSwaiting;
        public bool IsWaiting
        {
            get { return iSwaiting; }
            set
            {
                iSwaiting = value;
                RaisePropertyChanged("IsWaiting");
            }
        }

        public string WarningMessage
        {
            get => AppResources.CallWarningMessage;
        }

        IServicesPageViewModel IMedicalVideoCallViewModel.ServicesPage => throw new NotImplementedException();

        public MessagesOpentok MessagesOpentok
        {
            get => _dataMessagesOpenTok;
            set => _dataMessagesOpenTok = value;
        }

        RequestMedicalCallOpentok _requestMedicalCall = new RequestMedicalCallOpentok();

        public IServicesPageViewModel ServicesPage = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();

        public ICommand CallCategoryCommand { get { return new RelayCommand(CallCategory); } }

        public ICommand EndCallCommand { get { return new RelayCommand(EndCall); } }

        public ICommand RejectedCallCommand { get { return new RelayCommand(RejectedCall); } }

        public ICommand AcceptcallCommand { get { return new RelayCommand(Acceptcall); } }

        public ICommand OpenChatCommand { get { return new RelayCommand(OpenChat); } }

        public ICommand FileCommand { get { return new RelayCommand(AddFile); } }

        public ICommand CloseFileCommand { get { return new RelayCommand(CloseFile); } }

        public ICommand SaveFileCommand { get { return new RelayCommand(SaveFile); } }

        private async void SaveFile()
        {
            await dialogService.ShowMessage("Atención", "Se abrirá una pantalla nueva de la cual podrás descargar el archivo");
            await Browser.OpenAsync(UrlFile, BrowserLaunchMode.SystemPreferred);
        }

        private void CloseFile()
        {
            IsVisibleImage = false;
            IsVisiblePdf = false;
            Close = false;
        }

        private async void AddFile()
        {
            try
            {
                ResponseFile fileResponse = null;
                FileRequest file = await fileSelectService.AddFile(ServiceDate, ServiceNumber);

                if (file.FileData != null)
                {
                    dialogService.ShowProgress("Subiendo Archivo");
                    fileResponse = await apiService.PostFile(file);
                }

                if (!string.IsNullOrWhiteSpace(fileResponse.urlFileInBlob) && fileResponse.Success)
                {
                    Messages.Insert(0, new MessageChatViewModel()
                    {
                        UrlFile = fileResponse.urlFileInBlob,
                        Text = "Ha compartido un archivo",
                        User = "User",
                        CompleteName = UserName,
                        IsFile = true
                    });
                    string val = $"{{\"msg\":\"Ha compartido un archivo\",\"type\":3, \"urlFile\":\"{fileResponse.urlFileInBlob}\"}}";
                    await CrossOpenTok.Current.SendMessageAsync(val, "chat", true);
                    dialogService.HideProgress("Subiendo Archivo");
                    await dialogService.AlertIcon("", "Archivo enviado con éxito");

                }
                else
                {
                    dialogService.HideProgress("Subiendo Archivo");
                    await dialogService.ShowMessage("", "No se pudo adjuntar el archivo");
                }
            }
            catch (Exception e)
            {
                await dialogService.ShowMessage("Error", "No se pudo enviar el archivo");
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                
            }


        }

        private void OpenChat()
        {
            EnableChat = !EnableChat;
        }

        public ICommand SendMessageCommand { get { return new RelayCommand(SendChatMessage); } }

        private void SendChatMessage()
        {
            if (!string.IsNullOrWhiteSpace(TextToSend))
            {
                Messages.Insert(0, new MessageChatViewModel()
                {
                    Text = TextToSend,
                    User = "User",
                    CompleteName = UserName,
                    IsFile = false
                });
                string val = $"{{\"msg\":\"{TextToSend.Trim()}\",\"type\":2}}";
                Type = "chat";
                CrossOpenTok.Current.SendMessageAsync(val, "chat", true);
            }
            TextToSend = string.Empty;
            Type = string.Empty;
        }

        private void CallCategory()
        {
            ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
            callViewModel.CallCategory();
        }

        void EndCall()
        {
            VideoCallStatusAsync((int)CallStatus.Refused, null);
            CrossOpenTok.Current.EndSession(true, true);
            navigationService.Back();
        }

        void RejectedCall()
        {
            IsCallIcon = false;
            IsMessageCalled = true;
            IsWaiting = false;
            IsAcceptcall = false;
            IsRejectedCall = false;
            PublisherIsVisible = false;
            SubscriberIsVisible = false;
            _requestMedicalCall.type = (int)CallStatus.Refused;
            VideoCallStatusAsync((int)CallStatus.Refused, null);
            string values = JsonConvert.SerializeObject(_requestMedicalCall);
            _ = CrossOpenTok.Current.SendMessageAsync(values, "call", false);
        }

        void Acceptcall()
        {
            Close = false;
            EnableChat = false;
            EnableUpload = false;
            SubscriberIsVisible = true;
            PublisherIsVisible = true;
            IsPlayerSound = false;
            IsCallIcon = false;
            IsWaiting = false;
            IsMessageCalled = true;
            VidoCallText = AppResources.MessageWelcomeVideoCall;
            IsAcceptcall = false;
            IsRejectedCall = false;
            _requestMedicalCall.type = (int)CallStatus.Accepted;
            VideoCallStatusAsync((int)CallStatus.Accepted, null);
            string values = JsonConvert.SerializeObject(_requestMedicalCall);
            _ = CrossOpenTok.Current.SendMessageAsync(values, "call", false);
        }

        public async Task VideoCallStatusAsync(int type, string doctorName, bool sendBackToRoot = true, bool showEvaluateCall = false)
        {
            IsMessageCalled = true;
            IsWaiting = true;
            switch (type)
            {
                case (int)CallStatus.Waiting:
                    IsAcceptcall = false;
                    IsRejectedCall = false;
                    DoctorName = doctorName;
                    VidoCallText = string.IsNullOrEmpty(doctorName) ? AppResources.WaitPlease : string.Format(AppResources.MessageWaitCall, doctorName);
                    break;
                case (int)CallStatus.Ringing:
                    IsPlayerSound = true;
                    IsMessageCalled = false;
                    IsCallIcon = true;
                    IsAcceptcall = true;
                    IsRejectedCall = true;
                    Callplayer();
                    break;
                case (int)CallStatus.CancellRing:
                    IsCallIcon = false;
                    IsMessageCalled = true;
                    IsAcceptcall = false;
                    IsRejectedCall = false;
                    IsWaiting = false;
                    PublisherIsVisible = false;
                    SubscriberIsVisible = false;
                    PublisherIsVisible = false;
                    subscriberIsVisible = false;
                    player.Stop();
                    //VidoCallText = AppResources.WaitPlease;
                    break;

                case (int)CallStatus.Refused:
                    player.Stop();
                    break;
                case (int)CallStatus.Accepted:
                    Xamarin.Essentials.DeviceDisplay.KeepScreenOn = true;

                    IsMessageCalled = false;
                    player.Stop();
                    break;
                case (int)CallStatus.lostCall:
                    PublisherIsVisible = false;
                    SubscriberIsVisible = false;
                    IsMessageCalled = false;
                    player.Stop();
                    CrossOpenTok.Current.EndSession(false, false);
                    Xamarin.Essentials.DeviceDisplay.KeepScreenOn = false;
                    await dialogService.ShowMessage(AppResources.TitleMessageWaitCall, AppResources.Lostcall);
                    await navigationService.BackToRoot();
                    await navigationService.Navigate(AppPages.ServicesPage);
                    break;
                case (int)CallStatus.Ending:
                    IsCallIcon = false;
                    IsMessageCalled = false;
                    IsAcceptcall = false;
                    IsRejectedCall = false;
                    PublisherIsVisible = false;
                    SubscriberIsVisible = false;
                    player.Stop();
                    CrossOpenTok.Current.EndSession(sendBackToRoot, false);
                    Xamarin.Essentials.DeviceDisplay.KeepScreenOn = false;

                    if (showEvaluateCall && !string.IsNullOrEmpty(_dataMessagesOpenTok?.CallHistoryId))
                        await navigationService.Navigate(AppPages.EvaluateVideoCallPage);
                    else if (sendBackToRoot)
                    {
                        if ((Application.Current.MainPage as MasterDetailPage)?.Detail?.Navigation?.NavigationStack.Count > 3)
                        {
                            //await navigationService.BackToRoot();
                            //await navigationService.Navigate(AppPages.ServicesPage);
                            IQueuingViewModel queuingView = ServiceLocator.Current.GetInstance<IQueuingViewModel>();
                            queuingView.Qualify = false;
                            await navigationService.Navigate(AppPages.Queuing);
                        }
                    }
                    break;
            }
        }

        private void Callplayer()
        {
            player.Stop();
            player.Loop = true;
            player.Play();
        }

        public void SendMessage()
        {
            _ = CrossOpenTok.Current.SendMessageAsync(ServiceLocator.Current.GetInstance<IQueuingViewModel>().MessageOpentok, "call", false);
        }

        public void LoadCallHistoryId(MessagesOpentok data)
        {
            _dataMessagesOpenTok = data;
        }

        public VideoCallPageViewModel(IDialogService dialogService, INavigationService navigationService, IApiService apiService)
        {
            Messages = new ObservableCollection<MessageChatViewModel>();
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            player = CrossSimpleAudioPlayer.Current;
            LoadData();
            Xamarin.Essentials.DeviceDisplay.KeepScreenOn = true;
        }

        void LoadData()
        {
            VidoCallText = AppResources.WaitPlease;
            IsMessageCalled = true;
            IsWaiting = true;
            PublisherIsVisible = false;
            subscriberIsVisible = false;
            IsCallIcon = false;
            IsPlayerSound = false;
        }

        public void RemovePage() => navigationService.RemovePage(1);
    }
}
