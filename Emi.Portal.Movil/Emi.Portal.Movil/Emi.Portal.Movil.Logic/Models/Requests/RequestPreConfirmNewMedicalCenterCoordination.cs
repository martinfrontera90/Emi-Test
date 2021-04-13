using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestPreConfirmNewMedicalCenterCoordination : Request
    {       
        public string Token { get; set; }
        public string RDACode { get; set; }
        public string LocalCode { get; set; }
        public string ClinicCode { get; set; }
        public string SpecialityCode { get; set; }
        public string Number { get; set; }
        public string Phone { get; set; }

        public RequestPreConfirmNewMedicalCenterCoordination()
        {
            Action = "PreConfirmCoordination";
            Controller = AppConfigurations.CoordinationsController;
        }
    }
}
