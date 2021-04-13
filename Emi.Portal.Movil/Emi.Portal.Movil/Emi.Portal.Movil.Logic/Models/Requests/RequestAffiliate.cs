namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Resources;
    using CommonServiceLocator;

    public class RequestAffiliate : Request
    {
        public RequestAffiliate()
        {
            Controller = AppConfigurations.AffiliateController;
            Document = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.Document;
            DocumentType = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.DocumentType;
            Action = AppConfigurations.GetAffiliate;
        }
    }
}
