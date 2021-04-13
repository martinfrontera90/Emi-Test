namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RequestInvoices : Request
    {
        public string IdStatusInvoice { get; set; }
        public string InitDate { get; set; }
        public string EndDate { get; set; }
        public RequestInvoices()
        {
            Action = AppConfigurations.GetListInvoices;
            Controller = AppConfigurations.InvoicesController;
        }
    }
}
