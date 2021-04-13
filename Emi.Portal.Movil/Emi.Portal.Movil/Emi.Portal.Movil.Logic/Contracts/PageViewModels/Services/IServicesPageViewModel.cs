namespace Emi.Portal.Movil.Logic
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;


    public interface IServicesPageViewModel
    {
        ICommand CheckCoverageCommand { get; }
        ICommand InformationCommand { get; }
        Task LoadPersons();
        //List<Polygon> Coverages { get; set; }
        PersonViewModel PersonSelected { get; set; }
        ObservableCollection<Address> Addresses { get; set; }
        string ApplicantCellPhone { get; set; }
        string PatientCellPhone { get; set; }
        Address AddressSelected { get; set; }
        ICommand ConfirmateServiceCommand { get; }
        ServicesEnabledViewModel ServiceSelected { get; set; }
        ObservableCollection<Country> Countries { get; set; }
        Country CountrySelected { get; set; }
        bool IsVisibleMoreOptions { get; set; }
        bool IsVisibleAddNewAddress { get; set; }
        ICommand CancelChatCommand { get; }
        ICommand ExitChatCommand { get; }
        bool ReloadData { get; set; }
        ICommand AditionalDataContinueCommand { get; }
        bool IsToggledPhoneNumber { get; set; }
        string AditionlPhone { get; set; }
        string AditionalEmail { get; set; }
        string ErrorAditionlPhone { get; set; }
        string ErrorAditionalEmail { get; set; }
        ICommand CallCategoryCommand { get; }
        string Message { get; set; }
        LocationViewModel CurrentLocation { get; set; }
        //LocationViewModel AddressLocation { get; set; }
        ICommand ShopOnlineCommand { get; }
        ICommand CancelNewAddressCommand { get; }
        bool IsShowingUser { get; set; }
        UserYoung UserYoung { get; set; }
        string ServiceSelectedName { get; set; }
        ICommand AdvanceLocationCommand { get; }
        bool ReadyLocation { get; set; }
        bool NewAddressAdded { get; set; }
        string MessageOpentok { get; set; }
        string TitlePage { get; set; }
        string DynamicTitlePage { get; set; }
        void LoadAgendas();

    }
}
