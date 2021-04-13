namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;
    public class ResponseFaqs : ResponseBase
    {
        [JsonProperty(PropertyName = "FaqsResponse")]
        public List<FaqComplete> Faqs { get; set; }
    }
}
