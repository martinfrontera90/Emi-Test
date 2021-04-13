namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Resources;
    public class RequestSearchMember : Request
    {        
        public string Number { get; set; }

        public RequestSearchMember()
        {
            Action = AppConfigurations.SearchMember;
            Controller = AppConfigurations.FamilyController;
        }
    }
}
