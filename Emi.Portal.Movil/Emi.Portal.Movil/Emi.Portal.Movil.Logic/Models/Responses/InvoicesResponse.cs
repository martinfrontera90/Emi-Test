namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;
    using System.Collections.Generic;   

    public class InvoicesResponse
    {
        [JsonProperty(PropertyName = "ListInvoices")]
        public List<Invoice> Invoices { get; set; }
        public string DocumentType { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
    }
}
