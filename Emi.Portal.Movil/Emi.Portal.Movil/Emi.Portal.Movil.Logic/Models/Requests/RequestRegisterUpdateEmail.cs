namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestRegisterUpdateEmail : RequestRegister
    {
        public RequestRegisterUpdateEmail()
        {
            Action = AppConfigurations.VerifyUpdateEmailCode;
            Controller = AppConfigurations.RegistrationController;
        }
    }
}
