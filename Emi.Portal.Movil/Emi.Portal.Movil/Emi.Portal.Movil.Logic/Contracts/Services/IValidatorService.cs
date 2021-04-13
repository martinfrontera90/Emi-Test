namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Models.Responses;
    public interface IValidatorService
    {
        Task<bool> ValidateResponse(ResponseBase response);
    }
}
