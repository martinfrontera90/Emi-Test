using Emi.Portal.Movil.Logic.ViewModels.Domain;

namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponseLegalContent : ResponseBase
    {
        public LegalContent LegalContent { get; set; }
        public int Version { get; set; }
    }
}
