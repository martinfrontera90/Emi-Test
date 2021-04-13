namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseAllFaqsGroup : ResponseBase
    {
        [JsonProperty(PropertyName = "FaqsResponse")]
        public List<Category> Categories { get; set; }
    }
}
