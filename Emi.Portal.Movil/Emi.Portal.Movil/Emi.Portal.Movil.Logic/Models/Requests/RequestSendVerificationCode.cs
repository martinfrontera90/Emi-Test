namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestSendVerificationCode : RequestRegister
    {
        public RequestSendVerificationCode()
        {
            Action = AppConfigurations.SendVerificationCode;
            Controller = AppConfigurations.RegistrationController;
        }
    }
}
