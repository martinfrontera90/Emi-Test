namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    public interface IPersonalDataPageViewModel
    {        
        ICommand CancelUpdateCommand { get; }        
        ICommand InformationCommand { get; }
        ICommand UpdateCommand { get; }
        Task LoadPersonalData();
        string TitlePage { get; set; }
    }
}
