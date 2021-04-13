namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public    class RequestLegalContent : Request
    {
        public string Tag { get; set; }
        public RequestLegalContent()
        {
            Action = AppConfigurations.GetLegalContent;
            Controller = AppConfigurations.ContentsController;
        }
    }
}
