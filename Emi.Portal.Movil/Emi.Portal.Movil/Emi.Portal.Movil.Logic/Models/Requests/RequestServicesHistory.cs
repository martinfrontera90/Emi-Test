using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestServicesHistory : Request
    {
        public string ServiceType { get; set; }
        public string Speciality { get; set; }
        public string DoctorName { get; set; }
        public string InitDate { get; set; }
        public string EndDate { get; set; }
        public string City { get; set; }

        public RequestServicesHistory()
        {
            Action = AppConfigurations.GetServicesHistory;
            Controller = AppConfigurations.ServicesController;
        }
    }
}
