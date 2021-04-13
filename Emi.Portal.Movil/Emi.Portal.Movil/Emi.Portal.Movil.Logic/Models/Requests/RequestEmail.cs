namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestEmail : Request
    {
        public string UserName { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
        public string ConfirmEmail { get; set; }

        public RequestEmail()
        {
            Action = AppConfigurations.ChangeEmail;
            Controller = AppConfigurations.AccountController;
            UserName = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.UserName;
        }
    }
}
