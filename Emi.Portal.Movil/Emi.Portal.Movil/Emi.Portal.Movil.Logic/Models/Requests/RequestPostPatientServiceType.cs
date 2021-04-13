using System;
using Emi.Portal.Movil.Logic.Resources;

namespace Emi.Portal.Movil.Logic.Models.Requests
{
    public class RequestPostPatientServiceType : Request
    {
        public RequestPostPatientServiceType()
        {
            Controller = AppConfigurations.VideoCallController;
            Action = AppConfigurations.PostPatientServiceType;
        }
    }
}
