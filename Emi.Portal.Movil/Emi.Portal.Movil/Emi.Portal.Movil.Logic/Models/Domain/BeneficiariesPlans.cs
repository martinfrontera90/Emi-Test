namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class BeneficiariesPlans
    {
        [JsonProperty(PropertyName = "TipoDocumentoDesc")]
        public string DocumentType { get; set; }

        [JsonProperty(PropertyName = "Documento")]
        public string Document { get; set; }

        [JsonProperty(PropertyName = "Vida")]
        public string Life { get; set; }

        [JsonProperty(PropertyName = "Nombre1")]
        public string Name1 { get; set; }

        [JsonProperty(PropertyName = "Nombre2")]
        public string Name2 { get; set; }

        [JsonProperty(PropertyName = "Apellido1")]
        public string LastName1 { get; set; }

        [JsonProperty(PropertyName = "Apellido2")]
        public string LastName2 { get; set; }

        [JsonProperty(PropertyName = "ProductosContratados")]
        public List<HiredProduct> HiredProducts { get; set; }
    }
}
