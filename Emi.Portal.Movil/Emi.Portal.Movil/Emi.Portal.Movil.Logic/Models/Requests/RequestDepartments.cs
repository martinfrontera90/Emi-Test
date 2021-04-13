namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public   class RequestDepartments : Request
     {
        public RequestDepartments()
        {
            Action = "GetDepartments";
            Controller = AppConfigurations.DataListsController;
        }
    }
}
