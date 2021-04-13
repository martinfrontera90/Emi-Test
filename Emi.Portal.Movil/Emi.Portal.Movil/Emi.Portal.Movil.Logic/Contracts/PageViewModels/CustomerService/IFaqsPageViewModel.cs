namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    public interface IFaqsPageViewModel
    {
        ObservableCollection<FaqCompleteViewModel> GroupCategoriesSubcategories { get; set; }
        ObservableCollection<FaqCompleteViewModel> FaqsFilter { get; set; }
        Task LoadFaqs();
        ICommand SearchCommand { get; }
        FaqCompleteViewModel SubCategorySelected { get; set; }
        string TitlePage { get; set; }
    }
}
