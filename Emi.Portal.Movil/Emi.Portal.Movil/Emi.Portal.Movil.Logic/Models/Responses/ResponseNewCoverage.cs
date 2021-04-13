namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseNewCoverage : ResponseBase
    {
        [JsonProperty(PropertyName = "Coverage")]
        public NewCoverage Coverage { get; set; }
    }
}
