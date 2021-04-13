namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class Address
    {
        public bool Coverage { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string DepartmentCode { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public string Neighborhood { get; set; }
        public string NeighborhoodCode { get; set; }
        public string Street { get; set; }
        public string DoorNumber { get; set; }
        public string Bis { get; set; }
        public string Apartment { get; set; }
        public string Corner { get; set; }
        public string AddressDetails { get; set; }
        public string StandardizedAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FullLocation { get; set; }
        public StandardizedMessage StandardizedMessage { get; set; }
        public string Direction { get; set; }
        public string StreetSO { get; set; }
        public bool IsNewAdress { get; set; }
        public string NumberApto { get; set; }

        public Address()
        {
            Latitude = "0.0";
            Longitude = "0.0";
        }
    }
}
