namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Invoice : ModelBase
    {
        public string InternalSeries { get; set; }
        public string InternalNumber { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string VoucherType { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string BalanceInvoice { get; set; }
        public string State { get; set; }
        public string Finalconsumer { get; set; }
        public string Taxedamount { get; set; }
        public string DescriptionInvoice { get; set; }
        public string BroadcastDate { get; set; }
        public string ExpirationDate { get; set; }
    }
}
