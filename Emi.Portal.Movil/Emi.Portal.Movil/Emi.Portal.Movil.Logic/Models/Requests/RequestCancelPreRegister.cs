namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestCancelPreRegister : Request
    {
        public string PhoneNumber { get; set; }

        public RequestCancelPreRegister()
        {
            Action = AppConfigurations.CancelPreRegister;
            Controller = AppConfigurations.RegistrationController;
        }
    }
}
