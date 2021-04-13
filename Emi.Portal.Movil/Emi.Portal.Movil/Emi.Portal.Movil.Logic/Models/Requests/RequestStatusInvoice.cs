namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;    

    public class RequestStatusInvoice : Request
    {
        public RequestStatusInvoice()
        {
            Action = AppConfigurations.GetListStatusInvoice;
            Controller = AppConfigurations.InvoicesController;
        }
    }
}
