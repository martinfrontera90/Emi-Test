namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseContingencyMessages : ResponseBase
    {
        public List<ContingencyMessages> Messages { get; set; }
    }
}
