using Emi.Portal.Movil.Logic.Contracts.Domain;
using Emi.Portal.Movil.Logic.Resources;
using CommonServiceLocator;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestFamilyMembers : Request
    {
        public RequestFamilyMembers()
        {
            Action = AppConfigurations.GetMembers;
            Controller = AppConfigurations.FamilyController;
            IdReference = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.IdReference;
        }
    }
}
