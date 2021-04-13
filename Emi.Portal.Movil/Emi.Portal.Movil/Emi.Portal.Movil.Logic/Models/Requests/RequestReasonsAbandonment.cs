using System;
using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestReasonsAbandonment : Request
    {
        public RequestReasonsAbandonment()
        {
            Controller = AppConfigurations.DataListsController;
            Action = AppConfigurations.GetReasonsAbandonment;
        }
    }
}
