namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseDownloadCertificate : ResponseBase
    {
        public CertificateDownload Download { get; set; }
    }
}
