namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;    

    public class ResponseValidateUserYoung : ResponseBase
    {
        //[JsonProperty(PropertyName = " ")]
        //public UserYoung UserYoung { get; set; }
        public bool resultUserYoung { get; set; }
    }
}
