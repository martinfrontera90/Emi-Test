namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseNewVideocall : ResponseBase
    {
        public PetitionVideoCall Petition { get; set; }
    }
}
