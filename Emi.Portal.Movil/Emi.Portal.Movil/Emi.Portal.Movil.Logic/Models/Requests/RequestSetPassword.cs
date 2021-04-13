namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestSetPassword : Request
    {
        public string Code { get; set; }

        public string ConfirmPassword { get; set; }

        public string NewPassword { get; set; }

        public RequestSetPassword()
        {
            Controller = AppConfigurations.LoginController;
            Action = AppConfigurations.SetPassword;
        }
    }
}
