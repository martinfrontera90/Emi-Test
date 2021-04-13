using Emi.Portal.Movil.Droid.OpenTok;
using Emi.Portal.Movil.Logic.VideoCall;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(OpenTokSubscriberView), typeof(OpenTokSubscriberViewRenderer))]
namespace Emi.Portal.Movil.Droid.Service
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Android;
    using Android.Content.PM;
    using Android.Runtime;
    using Android.Support.V4.App;
    using Android.Support.V4.Content;
    using Com.Opentok.Android;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Newtonsoft.Json;
    using Plugin.CurrentActivity;

    [Preserve(AllMembers = true)]
    public sealed class PlatformOpenTokService : BaseOpenTokService
    {
        IMedicalVideoCallViewModel _medicalVideoCall = ServiceLocator.Current.GetInstance<IMedicalVideoCallViewModel>();

        const int RequestId = 0;

        MessagesOpentok messagesOpentok = new MessagesOpentok();

        private readonly object _locker = new object();

        public Connection InvitedConnection { get; set; }

        public event Action PublisherUpdated;

        public event Action SubscriberUpdated;

        private readonly string[] _requestPermissions = {
            Manifest.Permission.Camera,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.RecordAudio,
            Manifest.Permission.ModifyAudioSettings,
            Manifest.Permission.Internet,
            Manifest.Permission.AccessNetworkState,
            Manifest.Permission.WakeLock
        };

        public static PlatformOpenTokService Instance => CrossOpenTok.Current as PlatformOpenTokService;

        public Session Session { get; private set; }

        public PublisherKit PublisherKit { get; private set; }

        public SubscriberKit SubscriberKit { get; private set; }

        private PlatformOpenTokService()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void SendMessage() => _medicalVideoCall.SendMessage();

        public void ClearPublisherUpdated() => PublisherUpdated = null;

        public void ClearSubscribeUpdated() => SubscriberUpdated = null;

        public static void Init()
        {
            OpenTokPublisherViewRenderer.Preserve();
            OpenTokSubscriberViewRenderer.Preserve();
            CrossOpenTok.Init(() => new PlatformOpenTokService());
        }

        public override bool CheckPermissions()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            var shouldGrantPermissions = _requestPermissions.Any(permission => ContextCompat.CheckSelfPermission(activity, permission) != (int)Permission.Granted);

            if (shouldGrantPermissions)
                ActivityCompat.RequestPermissions(activity, _requestPermissions, RequestId);

            return !shouldGrantPermissions;
        }

        public override bool TryStartSession(bool sendBackToRoot)
        {
            if (!CheckPermissions() ||
                string.IsNullOrWhiteSpace(ApiKey) ||
                string.IsNullOrWhiteSpace(SessionId) ||
                string.IsNullOrWhiteSpace(UserToken))
            {
                return false;
            }

            IsSessionStarted = true;
            EndSession(sendBackToRoot, false);
            IsVideoPublishingEnabled = true;
            IsAudioPublishingEnabled = true;
            IsVideoSubscriptionEnabled = true;
            IsAudioSubscriptionEnabled = true;
            IsSubscriberVideoEnabled = true;

            Session = new Session.Builder(CrossCurrentActivity.Current.AppContext, ApiKey, SessionId).Build();
            Session.ConnectionDestroyed += OnConnectionDestroyed;
            Session.Connected += OnDidConnect;
            Session.StreamReceived += OnStreamCreated;
            Session.StreamDropped += OnStreamDestroyed;
            Session.Error += OnError;
            Session.Signal += OnSignal;
            Session.Connect(UserToken);

            return true;
        }

        public override void EndSession(bool sendBackToRoot, bool showEvaluateCall)
        {
            try
            {
                if (Session == null && SubscriberKit == null && PublisherKit == null)
                    return;

                lock (_locker)
                {
                    if (SubscriberKit != null)
                    {
                        SubscriberKit.SubscribeToAudio = false;
                        SubscriberKit.SubscribeToVideo = false;
                        SubscriberKit.Connected -= OnSubscriberConnected;
                        SubscriberKit.StreamDisconnected -= OnSubscriberDisconnected;
                        SubscriberKit.SubscriberDisconnected -= OnSubscriberDisconnected;
                        SubscriberKit.VideoDisabled -= OnSubscriberVideoDisabled;
                        SubscriberKit.VideoEnabled -= OnSubscriberVideoEnabled;
                        SubscriberKit.Dispose();
                        SubscriberKit = null;
                    }

                    if (PublisherKit != null)
                    {
                        PublisherKit.PublishAudio = false;
                        PublisherKit.PublishVideo = false;
                        PublisherKit.StreamCreated -= OnPublisherStreamCreated;
                        PublisherKit.Dispose();
                        PublisherKit = null;
                    }

                    if (Session != null)
                    {
                        Session.ConnectionDestroyed -= OnConnectionDestroyed;
                        Session.Connected -= OnDidConnect;
                        Session.StreamReceived -= OnStreamCreated;
                        Session.StreamDropped -= OnStreamDestroyed;
                        Session.Error -= OnError;
                        Session.Signal -= OnSignal;
                        Session.Disconnect();
                        Session.Dispose();
                        Session = null;
                    }
                }

                ClearPublisherUpdated();
                ClearSubscribeUpdated();
                _medicalVideoCall.VideoCallStatusAsync((int)CallStatus.Ending, null, sendBackToRoot, showEvaluateCall);
            }
            finally
            {
                InvitedConnection = null;
                _medicalVideoCall.Messages.Clear();
                IsSessionStarted = false;
                IsPublishingStarted = false;
            }
        }

        public override Task<bool> SendMessageAsync(string message, string type, bool to = false)
        {
            if (to)
            {
                Session.SendSignal(type, message, InvitedConnection);
            }
            else
                Session.SendSignal(type, message);
            return Task.FromResult(true);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PublisherKit != null)
            {
                switch (e.PropertyName)
                {
                    case nameof(IsVideoPublishingEnabled):
                        PublisherKit.PublishVideo = IsVideoPublishingEnabled;
                        return;
                    case nameof(IsAudioPublishingEnabled):
                        PublisherKit.PublishAudio = IsAudioPublishingEnabled;
                        return;
                }
            }
            if (SubscriberKit != null)
            {
                switch (e.PropertyName)
                {
                    case nameof(IsVideoSubscriptionEnabled):
                        SubscriberKit.SubscribeToVideo = IsVideoSubscriptionEnabled;
                        return;
                    case nameof(IsAudioSubscriptionEnabled):
                        SubscriberKit.SubscribeToAudio = IsAudioSubscriptionEnabled;
                        return;
                }
            }
        }

        public override void CycleCamera() => (PublisherKit as Publisher)?.CycleCamera();

        private void OnConnectionDestroyed(object sender, Session.ConnectionDestroyedEventArgs e) => EndSession(true, true);

        private void OnDidConnect(object sender, EventArgs e)
        {
            lock (_locker)
            {
                if (Session == null ||
                    PublisherKit != null)
                {
                    return;
                }

                PublisherKit = new Publisher.Builder(CrossCurrentActivity.Current.AppContext).Build();
                PublisherKit.StreamCreated += OnPublisherStreamCreated;

                PublisherKit.SetStyle(BaseVideoRenderer.StyleVideoScale, BaseVideoRenderer.StyleVideoFill);
                PublisherKit.PublishAudio = IsAudioPublishingEnabled;
                PublisherKit.PublishVideo = IsVideoPublishingEnabled;
                PublisherUpdated?.Invoke();
                Session.Publish(PublisherKit);
                PublisherKit.PublishVideo = IsVideoPublishingEnabled;
            }
        }

        private void OnStreamCreated(object sender, Session.StreamReceivedEventArgs e)
        {
            lock (_locker)
            {
                if (Session == null ||
                    SubscriberKit != null)
                {
                    return;
                }

                SubscriberKit = new Subscriber.Builder(CrossCurrentActivity.Current.AppContext, e.P1).Build();
                SubscriberKit.Connected += OnSubscriberConnected;
                SubscriberKit.StreamDisconnected += OnSubscriberDisconnected;
                SubscriberKit.SubscriberDisconnected += OnSubscriberDisconnected;
                SubscriberKit.VideoDisabled += OnSubscriberVideoDisabled;
                SubscriberKit.VideoEnabled += OnSubscriberVideoEnabled;

                SubscriberKit.SetStyle(BaseVideoRenderer.StyleVideoScale, BaseVideoRenderer.StyleVideoFill);
                SubscriberKit.SubscribeToAudio = IsAudioSubscriptionEnabled;
                SubscriberKit.SubscribeToVideo = IsVideoSubscriptionEnabled;
                SubscriberUpdated?.Invoke();
                Session.Subscribe(SubscriberKit);
            }
        }

        private void OnStreamDestroyed(object sender, Session.StreamDroppedEventArgs e)
        {

            SubscriberUpdated?.Invoke();

            if (messagesOpentok.type == (int)CallStatus.CancellRing)
                _medicalVideoCall.VideoCallStatusAsync((int)CallStatus.CancellRing, null, true);
        }

        private void OnError(object sender, Session.ErrorEventArgs e)
        {
            RaiseError(e.P1.Message);
            EndSession(true, false);
        }

        private void OnSubscriberVideoDisabled(object sender, Subscriber.VideoDisabledEventArgs e)
        => IsSubscriberVideoEnabled = false;

        private void OnSubscriberVideoEnabled(object sender, Subscriber.VideoEnabledEventArgs e)
        {
            lock (_locker)
            {
                IsSubscriberVideoEnabled = SubscriberKit?.Stream?.HasVideo ?? false;
            }
        }

        private void OnSubscriberConnected(object sender, EventArgs e) => OnSubscriberConnectionChanged(true);

        private void OnSubscriberDisconnected(object sender, EventArgs e) => OnSubscriberConnectionChanged(false);

        private void OnSubscriberConnectionChanged(bool isConnected)
        {
            lock (_locker)
            {
                if (SubscriberKit != null)
                {
                    SubscriberUpdated?.Invoke();
                    IsSubscriberConnected = isConnected;
                    IsSubscriberVideoEnabled = SubscriberKit?.Stream?.HasVideo ?? false;
                    PublisherUpdated?.Invoke();
                }
            }
        }

        private void OnPublisherStreamCreated(object sender, PublisherKit.StreamCreatedEventArgs e)
        {
            IsPublishingStarted = true;
            SendMessage();
        }

        private void OnSignal(object sender, Session.SignalEventArgs e)
        {
            try
            {
                messagesOpentok = JsonConvert.DeserializeObject<MessagesOpentok>(e.P2);

                if (e.P1 == "infoCall")
                {
                    _medicalVideoCall.LoadCallHistoryId(messagesOpentok);
                    _medicalVideoCall.ServiceDate = messagesOpentok.ServiceDate;
                    _medicalVideoCall.ServiceNumber = messagesOpentok.ServiceNumber;
                }

                if (!String.IsNullOrEmpty(messagesOpentok.from))
                    _medicalVideoCall.VideoCallStatusAsync(messagesOpentok.type, messagesOpentok.info?.doctorName, true);

                if (e.P1.Equals("call") && messagesOpentok.type == 1 && e.P0.Connection.ConnectionId != e.P3.ConnectionId)
                    InvitedConnection = e.P3;

                if (e.P1.Equals("file"))
                    if (messagesOpentok.status == 3)
                    {
                        _medicalVideoCall.EnableUpload = true;
                    }
                    else if (messagesOpentok.status == 2)
                    {
                        _medicalVideoCall.EnableUpload = false;
                    }

                SendMessageAsync($"{{\"msg\":\"OK\",\"type\":1}}", "chat", true);
                SendMessageAsync($"{{\"status\":1}}", "file", true);

                if (e.P1.Equals("chat"))
                {
                    MessageChatViewModel message;
                    if (messagesOpentok.type == 2)
                    {
                        message = new MessageChatViewModel
                        {
                            Text = messagesOpentok.msg,
                            User = "Doctor",
                            CompleteName = $"Dr. {_medicalVideoCall.DoctorName}",
                            IsFile = false
                        };
                        _medicalVideoCall.EnableChat = true;
                        _medicalVideoCall.Messages.Insert(0, message);
                    }
                    else if (messagesOpentok.type == 3)
                    {
                        message = new MessageChatViewModel
                        {
                            UrlFile = messagesOpentok.UrlFile,
                            Text = messagesOpentok.msg,
                            User = "Doctor",
                            CompleteName = $"Dr. {_medicalVideoCall.DoctorName}",
                            IsFile = true
                        };
                        _medicalVideoCall.EnableChat = true;
                        _medicalVideoCall.Messages.Insert(0, message);
                    }
                }

                RaiseMessageReceived(e.P2);
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(ex);
            }
        }
    }
}