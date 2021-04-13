﻿namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseCertificateBeneficiaries : ResponseBase
    {
        public List<BeneficiaryCertificates> Beneficiaries { get; set; }
    }
}
