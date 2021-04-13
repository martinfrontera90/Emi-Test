using Emi.Portal.Movil.Logic.Enumerations;

namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class EnabledService
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconWeb { get; set; }
        public string IconApp { get; set; }
        public bool NeedLocation { get; set; }
        public Message Message { get; set; }
        public ServiceType ServiceType { get; set; }
        public bool NeedCoverage { get; set; }
        public string EstimatedTime { get; set; }
    }
}
