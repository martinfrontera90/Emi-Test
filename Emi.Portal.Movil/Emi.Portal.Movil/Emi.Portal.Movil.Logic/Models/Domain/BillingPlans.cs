namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class BillingPlans
    {
        [JsonProperty(PropertyName = "DescripcionFactura")]
        public string InvoiceDescription { get; set; }

        [JsonProperty(PropertyName = "Moneda")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "Importe")]
        public string Amount { get; set; }
    }
}
