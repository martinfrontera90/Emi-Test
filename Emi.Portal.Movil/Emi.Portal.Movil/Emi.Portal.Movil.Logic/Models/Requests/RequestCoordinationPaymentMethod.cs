namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestCoordinationPaymentMethod : RequestNewMedicalCenterCoordination
    {
        public string Price { get; set; }

        public RequestCoordinationPaymentMethod()
        {
            Action = "GetPaymentMethods";
            Controller = AppConfigurations.CoordinationsController;
        }
    }
}
