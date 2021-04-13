namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseSpecialities : ResponseBase
    {        
        public List<Speciality> MedicalSpecialites { get; set; }
    }
}
