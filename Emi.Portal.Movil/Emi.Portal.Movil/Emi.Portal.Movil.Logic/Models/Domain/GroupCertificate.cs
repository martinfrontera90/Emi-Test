namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;

    public class GroupCertificate
    {
        public string GroupName { get; set; }
        public string GroupCode { get; set; }
        public List<string> User { get; set; }
    }
}
