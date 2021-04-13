namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestLocationInformationService : Request
    {
        public string AppliantDocumentType { get; set; }
        public string AppliantDocument { get; set; }
        public string PatientDocumentType { get; set; }
        public string PatientDocument { get; set; }
        public string Code { get; set; }

        public RequestLocationInformationService()
        {
            Action = AppConfigurations.GetLocationInformationService;
            Controller = AppConfigurations.ServicesController;
            Code = AppConfigurations.CoverageCode;
        }
    }
}
