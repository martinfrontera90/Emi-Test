namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;
    public class Login
    {
        [JsonProperty(PropertyName = "UserName")]
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}
