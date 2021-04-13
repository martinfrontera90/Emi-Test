namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using CommonServiceLocator;
    using Resources;

    public class RequestUpdateMember : RequestAddMember
    {
        public RequestUpdateMember()
        {
            Action = AppConfigurations.UpdateMember;
            Controller = AppConfigurations.FamilyController;
            IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference;
        }
    }
}
