namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestQualifyQuestion : Request
    {
        public List<ServiceQualification> ServiceQualification { get; set; }
        public RequestQualifyQuestion()
        {
            Action = "QualifyQuestion";
            Controller = AppConfigurations.ServicesController;
        }
    }
}
