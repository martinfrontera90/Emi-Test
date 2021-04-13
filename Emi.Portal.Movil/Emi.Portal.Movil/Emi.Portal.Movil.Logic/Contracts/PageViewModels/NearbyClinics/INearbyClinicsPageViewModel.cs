namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Plugin.Geolocator.Abstractions;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface INearbyClinicsPageViewModel
    {
        ObservableCollection<ClinicViewModel> Clinics { get; set; }
        ClinicViewModel ClinicSelected { get; set; }
        Task LoadClinincs();    
        bool IsReady { get; set; }
        Position CurrentLocation {get; set; }
        bool IsEmergency { get; set; }
        string TitlePage { get; set; }
    }
}
