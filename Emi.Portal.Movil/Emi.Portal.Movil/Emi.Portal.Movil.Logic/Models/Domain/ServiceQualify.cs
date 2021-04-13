namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    public class ServiceQualify
    {
        public string Code { get; set; }
        public string ServiceName { get; set; }
        public List<Question> Questions { get; set; }
    }
}
