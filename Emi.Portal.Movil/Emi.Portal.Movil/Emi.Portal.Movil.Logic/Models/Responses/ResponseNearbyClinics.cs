namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;
    public class ResponseNearbyClinics : ResponseBase
    {
        [JsonProperty(PropertyName = "MedicalCenterSchedules")]
        public List<Clinic> Clinics { get; set; }
    }
}
