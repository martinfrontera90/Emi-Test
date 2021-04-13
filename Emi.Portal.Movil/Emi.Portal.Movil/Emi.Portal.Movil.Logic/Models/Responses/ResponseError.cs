namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Newtonsoft.Json;
    public class ResponseError
    {
        [JsonProperty(PropertyName = "Message")]
        public string Description { get; set; }

    }
}
