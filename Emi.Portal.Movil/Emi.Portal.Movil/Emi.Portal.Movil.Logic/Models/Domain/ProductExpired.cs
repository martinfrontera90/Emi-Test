using System;
namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class ProductExpired
    {
        public string DocumentType { get; set; }
        public string DescDocument { get; set; }
        public string Document { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public string DescStatus { get; set; }
        public string CodProduct { get; set; }
        public string DescProduct { get; set; }
        public string DateAccomplished { get; set; }
        public string DateExpired { get; set; }
        public string Coordinate { get; set; }
        public string DescGroupProduct { get; set; }
    }
}
