using System;
namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class TracingPQR
    {
        public string PersonOccurredEvent { get; set; }
        public int VoucherNumber { get; set; }
        public string ReportDate { get; set; }
        public string EventDate { get; set; }
        public string EventTypeName { get; set; }
        public int EventTypeCode { get; set; }
        public string EventRelatedAreaName { get; set; }
        public int EventRelatedAreaCode { get; set; }
        public string EventStatusName { get; set; }
        public int EventStatusCode { get; set; }
        public string Commentary { get; set; }
    }
}
