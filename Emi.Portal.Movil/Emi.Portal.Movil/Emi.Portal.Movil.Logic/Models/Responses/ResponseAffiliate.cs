namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseAffiliate : ResponseBase
    {
        [JsonProperty(PropertyName = "Life")]
        public Person PersonalData { get; set; }
    }
}
