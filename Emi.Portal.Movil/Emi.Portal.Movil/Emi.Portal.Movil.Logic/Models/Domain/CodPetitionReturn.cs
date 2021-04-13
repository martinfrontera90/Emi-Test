namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class CodPetitionReturn
    {
        [JsonProperty(PropertyName = "CodPetitionReturn")]
        public string CodePetitionReturn { get; set; }
        [JsonProperty(PropertyName = "Url")]
        public string Url { get; set; }
    }
}
