namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestStandarizedAddressLists : Request
    {
        public RequestStandarizedAddressLists()
        {
            Action = "GetStandarizedAddressLists";
            Controller = AppConfigurations.ServicesController;
        }
    }
}
