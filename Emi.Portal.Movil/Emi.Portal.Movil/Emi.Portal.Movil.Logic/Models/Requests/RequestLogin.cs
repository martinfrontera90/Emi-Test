namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestLogin : Request
    {
        public Login Login { get; set; }

        public RequestLogin()
        {
            Action = AppConfigurations.Login;
            Controller = AppConfigurations.LoginController;
        }
    }
}
