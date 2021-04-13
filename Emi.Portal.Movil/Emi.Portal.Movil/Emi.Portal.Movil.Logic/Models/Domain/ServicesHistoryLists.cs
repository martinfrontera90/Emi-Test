namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using System.Collections.Generic;
    public class ServicesHistoryLists
    {
        public List<ServicesType> ServicesType { get; set; }
        public List<Speciality> Specialities { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<City> Cities { get; set; }
    }
}
