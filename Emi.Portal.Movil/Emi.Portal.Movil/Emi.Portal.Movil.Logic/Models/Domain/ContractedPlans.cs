namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ContractedPlans
    {
        [JsonProperty(PropertyName = "Familias")]
        public List<ContractFamily> Family { get; set; }

        [JsonProperty(PropertyName = "Exito")]
        public int Exit { get; set; }

        [JsonProperty(PropertyName = "Mensaje")]
        public object Message { get; set; }
    }
}
