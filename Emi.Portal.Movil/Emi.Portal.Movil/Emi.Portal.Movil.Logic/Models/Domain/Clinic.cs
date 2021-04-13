namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Clinic
    {
        [JsonProperty(PropertyName = "ClinicName")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Address")]
        public string Addres { get; set; }

        [JsonProperty(PropertyName = "Latitude")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public string Longitude { get; set; }
        
        public string Schedule { get; set; }
        
        public List<string> Services { get; set; }

        [JsonProperty(PropertyName = "ClinicCode")]
        public string Code { get; set; }

        public double Distance { get; set; }
        public string AdultTime { get; set; }
        public string PediatricTime { get; set; }
    }
}
