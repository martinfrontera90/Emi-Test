namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    public class ResponseMedicalService : ResponseBase
    {
        public string ServiceNumber { get; set; }
        public ChatAgent ChatAgent { get; set; }        
    }
}
