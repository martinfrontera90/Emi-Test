namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestExistsMedicalHomeService : Request
    {
        public RequestExistsMedicalHomeService()
        {
            Action = AppConfigurations.GetExistsMedicalHomeService;
            Controller = AppConfigurations.ServicesController;
        }
    }
}
