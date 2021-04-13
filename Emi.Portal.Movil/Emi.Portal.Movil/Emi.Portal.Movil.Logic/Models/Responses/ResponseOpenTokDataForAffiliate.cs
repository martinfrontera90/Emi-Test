namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponseOpenTokDataForAffiliate : ResponseBase
    {
        public string OpenTokToken { get; set; }

        public int ApiKey { get; set; }
        public string IdSessionTokbox { get; set; }
        public int DoctorId { get; set; }
    }
}