namespace Emi.Portal.Movil.Logic.Contracts.Domain
{
    using Emi.Portal.Movil.Logic.Models.Responses;

    public interface ILoginViewModel
    {
        ResponseLogin User { get; set; }
    }
}
