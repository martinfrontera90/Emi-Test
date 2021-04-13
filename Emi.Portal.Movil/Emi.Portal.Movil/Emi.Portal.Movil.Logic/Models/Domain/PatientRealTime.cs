using System;
namespace Emi.Portal.Movil.Logic.Models.Domain
{
    public class PatientRealTime
    {
        public int? Position { get; set; }

        public string Room { get; set; }

        public int? Doctor { get; set; }

        public string OnLineFrom { get; set; }
    }
}
