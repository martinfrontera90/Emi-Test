namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseCertificates : ResponseBase
    {
        public List<Certificate> Certificates { get; set; }
        public List<GroupCertificate> Groups { get; set; }
        public bool PeaceSafe { get; set; }

    }
}
