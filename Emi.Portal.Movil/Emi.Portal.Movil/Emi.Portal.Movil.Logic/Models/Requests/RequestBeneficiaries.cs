namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestBeneficiaries : Request
    {        
        public RequestBeneficiaries()
        {
            Action = AppConfigurations.GetBeneficiaries;
            Controller = AppConfigurations.AffiliateController;
        }
    }
}
