namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;    
    using System.Collections.Generic;    

    public class ResponseStatusInvoices : ResponseBase
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<StatusInvoice> StatusInvoices { get; set; }
    }
}
