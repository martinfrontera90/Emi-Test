namespace Emi.Portal.Movil.Logic.Models.Requests
{
	using Emi.Portal.Movil.Logic.Resources;
    
	public class RequestForgotPassword : Request
    {
        public string User { get; set; }

        public string Code { get; set; }

        public RequestForgotPassword()
        {
            Action = AppConfigurations.ForgotPassword;
            Controller = AppConfigurations.LoginController;
        }
    }
}
