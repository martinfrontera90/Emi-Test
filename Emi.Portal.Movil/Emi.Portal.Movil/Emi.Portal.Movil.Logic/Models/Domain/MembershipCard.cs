namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System;
    using Newtonsoft.Json;

    public class MembershipCard
    {
        [JsonProperty("FullNameAffiliate")]
        public string NameAffiliate { get; set; }

        [JsonProperty("DocumentAffiliate")]
        public DocumentCard DocumentAffiliate { get; set; }

        [JsonProperty("BirthDate")]
        public DateTimeOffset BirthDate { get; set; }

        [JsonProperty("AffiliatePlan")]
        public string AffiliatePlan { get; set; }

        [JsonProperty("AffiliateDate")]
        public DateTimeOffset AffiliateDate { get; set; }

        public string DescDocumentType { get; set; }
        public string DescPlan { get; set; }
        public string DescProduct { get; set; }
    }
}
