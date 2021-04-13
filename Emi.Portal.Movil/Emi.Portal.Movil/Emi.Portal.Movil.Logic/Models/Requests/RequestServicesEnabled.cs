namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestServicesEnabled : Request
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Code { get; set; }

        public RequestServicesEnabled()
        {
            Action = "GetEnabledServices";
            Code = AppConfigurations.CoverageCode;
            Controller = AppConfigurations.ServicesController;
        }
    }
}
