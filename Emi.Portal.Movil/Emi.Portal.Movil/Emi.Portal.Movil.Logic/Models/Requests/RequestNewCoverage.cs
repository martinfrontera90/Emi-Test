namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestNewCoverage : Request
    {
        public string Address { get; set; }
        public string Municipality { get; set; }
        public bool List { get; set; }
    }
}