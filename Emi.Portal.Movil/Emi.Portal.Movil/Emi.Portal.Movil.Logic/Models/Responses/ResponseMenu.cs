namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseMenu : ResponseBase
    {
        [JsonProperty("Menus")]
        public List<ItemMenu> MenuItems { get; set; }
    }
}
