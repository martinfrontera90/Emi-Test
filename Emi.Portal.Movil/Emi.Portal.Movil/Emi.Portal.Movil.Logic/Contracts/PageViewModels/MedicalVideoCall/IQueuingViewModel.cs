using System.Threading.Tasks;
using System.Windows.Input;
using Emi.Portal.Movil.Logic.Models.Requests;
using Emi.Portal.Movil.Logic.Models.Responses;

namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall
{
    public interface IQueuingViewModel
    {
        Task AllInOrder();
        void LoadRequestQueuing(RequestMedicalService request, RequestMedicalCallOpentok requestMedicalCall);
        void RemovePage();
        string MessageOpentok { get; set; }
        int? OldPosition { get; set; }
        bool OldHasDoctor { get; set; }
        bool ValideDoctor { get; set; }
        void SendMessage();
        ICommand GetOutCommand { get; }
        Task GetOutSesion();

        RequestMedicalService requestMedicalService { get; set; }
        PostPatientServiceTypeResponse patient { get; set; }
        bool Qualify { get; set; }
    }
}
