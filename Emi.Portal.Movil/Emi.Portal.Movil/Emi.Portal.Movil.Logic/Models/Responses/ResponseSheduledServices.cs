namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    public class ResponseSheduledServices : ResponseBase
    {
        public List<ServiceHistory> ServiceHistory { get; set; }           
    }
}
