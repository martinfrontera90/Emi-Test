namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    public interface IRegisterPageViewModel
    {
        ICommand CancelCommand { get; }
        ICommand CompleteCommand { get; }
        ICommand DataManagementPolicyCommand { get; }
        ICommand InformationCommand { get; }
        ICommand NextToDataPersonalCommand { get; }
        ICommand NextToNameCommand { get; }
        ICommand NextToPasswordCommand { get; }
        ICommand SendVerificationCodeCommand { get; }
        ICommand TermsAndConditionsCommmand { get; }
        ICommand VerificationCodeCommand { get; }
        void Cancel();
        void Clean();
        Task LoadPage();
        Task LoadDocuments();
        ObservableCollection<Document> Documents { get; set; }
    }
}

