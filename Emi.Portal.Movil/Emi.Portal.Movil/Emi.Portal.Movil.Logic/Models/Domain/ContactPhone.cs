using Emi.Portal.Movil.Logic.Enumerations;

namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class ContactPhone
    {
        public CallType Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public object Description { get; set; }
    }
}
