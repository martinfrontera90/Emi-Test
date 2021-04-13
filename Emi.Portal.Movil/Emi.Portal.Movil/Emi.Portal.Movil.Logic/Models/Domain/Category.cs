namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Category
    {
        public string CategoriesFaqsId { get; set; }

        [JsonProperty(PropertyName = "CategoryName")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "SubcategoriesFaqsResponse")]
        public List<Subcategory> Subcategories { get; set; }
    }
}
