namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestCertificates : Request
    {
        public string GroupCode { get; set; }
        public bool LoginUser { get; set; }
        public bool RequestGroup { get; set; }
        public string TypeCertificate { get; set; }
        public string LoggedUserDocument { get; set; }
        public string LoggedUserDocumentType { get; set; }
    }
}
