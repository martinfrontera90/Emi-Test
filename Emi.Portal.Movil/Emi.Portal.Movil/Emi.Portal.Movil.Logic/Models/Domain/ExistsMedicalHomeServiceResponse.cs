using Emi.Portal.Movil.Logic.Models.Responses;

namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class ExistsMedicalHomeServiceResponse : ResponseBase
    {
        public bool CurrentService { get; set; }
        public string DescriptionState { get; set; }
        public string ServiceTypeDescription { get; set; }
        public string UserName { get; set; }
    }
}
