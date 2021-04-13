namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;
    public class ResponseCoverage : ResponseBase
    {
        [JsonProperty(PropertyName = "polygonsResponses")]
        public List<Polygon> Coverages { get; set; }        
    }
}
