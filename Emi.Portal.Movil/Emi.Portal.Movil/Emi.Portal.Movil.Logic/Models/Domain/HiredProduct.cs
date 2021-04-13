namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System;
    using Newtonsoft.Json;

    public class HiredProduct
    {
        [JsonProperty(PropertyName = "NombrePlan")]
        public string PlanName { get; set; }

        [JsonProperty(PropertyName = "NombreOpcional")]
        public string NameOptional { get; set; }

        [JsonProperty(PropertyName = "TarifaFacturada")]
        public BilledRate BilledRate { get; set; }
    }
}
