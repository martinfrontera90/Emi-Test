namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class RequestDownloadCertificate : Request
    {

        public string TypeCertificate { get; set; }
        public bool RequestGroup { get; set; }
        public string GroupCode { get; set; }
        public string FileName { get; set; }
        public int CertificateCode { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<CertificateCities> CodeCity { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string CertifiedYear { get; set; }
        public string NameCountry { get; set; }
        public string FullNameCertified { get; set; }
        public string Mail { get; set; }
    }
}
