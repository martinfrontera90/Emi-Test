namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public    class RequestSheduledServices : Request
    {
        public RequestSheduledServices()
        {
            Action = AppConfigurations.GetSheduledServices;
            Controller = AppConfigurations.ServicesController;
        }
    }
}
