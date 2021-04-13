namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestPendingCoordinations : Request
    {
        public RequestPendingCoordinations()
        {
            Action = AppConfigurations.PendingCoordinations;
            Controller = AppConfigurations.CoordinationsController;
        }
    }
}
