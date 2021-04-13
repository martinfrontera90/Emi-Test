namespace Emi.Portal.Movil.Logic.Models.Requests
{
    using Emi.Portal.Movil.Logic.Resources;

    public class RefundTypeRequest : Request
    {
        public bool Responsable { get; set; }
        public RefundTypeRequest()
        {
            Controller = AppConfigurations.DataListsController;
            Action = AppConfigurations.GetRefundType;
        }
    }
}
