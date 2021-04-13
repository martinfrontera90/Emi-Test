namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestRegisterMinor : Request
    {
        public string StringMinorDocument { get; set; }
        public string StringCivilRegistration { get; set; }
        public string MinorDocumentType { get; set; }
        public string MinorDocument { get; set; }
        public string MinorFullName { get; set; }
        public string ResponsibleFullName { get; set; }
        public string ExtentionDocument { get; set; }
        public string ExtentionCivilRegistration { get; set; }
        public string MailResponsible { get; set; }
    }
}
