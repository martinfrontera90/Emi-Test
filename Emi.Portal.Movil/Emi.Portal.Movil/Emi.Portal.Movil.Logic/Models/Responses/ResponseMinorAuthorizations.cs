namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseMinorAuthorizations : ResponseBase
    {
        [JsonProperty(PropertyName = "AuthorizedMinors")]
        public List<Minor> Minors { get; set; }
    }
}
