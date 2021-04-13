using System.Collections.Generic;
using Newtonsoft.Json;

namespace Emi.Portal.Movil.Logic.Models.Responses
{
    public class ResponseGenders : ResponseBase
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<Gender> Genders { get; set; }
    }
}
