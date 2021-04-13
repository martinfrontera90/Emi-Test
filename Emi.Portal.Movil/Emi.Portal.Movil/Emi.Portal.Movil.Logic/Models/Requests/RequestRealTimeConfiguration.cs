using System;
using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestRealTimeConfiguration : Request
    {
        public RequestRealTimeConfiguration()
        {
            Controller = AppConfigurations.VideoCallController;
            Action = AppConfigurations.GetFirebaseConfiguration;
        }
    }
}
