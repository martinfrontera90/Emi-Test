using System;
namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class PQRSUser
    {
        public int Country { get; set; }
        public string FullName { get; set; }
        public int DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string Document { get; set; }
        public bool ActiveUser { get; set; }
        public bool ActiveUserResponsible { get; set; }
        public bool BankAccountUser { get; set; }
        public string State { get; set; }
        public string CodeState { get; set; }
        public string City { get; set; }
        public string CodeCity { get; set; }
        public int CodeMessage { get; set; }
    }
}
