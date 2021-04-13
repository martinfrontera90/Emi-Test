namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestMinorAuthorizations : Request
    {
        public RequestMinorAuthorizations()
        {
            Action = "GetMinorAuthorizations";
            Controller = "Family";
        }
    }
}
