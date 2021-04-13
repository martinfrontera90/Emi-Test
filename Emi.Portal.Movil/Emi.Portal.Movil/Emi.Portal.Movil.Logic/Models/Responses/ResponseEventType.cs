namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseEventType : ResponseBase
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<EventType> EventTypes { get; set; }
    }
}
