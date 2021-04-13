namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestClinics : Request
    {
        public RequestClinics()
        {
            Action = AppConfigurations.GetClinics;
            Controller = AppConfigurations.CoordinationsController;
        }
    }
}
