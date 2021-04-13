using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestServiceFile : RequestSendServiceFile
    {
        public RequestServiceFile()
        {
            Action = AppConfigurations.GetServiceFile;
            Controller = AppConfigurations.ServicesController;
        }
    }
}
