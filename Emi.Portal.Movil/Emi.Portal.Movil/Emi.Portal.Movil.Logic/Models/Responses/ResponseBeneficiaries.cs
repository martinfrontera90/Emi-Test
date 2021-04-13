namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    public class ResponseBeneficiaries : ResponseBase
    {
        public List<Person> Beneficiaries { get; set; }
    }
}
