namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestServiceTypes : Request
    {
        public RequestServiceTypes()
        {
            Action = AppConfigurations.GetServiceTypes;
            Controller = AppConfigurations.DataListsController;
        }
    }
}
