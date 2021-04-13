namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

   public class ResponseDocuments : ResponseBase
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<Document> Documents { get; set; }
    }
}
