namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseServiceTypes : ResponseBase
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<Service> Services { get; set; }
    }
}
