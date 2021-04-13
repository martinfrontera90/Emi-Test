namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System;
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseDeactivationType : ResponseBase
    {
        public List<DeactivationType> DataList { get; set; }
    }
}
