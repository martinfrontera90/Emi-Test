namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestSpecialities : Request
    {
        public string ServiceType { get; set; }

        public RequestSpecialities()
        {
            Action = AppConfigurations.GetMedicalSpecialites;
            Controller = AppConfigurations.DataListsController;
        }
    }
}
