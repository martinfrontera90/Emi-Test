namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseServicesEnabled : ResponseBase
    {
        public List<EnabledService> EnabledServices { get; set; }
    }
}
