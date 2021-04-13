namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Newtonsoft.Json;
    public class Response
    {
        [JsonProperty(PropertyName = "Exito")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "Mensaje")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string ErrorMessage { get; set; }

        public string Tittle { get; set; }        
    }
}
