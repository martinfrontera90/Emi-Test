namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseFamilyMembers : ResponseBase
    {        
        public List<Person> Members{ get; set; }
    }
}
