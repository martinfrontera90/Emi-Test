namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    public class ResponsePendingCoordinations : ResponseBase
    {
        public List<PendingCoordination> PendingCoordinations { get; set; }
        public string Recommendations { get; set; }
    }
}
