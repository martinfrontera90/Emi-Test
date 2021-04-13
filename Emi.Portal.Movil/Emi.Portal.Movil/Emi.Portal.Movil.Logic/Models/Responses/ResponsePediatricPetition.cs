namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponsePediatricPetition : ResponseBase
    {
        public PediatricAtention CreateAttentionResult { get; set; }
    }
}
