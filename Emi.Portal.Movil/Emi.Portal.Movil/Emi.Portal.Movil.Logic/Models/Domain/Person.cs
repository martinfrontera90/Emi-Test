namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Emi.Portal.Movil.Logic.Enumerations;
    public class Person
    {
        public bool Active { get; set; }
        public AffiliateType AffiliateType { get; set; }
        public string Beneficiary { get; set; }
        public string Document { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeShort { get; set; }
        public string Email { get; set; }
        public string FullNames { get; set; }
        public string IdReference { get; set; }
        public string Names { get; set; }
        public string Phone { get; set; }
        public string Surnames { get; set; }
        public string CellPhone { get; set; }
        public string CellPhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public double DataCoveragePercentage { get; set; }
    }
}
