namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestDeactivationType : Request
    {
        public RequestDeactivationType()
        {
            Action = AppConfigurations.DeactivationType;
            Controller = AppConfigurations.DataListsController;
        }
    }
}
