namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestPayMedicalCenterCoordination : RequestNewMedicalCenterCoordination
    {
        public string PaymentMethodCode { get; set; }
        public string Price { get; set; }
        public string Installments { get; set; }

        public RequestPayMedicalCenterCoordination()
        {
            Action = "Pay";
            Controller = AppConfigurations.CoordinationsController;
        }
    }
}
