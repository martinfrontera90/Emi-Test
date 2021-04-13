namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System;
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Newtonsoft.Json;

    public class ResponseCountries : ResponseBase
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<ModelBase> Countries { get; set; }
    }
}
