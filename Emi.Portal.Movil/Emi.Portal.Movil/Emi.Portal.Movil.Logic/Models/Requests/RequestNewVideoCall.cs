namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestNewVideoCall : Request
    {
        public string Type { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string NumberStreet { get; set; }
        public string Bis { get; set; }
        public string Apto { get; set; }
        public string Corner { get; set; }
        public string Reference { get; set; }
        public string Cellphone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool CoverageZona { get; set; }

    }
}
