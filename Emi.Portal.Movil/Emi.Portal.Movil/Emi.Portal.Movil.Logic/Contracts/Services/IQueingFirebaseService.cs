using System;
namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using Emi.Portal.Movil.Logic.Models.Responses;

    public interface IQueingFirebaseService
    {
        void InitializeApp(RealTimeConfigurationResponse realTime);
        object ExistsPatient { get; set; }
        void Connect();
        void LoadSesionWaitingRoom(string patientServiceType, string patientDocument);
        void GetOutSesionWaitingRoom(string patientServiceType, string patientDocument);
        void GetDoctors(string patientServiceType);
        string GetMessageKey();
        string GetDoctorKey();
    }
}
