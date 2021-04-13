namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Resources;
    using CommonServiceLocator;

    public class RequestRemoveMember : Request
    {
        public RequestRemoveMember()
        {
            Controller = AppConfigurations.FamilyController;
            Action = AppConfigurations.RemoveMember;
            IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference;
        }
    }
}
