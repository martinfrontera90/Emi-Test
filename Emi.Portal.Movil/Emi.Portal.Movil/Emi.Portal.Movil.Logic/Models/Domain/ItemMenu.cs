namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ItemMenu
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("MenuName")]
        public string MenuName { get; set; }

        [JsonProperty("Parent_MenuID")]
        public string ParentMenuId { get; set; }

        [JsonProperty("IcoWeb")]
        public string IcoWeb { get; set; }

        [JsonProperty("ImageApp")]
        public string ImageApp { get; set; }

        [JsonProperty("MenuUrl")]
        public string MenuUrl { get; set; }

        [JsonProperty("ResourceApp")]
        public string ResourceApp { get; set; }

        public List<ItemMenu> MenuChilds { get; set; }
    }
}
