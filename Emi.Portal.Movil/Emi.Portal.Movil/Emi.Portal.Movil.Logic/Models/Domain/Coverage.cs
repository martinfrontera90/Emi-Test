namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;
    public class Coverage
    {        
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [JsonProperty(PropertyName = "Coverage")]
        public bool InCoverage { get; set; }
    }
}
