namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    public class Subcategory
    {
        public string SubcategoriesFaqsId { get; set; }
        public string Name { get; set; }
        public List<Faq> Faqs { get; set; }
    }
}
