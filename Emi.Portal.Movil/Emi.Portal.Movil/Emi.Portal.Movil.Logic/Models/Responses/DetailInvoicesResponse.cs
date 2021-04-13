namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DetailInvoicesResponse
    {
        public string InternalSerie { get; set; }
        public string InternalNumberMyProperty { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string TypeVoucher { get; set; }
        public string Currency { get; set; }
        public string Importe { get; set; }
        public string InvoiceBalance { get; set; }
        public string State { get; set; }
        public string Finalconsumer { get; set; }
        public string TaxedAmount { get; set; }
        public string DescriptionInvoice { get; set; }
        public string GeneratedDate { get; set; }
        public string ExpirationDate { get; set; }
        public string Paymentmode { get; set; }
        public string AddressCharge { get; set; }
        public string BankCardPayment { get; set; }
        public string CompanyPayment { get; set; }
        [JsonProperty(PropertyName = "Details")]
        public List<BeneficiaryAndProduct> BeneficiaryAndProduct { get; set; }        
    }
}
