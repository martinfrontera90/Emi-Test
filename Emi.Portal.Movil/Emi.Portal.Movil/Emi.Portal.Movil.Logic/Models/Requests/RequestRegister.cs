namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestRegister : Request
    {
        public Register Register { get; set; }

        public RequestRegister()
        {
            Action = AppConfigurations.Register;
            Controller = AppConfigurations.RegistrationController;
        }
    }
}
