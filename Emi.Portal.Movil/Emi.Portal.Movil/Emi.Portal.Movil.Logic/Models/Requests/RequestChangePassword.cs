namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Resources;
    using CommonServiceLocator;

    public class RequestChangePassword : Request
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string UserName { get; set; }

        public RequestChangePassword()
        {
            Action = AppConfigurations.ChangePassword;
            Controller = AppConfigurations.RegistrationController;
        }
    }
}
