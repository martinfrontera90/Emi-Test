namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Enumerations;
    using Newtonsoft.Json;
    public class ResponseLogin : Response
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Access_token { get; set; }
        public AffiliateType AffiliateType { get; set; }
        [JsonProperty(PropertyName = ".expires")]
        public string Expires { get; set; }
        public ResponseErrorLogin Error { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int Expires_in { get; set; }
        public string IdReference { get; set; }
        [JsonProperty(PropertyName = ".issued")]
        public string Issued { get; set; }
        public string NameOne { get; set; }
        public string NameTwo { get; set; }
        public string LastNameOne { get; set; }
        public string LastNameTwo { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string Token_type { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        public string DocumentType { get; set; }
        public string Document { get; set; }
        public string DocumentTypeName { get; set; }
        public string StatusCode { get; set; }
        public RoleType RoleType { get; set; }
        public string CellPhone { get; set; }
        public string UserMidd { get; set; }
        public string SessionCode { get; set; }
        public string ChangedPassword { get; set; }
    }
}
