namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface ISearchFamilyPageViewModel
    {
        string Document { get; set; }
        Task LoadDocuments();
        ICommand SearchFamilyCommand { get; }
    }
}
