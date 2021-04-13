using System.Collections.Generic;
using Emi.Portal.Movil.Logic.Models.Domain;
using Newtonsoft.Json;

namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponseDocumentRegister : Response
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<Document> Documents { get; set; }
        public int StatusCode { get; set; }        
        public int Type { get; set; }
        public string Title { get; set; }
    }
}
