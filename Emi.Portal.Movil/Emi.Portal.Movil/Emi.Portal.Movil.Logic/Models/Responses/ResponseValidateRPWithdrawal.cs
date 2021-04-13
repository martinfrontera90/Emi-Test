namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseValidateRPWithdrawal : ResponseBase
    {
        public RPWithdrawalAndCard ValidateRPWithdrawalAndCard { get; set; }
    }
}
