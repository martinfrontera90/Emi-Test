namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;

    public class MedicalCenter
    {
        public string Address { get; set; }
        public string ClinicCode { get; set; }
        public string ClinicName { get; set; }
        public string Latitude { get; set; }
        public string LocalCode { get; set; }                        
        public string Longitude { get; set; }
        public string RDACode { get; set; }
        public List<string> Services { get; set; }
        public string Schedule { get; set; }
        public object SpecialityCode { get; set; }        
        public List<Schedule> MedicalCenterSchedules { get; set; }
        public string AdultTime { get; set; }
        public string PediatricTime { get; set; }
    }
}
