namespace Emi.Portal.Movil.Logic.Contracts.PageViewModels
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;

    public interface INewMedicalCenterCoordinationPageViewModel
    {
        void ClearData();
        void PreConfirmNewCoordination();
        Task LoadData();
        void LoadMedicalCenters();
        ICommand SchedulesCommand { get; }
        ScheduleViewModel ScheduleSelected { get; set; }
        void ConfirmNewCoordination();
        void GetPaymentMethods();
        CoordinationPaymentMethodViewModel PaymentMethodSelected { get; set; }
        void Payment();
        ICommand ClosePaymentPageCommand { get; }
        PersonViewModel PersonSelected { get; set; }
        ICommand InformationCommand { get; }        
        Task LoadPersons();
    }
}
