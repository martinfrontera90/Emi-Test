namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Newtonsoft.Json;

    public class BankPlans
    {
        [JsonProperty(PropertyName = "BancoDesc")]
        public string Bank { get; set; }
    }
}
