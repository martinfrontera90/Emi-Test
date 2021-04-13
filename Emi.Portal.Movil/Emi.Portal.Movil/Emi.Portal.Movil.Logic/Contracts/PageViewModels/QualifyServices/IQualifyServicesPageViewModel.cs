namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels.QualifyServices
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    public interface IQualifyServicesPageViewModel
    {
        Task LoadCalificate(string code);
        ICommand SendCalificateCommand { get; }
        string TitlePage { get; set; }
    }
}
