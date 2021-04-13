using System;
using Emi.Portal.Movil.Logic.Models.Domain;
using Newtonsoft.Json;

namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponsePQRSValidateUser : ResponseBase
    {
        [JsonProperty(PropertyName = "pqrs")]
        public PQRSUser ResponseUser { get; set; }

    }
}
