namespace Emi.Portal.Movil.Logic.VideoCall
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    public interface IOpenTokService : INotifyPropertyChanged
    {
        event Action<string> Error;

        event Action<string> MessageReceived;

        bool IsVideoPublishingEnabled { get; set; }

        bool IsAudioPublishingEnabled { get; set; }

        bool IsVideoSubscriptionEnabled { get; set; }

        bool IsAudioSubscriptionEnabled { get; set; }

        bool IsSubscriberVideoEnabled { get; set; }

        string ApiKey { get; set; }

        string SessionId { get; set; }

        string UserToken { get; set; }

        bool IsSessionStarted { get; set; }

        bool IsPublishingStarted { get; set; }

        bool IsMessageCalled { get; set; }

        bool CheckPermissions();

        bool TryStartSession(bool sendBackToRoot);

        void EndSession(bool sendBackToRoot, bool showEvaluateCall);

        void CycleCamera();

        Task<bool> SendMessageAsync(string message, string type, bool to);
    }
}
