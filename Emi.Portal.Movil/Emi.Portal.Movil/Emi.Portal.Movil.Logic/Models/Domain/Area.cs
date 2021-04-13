namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class Area
    {
        [JsonProperty(PropertyName = "Nombre")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "IdArea")]
        public string Code { get; set; }
        public string Mail { get; set; }
    }
}
