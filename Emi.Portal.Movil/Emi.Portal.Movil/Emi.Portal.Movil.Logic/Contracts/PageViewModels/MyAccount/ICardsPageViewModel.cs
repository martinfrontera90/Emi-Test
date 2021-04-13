namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public interface ICardsPageViewModel
    {
        Task LoadData();
        MembershipCard CardSelected { get; set; }
    }
}
