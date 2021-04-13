namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestCancelService : Request
    {
        public string Code { get; set; }
        public string ServiceType { get; set; }
        public PendingCoordination PendingCoordination { get; set; }  
        public Applicant applicant { get; set; }

    public RequestCancelService()
        {
            Action = AppConfigurations.CancelService;
            Controller = AppConfigurations.ServicesController;
        }
    }
}
