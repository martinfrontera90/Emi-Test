using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestDocument : Request
    {
        public RequestDocument()
        {
            Action = AppConfigurations.GetDocumentTypes;
            Controller = AppConfigurations.DataListsController;
        }
    }
}
