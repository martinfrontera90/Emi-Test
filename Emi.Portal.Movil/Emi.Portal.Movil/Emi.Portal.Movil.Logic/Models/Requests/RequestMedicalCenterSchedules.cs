namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestMedicalCenterSchedules : Request
    {
        public string SpecialityCode { get; set; }

        public RequestMedicalCenterSchedules()
        {
            Controller = AppConfigurations.CoordinationsController;
            Action = AppConfigurations.MedicalCenterSchedules;
        }
    }
}
