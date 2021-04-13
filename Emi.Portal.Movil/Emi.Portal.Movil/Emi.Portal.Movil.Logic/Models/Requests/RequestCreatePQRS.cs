namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using System;
    using System.Collections.Generic;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class RequestCreatePQRS : Request
    {
        public string EventType { get; set; }
        public string SubjectOfTheEvent { get; set; }
        public string ApplicantDocumentType { get; set; }
        public string ApplicantDocument { get; set; }
        public string ThirdDocumentType { get; set; }
        public string ThirdDocument { get; set; }
        public bool BankAccountUser { get; set; }
        public string RelatedArea { get; set; }
        public string NamesOfficial { get; set; }
        public string EventDate { get; set; }
        public string EventComment { get; set; }
        public bool AcceptTermsAndConditions { get; set; }
        public string ReasonsForReimbursement { get; set; }
        public List<FileSelected> SendFileThanksAndCongratulations { get; set; }
        public List<FileSelected> SendFilesComplaintsAndClaims { get; set; }
        public List<FileSelected> SendBeneficiaryDeathCertificate { get; set; }
        public List<FileSelected> SendFileBankAccount { get; set; }
        public List<FileSelected> SendFileTitularDeathCertificate { get; set; }
        public List<FileSelected> SendFilesCivilRegistrationMarriage { get; set; }
        public List<FileSelected> SendFileBirthCertificateSon { get; set; }
        public List<FileSelected> SendFileSpouseDeathCertificate { get; set; }
        public List<FileSelected> SendFileExtraJudgmentStatement { get; set; }
        public List<FileSelected> SendFileBankAccountCertificate { get; set; }
        public List<FileSelected> SendFileBankStatementOrRemovablePayroll { get; set; }
        public string EventCodeDepartment { get; set; }
        public string EventCodeCity { get; set; }
    }
}
