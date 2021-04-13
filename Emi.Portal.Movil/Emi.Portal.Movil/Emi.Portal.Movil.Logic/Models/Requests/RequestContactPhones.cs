namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestContactPhones : Request
    {
        public RequestContactPhones()
        {
            Action = AppConfigurations.GetContactPhones;
            Controller = AppConfigurations.DataListsController;
        }
    }
}
