namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestServiceQualify : Request
    {
        public string User { get; set; }
        public string Code { get; set; }

        public RequestServiceQualify()
        {
            Action = "GetServiceQualify";
            Controller = AppConfigurations.ServicesController;
        }
    }
}
