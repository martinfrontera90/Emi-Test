namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestVersion : Request
    {
        public RequestVersion()
        {
            Controller = AppConfigurations.AccountController;
            Action = AppConfigurations.QueryVersionApp;
        }
    }
}
