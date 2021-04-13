namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;
    public class RequestNeighborhoods : Request
    {
        public string CityCode { get; set; }
        public RequestNeighborhoods()
        {
            Action = "GetNeighborhoods";
            Controller = AppConfigurations.DataListsController;
        }
    }
}
