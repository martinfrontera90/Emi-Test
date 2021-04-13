namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Emi.Portal.Movil.Logic.Models.Requests;

    public class MessagesOpentok
    {
        public int type { get; set; }

        public string from { get; set; }

        public string CallHistoryId { get; set; }

        public string ServiceDate { get; set; }

        public string ArchiveId { get; set; }

        public string msg { get; set; }

        public string ServiceNumber { get; set; }

        public int status { get; set; }

        public string UrlFile { get; set; }

        public RequestMedicalService info { get; set; }
    }
}