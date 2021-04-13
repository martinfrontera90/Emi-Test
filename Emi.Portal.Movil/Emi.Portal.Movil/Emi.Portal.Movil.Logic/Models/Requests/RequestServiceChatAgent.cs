namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestServiceChatAgent : Request
    {
        public string Value { get; set; }

        public RequestServiceChatAgent()
        {
            Action = "ServiceChatAgent";
            Controller = AppConfigurations.ServicesController;
            Value = "8";
        }
    }
}
