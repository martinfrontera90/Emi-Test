namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestMedicalCallOpentok : Request
    {
        public int type { get; set; }
        public string from { get; set; }
        public RequestMedicalService info { get; set; }

    }
}
