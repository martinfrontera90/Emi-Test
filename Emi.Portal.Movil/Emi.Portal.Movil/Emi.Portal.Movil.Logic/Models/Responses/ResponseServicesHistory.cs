namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseServicesHistory : ResponseBase
    {
        [JsonProperty(PropertyName = "ServiceHistory")]
        public List<ServiceHistory> ServicesHistory { get; set; }
    }
}
