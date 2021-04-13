namespace Emi.Portal.Movil.Logic
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Xamarin.Forms;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    public interface IMedicalVideoCallViewModel
    {
        ICommand CallCategoryCommand { get; }

        ICommand EndCallCommand { get; }

        ICommand RejectedCallCommand { get; }

        ICommand AcceptcallCommand { get; }

        bool EnableChat { get; set; }

        string DoctorName { get; set; }

        bool IsVisiblePdf { get; set; }

        bool EnableUpload { get; set; }

        bool IsVisibleImage { get; set; }

        string UserName { get; set; }

        string Type { get; set; }

        string ServiceDate { get; set; }

        string UrlFile { get; set; }

        string ServiceNumber { get; set; }

        ObservableCollection<MessageChatViewModel> Messages { get; set; }

        bool Close { get; set; }

        string UrlPdf { get; set; }

        string UrlImage { get; set; }

        bool PublisherIsVisible { get; set; }

        bool SubscriberIsVisible { get; set; }

        bool IsMessageCalled { get; set; }

        bool IsAcceptcall { get; set; }

        bool IsRejectedCall { get; set; }

        bool IsCallIcon { get; set; }

        string VidoCallText { get; set; }

        string CallWarningMessage { get; set; }

        bool IsPlayerSound { get; set; }

        Stream Ringtone { get; set; }

        MessagesOpentok MessagesOpentok { get; set; }

        Task VideoCallStatusAsync(int type, string doctorName, bool sendBackToRoot = true, bool showEvaluateCall = false);

        void SendMessage();

        WebView HtmlSourceText { get; set; }

        IServicesPageViewModel ServicesPage { get; }

        void LoadCallHistoryId(MessagesOpentok data);

        void RemovePage();
    }
}