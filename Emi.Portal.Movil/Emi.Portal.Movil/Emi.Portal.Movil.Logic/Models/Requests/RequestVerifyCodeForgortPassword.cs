namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestVerifyCodeForgortPassword : Request
    {
        public string User { get; set; }

        public string Code { get; set; }

        public RequestVerifyCodeForgortPassword()
        {
            Controller = AppConfigurations.LoginController;
            Action = AppConfigurations.VerifyCodeForgortPassword;
        }
    }
}
