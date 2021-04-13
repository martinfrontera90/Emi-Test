namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Polygon
    {
        public string CoverageCode { get; set; }
        [JsonProperty(PropertyName = "points")]
        public List<Point> Points { get; set; }
        public Polygon()
        {
            Points = new List<Point>();
        }
    }
}
