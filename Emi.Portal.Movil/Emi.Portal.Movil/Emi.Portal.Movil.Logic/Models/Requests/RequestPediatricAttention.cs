
namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class RequestPediatricAttention : Request
    {
        public string Phone { get; set; }
        public string Classification { get; set; }
        public PediatricAddressPetition AddressPetition { get; set; }
        public string IdSchedule { get; set; }
        public string ProgramedDate { get; set; }
    }
}
