namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestNewMedicalCenterCoordination : RequestPreConfirmNewMedicalCenterCoordination
    {
        public string PatientName { get; set; }
        public string PatientCode { get; set; }
        public string ProductCode { get; set; }
        public string Time { get; set; }
        public string YearMonthDay { get; set; }
        public string PaymentMethodName { get; set; }
        public string UserEmail { get; set; }

        public RequestNewMedicalCenterCoordination()
        {
            Action = AppConfigurations.ConfirmCoordination;
            Controller = AppConfigurations.CoordinationsController;
            Token = "12345";
        }
    }
}
