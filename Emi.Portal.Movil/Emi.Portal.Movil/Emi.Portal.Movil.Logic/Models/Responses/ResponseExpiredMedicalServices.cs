namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseExpiredMedicalServices : ResponseBase
    {
        public ExpiredProduct ExpiredProducts { get; set; }
    }
}
