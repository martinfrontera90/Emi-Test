namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    public class ResponseCoordinationPaymentMethod : ResponseBase
    {
        public List<CoordinationPaymentMethod> CoordinationPaymentMethods { get; set; }        
    }
}
