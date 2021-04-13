namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseStandarizedAddressLists : ResponseBase
    {
        public List<Via> Via { get; set; }
        public List<Quadrant> Quadrant { get; set; }

    }
}
