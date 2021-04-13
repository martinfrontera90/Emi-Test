namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestAllFaqs : Request
    {
        public RequestAllFaqs()
        {
            Action = AppConfigurations.GetAllFaqs;
            Controller = AppConfigurations.ContentsController;
        }
    }
}
