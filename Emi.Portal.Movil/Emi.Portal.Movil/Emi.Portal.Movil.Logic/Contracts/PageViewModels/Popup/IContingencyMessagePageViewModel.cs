namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup
{
    using System.Collections.ObjectModel;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public interface IContingencyMessagePageViewModel
    {
        void LoadData();

        ObservableCollection<ContingencyMessages> Messages { get; set; }
    }
}
