using Emi.Portal.Movil.iOS.OpenTok;
using Emi.Portal.Movil.Logic.VideoCall;
using OpenTok;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(OpenTokSubscriberView), typeof(OpenTokSubscriberViewRenderer))]
namespace Emi.Portal.Movil.OpenTok.iOS.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using AVFoundation;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Foundation;
    using Newtonsoft.Json;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    [Preserve(AllMembers = true)]
    public class PlatformOpenTokService : BaseOpenTokService
    {
        IMedicalVideoCallViewModel _medicalVideoCall = ServiceLocator.Current.GetInstance<IMedicalVideoCallViewModel>();

        public event Action PublisherUpdated;

        public event Action SubscriberUpdated;

        private readonly object _locker = new object();

        public OTConnection InvitedConnection { get; set; }

        MessagesOpentok messagesOpentok = new MessagesOpentok();

        public OTSession Session { get; private set; }

        public OTPublisher PublisherKit { get; private set; }

        public OTSubscriber SubscriberKit { get; private set; }

        private PlatformOpenTokService()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void VideoCallStatus(int type, string doctorName, bool sendBackToRoot, bool showEvaluateCall)
        {
            _medicalVideoCall.VideoCallStatusAsync(type, doctorName, sendBackToRoot, showEvaluateCall);
        }

        private void SendMessage()
        {
            _medicalVideoCall.SendMessage();
        }

        public static PlatformOpenTokService Instance => CrossOpenTok.Current as PlatformOpenTokService;

        public void ClearPublisherUpdated() => PublisherUpdated = null;

        public void ClearSubscribeUpdated() => SubscriberUpdated = null;

        public static void Init()
        {
            OpenTokPublisherViewRenderer.Preserve();
            OpenTokSubscriberViewRenderer.Preserve();
            CrossOpenTok.Init(() => new PlatformOpenTokService());
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

            Session = new OTSession(ApiKey, SessionId, null);
            Session.ConnectionDestroyed += OnConnectionDestroyed;
            Session.DidConnect += OnDidConnect;
            Session.StreamCreated += OnStreamCreated;
            Session.StreamDestroyed += OnStreamDestroyed;
            Session.DidFailWithError += OnError;
            Session.ReceivedSignalType += OnSignalReceived;
            Session.Init();

            Session.ConnectWithToken(UserToken, out OTError error);
            return true;
        }

        public override void EndSession(bool sendBackToRoot, bool showEvaluateCall)
        {
            try
            {
                if (Session == null)
                {
                    return;
                }

                lock (_locker)
                {
                    if (SubscriberKit != null)
                    {
                        SubscriberKit.SubscribeToAudio = false;
                        SubscriberKit.SubscribeToVideo = false;
                        SubscriberKit.DidConnectToStream -= OnSubscriberConnected;
                        SubscriberKit.DidDisconnectFromStream -= OnSubscriberDisconnected;
                        SubscriberKit.VideoDataReceived -= OnSubscriberVideoDataReceived;
                        SubscriberKit.VideoEnabled -= OnSubscriberVideoEnabled;
                        SubscriberKit.VideoDisabled -= OnSubscriberVideoDisabled;
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
                        Session.DidConnect -= OnDidConnect;
                        Session.StreamCreated -= OnStreamCreated;
                        Session.StreamDestroyed -= OnStreamDestroyed;
                        Session.DidFailWithError -= OnError;
                        Session.ReceivedSignalType -= OnSignalReceived;
                        Session.Disconnect();
                        Session.Dispose();
                        Session = null;
                    }
                }
                ClearPublisherUpdated();
                ClearSubscribeUpdated();
                VideoCallStatus((int)CallStatus.Ending, null, sendBackToRoot, showEvaluateCall);
            }
            finally
            {
                _medicalVideoCall.Messages.Clear();
                IsSessionStarted = false;
                IsPublishingStarted = false;
            }
        }

        public override bool CheckPermissions()
        {
            bool result = true;
            Dictionary<Permission, PermissionStatus> newStatus = new Dictionary<Permission, PermissionStatus>();

            var resultCheck = CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera).Result;

            if (resultCheck == PermissionStatus.Unknown)
            {
                Task.Run(async () =>
                {
                    newStatus = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                }).GetAwaiter().GetResult();


                if (newStatus.ContainsKey(Permission.Camera) && newStatus[Permission.Camera] != PermissionStatus.Granted)
                {
                    result = false;
                    return result;
                }
            }
            else if (resultCheck == PermissionStatus.Denied)
                return false;

            resultCheck = CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Microphone).Result;

            if (resultCheck == PermissionStatus.Unknown)
            {
                Task.Run(async () =>
                {
                    newStatus = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Microphone);
                }).GetAwaiter().GetResult();

                if (newStatus.ContainsKey(Permission.Microphone) && newStatus[Permission.Microphone] != PermissionStatus.Granted)
                {
                    result = false;
                    return result;
                }
            }
            else if (resultCheck == PermissionStatus.Denied)
                return false;

            return result;
        }

        public override Task<bool> SendMessageAsync(string message, string type, bool to = false)
        {
            OTError error = null;
            if (to)
            {
                Session.SignalWithType(type, message, InvitedConnection, out error);
            }
            else
                Session.SignalWithType(type, message, null, out error);
            return Task.FromResult(error == null);
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

        public override void CycleCamera()
        {
            if (PublisherKit == null)
            {
                return;
            }

            PublisherKit.CameraPosition = PublisherKit.CameraPosition == AVCaptureDevicePosition.Front
                ? AVCaptureDevicePosition.Back
                : AVCaptureDevicePosition.Front;
        }

        private void OnConnectionDestroyed(object sender, OTSessionDelegateConnectionEventArgs e) => EndSession(true, true);

        private void OnDidConnect(object sender, EventArgs e)
        {
            lock (_locker)
            {
                if (Session == null || PublisherKit != null)
                {
                    return;
                }

                PublisherKit = new OTPublisher(null, new OTPublisherSettings
                {
                    CameraFrameRate = OTCameraCaptureFrameRate.OTCameraCaptureFrameRate15FPS,
                    CameraResolution = OTCameraCaptureResolution.High,
                })
                {
                    PublishVideo = IsVideoPublishingEnabled,
                    PublishAudio = IsAudioPublishingEnabled
                };

                PublisherKit.StreamCreated += OnPublisherStreamCreated;
                PublisherUpdated?.Invoke();
                Session.Publish(PublisherKit, out OTError error);

            }
        }

        private void OnStreamCreated(object sender, OTSessionDelegateStreamEventArgs e)
        {
            lock (_locker)
            {
                if (Session == null || SubscriberKit != null)
                {
                    return;
                }

                SubscriberKit = new OTSubscriber(e.Stream, null)
                {
                    SubscribeToVideo = IsVideoSubscriptionEnabled
                };
                SubscriberKit.DidConnectToStream += OnSubscriberConnected;
                SubscriberKit.DidDisconnectFromStream += OnSubscriberDisconnected;
                SubscriberKit.VideoDataReceived += OnSubscriberVideoDataReceived;
                SubscriberKit.VideoEnabled += OnSubscriberVideoEnabled;
                SubscriberKit.VideoDisabled += OnSubscriberVideoDisabled;
                SubscriberKit.SubscribeToAudio = IsAudioSubscriptionEnabled;
                SubscriberUpdated?.Invoke();
                Session.Subscribe(SubscriberKit, out OTError error);
            }
        }

        private void OnStreamDestroyed(object sender, OTSessionDelegateStreamEventArgs e)
        {
            SubscriberUpdated?.Invoke();

            if (messagesOpentok.type == (int)CallStatus.CancellRing)
            {
                VideoCallStatus((int)CallStatus.CancellRing, null, true, false);
            }
        }

        private void OnError(object sender, OTSessionDelegateErrorEventArgs e)
        {
            RaiseError(e.Error?.Code.ToString());
            EndSession(true, false);
        }

        private void OnSubscriberVideoDisabled(object sender, OTSubscriberKitDelegateVideoEventReasonEventArgs e)
        => IsSubscriberVideoEnabled = false;

        private void OnSubscriberVideoDataReceived(object sender, EventArgs e)
        => SubscriberUpdated?.Invoke();

        private void OnSubscriberVideoEnabled(object sender, OTSubscriberKitDelegateVideoEventReasonEventArgs e)
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

        private void OnPublisherStreamCreated(object sender, OTPublisherDelegateStreamEventArgs e)
        {
            IsPublishingStarted = true;
            SendMessage();
        }

        private void OnSignalReceived(object sender, OTSessionDelegateSignalEventArgs e)
        {
            try
            {
                messagesOpentok = JsonConvert.DeserializeObject<MessagesOpentok>(e.StringData);

                if (e.Type == "infoCall")
                {
                    _medicalVideoCall.LoadCallHistoryId(messagesOpentok);
                    _medicalVideoCall.ServiceDate = messagesOpentok.ServiceDate;
                    _medicalVideoCall.ServiceNumber = messagesOpentok.ServiceNumber;
                }

                if (!String.IsNullOrEmpty(messagesOpentok.from))
                    _medicalVideoCall.VideoCallStatusAsync(messagesOpentok.type, messagesOpentok.info?.doctorName, true);

                if (e.Type.Equals("call") && messagesOpentok.type == 1 && Session.Connection.ConnectionId != e.Connection.ConnectionId)
                    InvitedConnection = e.Connection;

                SendMessageAsync($"{{\"msg\":\"OK\",\"type\":1}}", "chat", true);
                SendMessageAsync($"{{\"status\":1}}", "file", true);

                if (e.Type.Equals("file"))
                    if (messagesOpentok.status == 3)
                    {
                        _medicalVideoCall.EnableUpload = true;
                    }
                    else if (messagesOpentok.status == 2)
                    {
                        _medicalVideoCall.EnableUpload = false;
                    }

                if (e.Type.Equals("chat"))
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


                RaiseMessageReceived(e.StringData);
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(ex);
            }
        }
    }
}