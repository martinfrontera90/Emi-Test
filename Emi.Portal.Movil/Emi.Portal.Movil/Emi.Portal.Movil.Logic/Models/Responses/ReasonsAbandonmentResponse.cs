using System;
using System.Collections.Generic;
using Emi.Portal.Movil.Logic.Models.Domain;

namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ReasonsAbandonmentResponse : ResponseBase
    {
        public List<ReasonsAbandonment> DataList { get; set; }
    }
}
