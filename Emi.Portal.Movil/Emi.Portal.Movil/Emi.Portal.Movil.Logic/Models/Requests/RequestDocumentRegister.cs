
namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestDocumentRegister : Request
    {
        public RequestDocumentRegister()
        {
            Action = AppConfigurations.GetDocumentTypesRegister;
            Controller = AppConfigurations.DataListsController;
        }
    }
}
