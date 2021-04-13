namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestAreas : Request
    {
        public RequestAreas()
        {
            Controller = AppConfigurations.DataListsController;
            Action = AppConfigurations.GetEventAreas;
        }
    }
}
