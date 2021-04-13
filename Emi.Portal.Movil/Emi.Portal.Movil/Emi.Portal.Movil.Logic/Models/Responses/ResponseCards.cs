namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseCards : ResponseBase
    {
        [JsonProperty("membershipCards")]
        public List<MembershipCard> MembershipCards { get; set; }
    }
}
