namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class PaymentAddressPlans
    {
        [JsonProperty(PropertyName = "EstadoDesc")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "CiudadDesc")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "NombreCalle")]
        public string NameStreet { get; set; }

        [JsonProperty(PropertyName = "NumeroPuerta")]
        public string DoorNumber { get; set; }

        public string Bis { get; set; }

        [JsonProperty(PropertyName = "Apartamento")]
        public string Apartment { get; set; }

        [JsonProperty(PropertyName = "NombreEsquina")]
        public string NameCorner { get; set; }

        [JsonProperty(PropertyName = "NombreEsquina2")]
        public string NameCorner2 { get; set; }

        [JsonProperty(PropertyName = "Ubicacion")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "BarrioDesc")]
        public string Neighborhood { get; set; }
    }
}
