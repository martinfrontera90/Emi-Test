namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestDeactivateUserAccount : Request
    {
        public string UserAccount { get; set; }

        public DeactivationSelected SelectedOption { get; set; }

        public RequestDeactivateUserAccount()
        {
            Controller = AppConfigurations.AccountController;
            Action = AppConfigurations.DeactivateUserAccount;
            UserAccount = ServiceLocator.Current.GetInstance<ILoginViewModel>().User?.UserName;
        }
    }
}
