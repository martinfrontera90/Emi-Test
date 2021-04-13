using System;
using System.Collections.Generic;
using Emi.Portal.Movil.Logic.Models.Domain;

namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponseTracingPQRS : ResponseBase
    {
        public List<TracingPQR> TracingPqrs { get; set; }
    }
}
