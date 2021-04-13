namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestCancelMedicalHomeService : Request
    {
        public string IdService { get; set; }

        public RequestCancelMedicalHomeService()
        {
            Action = AppConfigurations.GetCancelMedicalHomeService;
            Controller = AppConfigurations.ServicesController;
        }
    }
}
