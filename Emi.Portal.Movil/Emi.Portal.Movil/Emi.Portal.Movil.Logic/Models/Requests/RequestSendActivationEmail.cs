using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestSendActivationEmail : Request
    {
        public string User { get; set; }
        public RequestSendActivationEmail()
        {
            Action = AppConfigurations.SendActivationEmail;
            Controller = AppConfigurations.RegistrationController;
        }
    }
}
