using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestSendServiceFile : Request
    {
        public string User { get; set; }
        public string Code { get; set; }

        public RequestSendServiceFile()
        {
            Action = AppConfigurations.SendServiceFile;
            Controller = AppConfigurations.ServicesController;
        }
    }
}