namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseNeighborhoods : ResponseBase
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<Neighborhood> Neighborhoods { get; set; }
    }
}
