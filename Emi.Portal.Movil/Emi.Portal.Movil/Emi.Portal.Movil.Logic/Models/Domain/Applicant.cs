using System;
namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class Applicant
    {
        public string ApplicantNameOne { get; set; }
        public string ApplicantNameTwo { get; set; }
        public string ApplicantLastNameOne { get; set; }
        public string ApplicantLastNameTwo { get; set; }
        public int ApplicantDocumentType { get; set; }
        public string ApplicantDocumentTypeName { get; set; }
        public string ApplicantDocument { get; set; }
        public string ApplicantCellPhone { get; set; }
        public string ApplicantMail { get; set; }
    }
}
