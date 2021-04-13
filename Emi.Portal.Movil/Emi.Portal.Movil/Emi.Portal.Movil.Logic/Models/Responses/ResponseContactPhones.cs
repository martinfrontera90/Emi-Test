namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseContactPhones : ResponseBase
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<ContactPhone> ContactPhones { get; set; }
    }
}
