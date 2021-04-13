namespace Emi.Portal.Movil.Logic.Models.Responses
{
    using System.Collections.ObjectModel;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class ResponseAsociatedUserAccounts : ResponseBase
    {
        public Collection<AsociatedUserAccounts> AsociatedUserAccounts { get; set; }
    }
}
