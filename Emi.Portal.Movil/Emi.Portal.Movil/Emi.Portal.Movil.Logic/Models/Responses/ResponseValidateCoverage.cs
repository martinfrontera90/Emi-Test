namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseValidateCoverage : ResponseBase
    {
        [JsonProperty(PropertyName = "coverage")]
        public Coverage Coverage { get; set; }
        public string NameLocation { get; set; }
    }
}
