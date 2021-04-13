namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System;
    using Newtonsoft.Json;

    public class PetitionVideoCall
    {
        [JsonProperty(PropertyName = "Cod")]
        public string Code { get; set; }
        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "Return")]
        public CodPetitionReturn Return { get; set; }
    }
}
