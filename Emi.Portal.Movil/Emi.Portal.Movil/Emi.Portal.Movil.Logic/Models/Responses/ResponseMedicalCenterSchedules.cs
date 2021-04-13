using System.Collections.Generic;
using Emi.Portal.Movil.Logic.Models.Domain;
using Newtonsoft.Json;

namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponseMedicalCenterSchedules : ResponseBase
    {
        [JsonProperty(PropertyName = "MedicalCenterSchedules")]
        public List<MedicalCenter> MedicalCenters { get; set; }
    }
}
