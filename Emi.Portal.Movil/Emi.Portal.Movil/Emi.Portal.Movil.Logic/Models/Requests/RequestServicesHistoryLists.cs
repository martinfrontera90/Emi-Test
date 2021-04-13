using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestServicesHistoryLists : Request
    {
        public RequestServicesHistoryLists()
        {
            Action = AppConfigurations.GetServicesHistoryLists;
            Controller = AppConfigurations.ServicesController;
        }
    }
}
