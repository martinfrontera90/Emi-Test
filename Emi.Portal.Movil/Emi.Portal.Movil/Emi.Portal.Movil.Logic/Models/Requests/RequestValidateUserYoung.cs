namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;    

    public class RequestValidateUserYoung : Request
    {
        public RequestValidateUserYoung()
        {
            Action = AppConfigurations.ValidateIsYoung;
            Controller = AppConfigurations.AffiliateController;
        }
    }
}
