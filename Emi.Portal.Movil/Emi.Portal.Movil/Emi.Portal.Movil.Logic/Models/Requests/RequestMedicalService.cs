namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestMedicalService : Request
    {
        public string ServiceType { get; set; }
        public string AppliantDocumentType { get; set; }
        public string AppliantDocument { get; set; }
        public string PatientDocumentType { get; set; }
        public string PatientDocument { get; set; }
        public string PatientFullNames { get; set; }
        public string ApplicantCellPhone { get; set; }
        public string ApplicantHomePhone { get; set; }
        public string PatientCellPhone { get; set; }
        public string PatientHomePhone { get; set; }
        public string Country { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string DoorNumber { get; set; }
        public string Bis { get; set; }
        public string Apartment { get; set; }
        public string Corner { get; set; }
        public string AddressDetails { get; set; }
        public string AppliantName { get; set; }
        public string Coverage { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string StreetSO { get; set; }
        public string doctorName { get; set; }
        public int DoctorId { get; set; }
        public string AffiliateUserName { get; set; }
        public string IdSessionTokbox { get; set; }
        public int ReasonsAbandonmentId { get; set; }
        public string ReasonsAbandonmentComment { get; set; }
        public RequestMedicalService()
        {
            Controller = AppConfigurations.ServicesController;
        }
    }
}
