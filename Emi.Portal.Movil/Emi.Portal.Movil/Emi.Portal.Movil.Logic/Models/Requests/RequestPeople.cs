namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestPeople : Request
    {
        public RequestPeople()
        {
            Action = "GetPersons";
            Controller = AppConfigurations.AffiliateController;
        }
    }
}
