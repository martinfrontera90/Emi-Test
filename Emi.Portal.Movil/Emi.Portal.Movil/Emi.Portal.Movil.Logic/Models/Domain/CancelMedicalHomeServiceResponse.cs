using Emi.Portal.Movil.Logic.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class CancelMedicalHomeServiceResponse : ResponseBase
    {
        public bool StatusRequest { get; set; }
        public string DescriptionState { get; set; }
        public string Message { get; set; }        
    }
}
