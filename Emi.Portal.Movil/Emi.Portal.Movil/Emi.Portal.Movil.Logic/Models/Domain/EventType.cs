namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class EventType
    {
        [JsonProperty(PropertyName = "IdTipo")]
        public string EventTypesId { get; set; }
        [JsonProperty(PropertyName = "Nombre")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string StateFilter { get; set; }
    }
}
