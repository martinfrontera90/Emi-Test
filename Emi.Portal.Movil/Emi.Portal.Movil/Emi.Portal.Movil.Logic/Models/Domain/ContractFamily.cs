namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ContractFamily
    {
        [JsonProperty(PropertyName = "TipoConsulta")]
        public string ConsultType { get; set; }

        [JsonProperty(PropertyName = "MatriculaFamilia")]
        public string FamilyEnrollment { get; set; }

        [JsonProperty(PropertyName = "VidaRP")]
        public string LifeRp { get; set; }

        [JsonProperty(PropertyName = "Nombre1")]
        public string Name1 { get; set; }

        [JsonProperty(PropertyName = "Nombre2")]
        public string Name2 { get; set; }

        [JsonProperty(PropertyName = "Apellido1")]
        public string LastName1 { get; set; }

        [JsonProperty(PropertyName = "Apellido2")]
        public string LastName2 { get; set; }

        [JsonProperty(PropertyName = "TipoFacturacion")]
        public string Bill { get; set; }

        [JsonProperty(PropertyName = "PeriodicidadDesc")]
        public string Periodicity { get; set; }

        [JsonProperty(PropertyName = "Banco")]
        public BankPlans Bank { get; set; }

        [JsonProperty(PropertyName = "Tarjeta")]
        public CardPlans Card { get; set; }

        [JsonProperty(PropertyName = "DireccionCobro")]
        public PaymentAddressPlans PaymentAddress { get; set; }

        [JsonProperty(PropertyName = "Facturacion")]
        public BillingPlans Billing { get; set; }

        [JsonProperty(PropertyName = "Beneficiarios")]
        public List<BeneficiariesPlans> Beneficiaries { get; set; }

    }
}
