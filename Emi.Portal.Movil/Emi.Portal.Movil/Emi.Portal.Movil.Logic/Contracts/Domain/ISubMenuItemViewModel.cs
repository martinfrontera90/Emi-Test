namespace Emi.Portal.Movil.Logic.Contracts.Domain
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    public interface ISubMenuItemViewModel
    {
        string Title { get; set; }
        ObservableCollection<SubMenuItemViewModel> Items { get; set; }
        ICommand SelectCommand { get; }
    }
}
