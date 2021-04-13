namespace Emi.Portal.Movil.Logic.Models.Responses
{   
    public class ResponseForgotPassword : ResponseBase
    {
        public bool PhoneRequest { get; set; }

        public ResponseError Error { get; set; }        
    }
}
