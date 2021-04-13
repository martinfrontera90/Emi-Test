
namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Newtonsoft.Json;
    public class ResponseErrorLogin
    {
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string Description { get; set; }

        public string Type { get; set; }
    }
}
