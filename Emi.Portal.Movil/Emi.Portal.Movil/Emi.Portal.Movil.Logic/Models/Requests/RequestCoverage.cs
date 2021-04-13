namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestCoverage : Request
    {
        public string Code { get; set; }
        public RequestCoverage()
        {
            Action = "Polygons";
            Controller = "Coverage";
        }
    }
}
