namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    public class CoordinationPaymentMethod
    {
        public bool ExternalMethod { get; set; }
        public string IconApp { get; set; }
        public List<int> Installments { get; set; }
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodDescription { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
