namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Resources;
    using CommonServiceLocator;

    public class RequestUpdateAffiliate : Request
    {
        public string CellPhoneNumber { get; set; }
        public string Channel { get; set; }
        
        public RequestUpdateAffiliate()
        {
            Action = AppConfigurations.UpdateAffiliate;
            Channel = AppConfigurations.ChannelKey;
            Controller = AppConfigurations.AffiliateController;
            Document = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.Document;
            DocumentType = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.DocumentType;
        }
    }
}
