namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestAllFaqsGroup : Request
    {
        public RequestAllFaqsGroup()
        {
            Action = "GetAllFaqsGroup";
            Controller = AppConfigurations.ContentsController;
        }
    }
}
