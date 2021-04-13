namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class CardPlans
    {
        [JsonProperty(PropertyName = "TarjetaDesc")]
        public string Card { get; set; }
    }
}
