namespace Emi.Portal.Movil.Logic.Models.Domain
{
    using Emi.Portal.Movil.Logic.Enumerations;
    public class ServiceHistory
    {
        public string Address { get; set; }
        public string Code { get; set; }
        public string Cost { get; set; }
        public PendingCoordination Coordination { get; set; }
        public string CityName { get; set; }
        public string Date { get; set; }
        public string DoctorName { get; set; }
        public string FileCode { get; set; }
        public string ServiceTypeDescription { get; set; }
        public string SpecialityName { get; set; }
        public ServiceType ServiceType { get; set; }
        public bool Cancelable { get; set; }
        public string ServiceNumber { get; set; }
        public bool Canceled { get; set; }
        public string CodeState { get; set; }
        public string DescriptionState { get; set; }
        public string IdService { get; set; }
        public string UserName { get; set; }
        public string UserDocument { get; set; }
        public string UserDocumentType { get; set; }
        public string UserDocumentTypeStr { get; set; }
    }
}
