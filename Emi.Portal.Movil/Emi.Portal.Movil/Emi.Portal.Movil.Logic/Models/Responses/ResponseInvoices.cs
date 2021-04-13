namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;    
    using System.Collections.Generic;

    public class ResponseInvoices : ResponseBase
    {        
        public InvoicesResponse InvoicesResponse { get; set; }
    }
}
