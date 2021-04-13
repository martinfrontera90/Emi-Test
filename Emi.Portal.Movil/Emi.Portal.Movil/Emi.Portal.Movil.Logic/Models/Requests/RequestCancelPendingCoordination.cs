namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Resources;

    public class RequestCancelPendingCoordination : Request
    {
        public PendingCoordination PendingCoordination { get; set; }

        public RequestCancelPendingCoordination()
        {
            Action = AppConfigurations.CancelCoordination;
            Controller = AppConfigurations.CoordinationsController;
        }
    }
}
