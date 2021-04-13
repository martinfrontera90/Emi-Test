

namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    public class ResponsePediatricAgendas : ResponseBase
    {
        public List<PediatricAgendas> Agendas { get; set; }
    }
}
