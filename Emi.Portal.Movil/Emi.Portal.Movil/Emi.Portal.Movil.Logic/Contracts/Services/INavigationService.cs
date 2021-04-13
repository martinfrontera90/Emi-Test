namespace Emi.Portal.Movil.Logic.Contracts.Services
{
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Enumerations;

    public interface INavigationService
    {
        Task Back(bool IsMasterDetail = true);
        Task BackModal();
        Task BackToRoot(bool IsMasterDetail = true);
        void CloseMenu();
        Task ClosedModal();
        Task Navigate(AppPages page, bool IsMainPage = false, string code = null);
        Task ShowModal(AppPages page);
        void RemovePage(int count);
    }
}
