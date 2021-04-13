namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public   class RequestPreRegister : RequestRegister 
    {
        public RequestPreRegister()
        {
            Action = AppConfigurations.PreRegister;
            Controller = AppConfigurations.RegistrationController;
        }
    }
}
