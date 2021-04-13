using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
   public class RequestAddMember : Request
    {                
        public string Names { get; set; }
        public string Phone { get; set; }
        public string Surnames { get; set; }

        public RequestAddMember()
        {
            Action = AppConfigurations.AddMember;
            Controller = AppConfigurations.FamilyController;
        }
    }
}
