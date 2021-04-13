namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System;
    public class Register
    {
        public DateTime BirthDate { get; set; }
        public string CellPhone { get; set; }
        public string CodeVerification { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string IdDocument { get; set; }
        public string LastNameOne { get; set; }
        public string LastNameTwo { get; set; }
        public string NameOne { get; set; }
        public string NameTwo { get; set; }        
        public string Password { get; set; }
        public bool TermsAndConditions { get; set; }
        public string TypeDocument { get; set; }
        public bool UpdateEmail { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
    }
}
