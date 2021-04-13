namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestAsociatedUserAccounts : Request
    {
        public string Cellphone { get; set; }
        public string Profile { get; set; }

        public RequestAsociatedUserAccounts()
        {
            Controller = AppConfigurations.AccountController;
            Action = AppConfigurations.GetAsociatedUserAccounts;
            Profile = "customers";
        }
    }
}
