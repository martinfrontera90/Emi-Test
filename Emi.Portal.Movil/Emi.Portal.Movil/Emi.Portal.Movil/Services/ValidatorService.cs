namespace Emi.Portal.Movil.Logic.Helpers
{
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Responses;

    public class ValidatorService : IValidatorService
    {
        IDialogService dialogService;
        public async Task<bool> ValidateResponse(ResponseBase response)
        {
            if (response.Success == false || response.StatusCode != 0)
            {
                await dialogService.ShowMessage(response.Title, response.Message);
                return false;
            }

            return true;
        }

        public ValidatorService(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
    }
}
