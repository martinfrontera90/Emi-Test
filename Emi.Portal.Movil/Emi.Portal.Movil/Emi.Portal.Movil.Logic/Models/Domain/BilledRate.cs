namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class BilledRate
    {
        [JsonProperty(PropertyName = "Moneda")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "Importe")]
        public string Amount { get; set; }
    }
}
