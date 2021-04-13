using System;
using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestAuthorizeChangePassword : Request
    {
        public string CellPhone { get; set; }
        public string Profile { get; set; }

        public RequestAuthorizeChangePassword()
        {
            Controller = AppConfigurations.AccountController;
            Action = AppConfigurations.AuthorizeChangePasswordByCellPhone;
            Profile = "customers";
        }
    }
}
