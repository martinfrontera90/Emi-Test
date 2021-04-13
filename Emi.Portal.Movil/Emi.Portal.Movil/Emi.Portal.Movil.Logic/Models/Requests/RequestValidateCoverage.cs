namespace Emi.Portal.Movil.Logic.Models.Requests
{    
    public class RequestValidateCoverage : Request
    {
        public string Code { get; set; }
        public string Address { get; set; }

        public RequestValidateCoverage()
        {
            Action = "ValidateCoverage";
            Controller = "Coverage";
        }
    }
}
