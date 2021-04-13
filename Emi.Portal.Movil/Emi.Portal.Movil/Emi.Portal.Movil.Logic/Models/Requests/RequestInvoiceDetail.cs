namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestInvoiceDetail : Request
    {
        public string InternalSerie { get; set; }
        public string InternalNumber { get; set; }                               

        public RequestInvoiceDetail()
        {
            Action = AppConfigurations.GetDetailInvoice;
            Controller = AppConfigurations.InvoicesController;
        }
    }
}
