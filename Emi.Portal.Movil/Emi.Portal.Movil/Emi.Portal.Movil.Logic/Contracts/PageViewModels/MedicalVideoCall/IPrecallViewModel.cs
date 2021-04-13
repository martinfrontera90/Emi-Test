namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Models.Requests;

    public interface IPrecallViewModel
    {
        ICommand CallCategoryCommand { get; }

        IServicesPageViewModel ServicesPage { get; }

        string TextValidation { get; set; }

        double ValueProgress { get; set; }

        string MessageOpentok { get; set; }

        void SendMessage();

        Task VideoCallStatusAsync(int type, string doctorName);

        Task ValidateConection();

        void LoadRequestCall(RequestMedicalService request, RequestMedicalCallOpentok requestMedicalCall);

        void RemovePage();
    }
}
