namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System;
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ReponseYoungMembers : ResponseBase
    {
        public List<YoungMember> Members { get; set; }
    }
}
