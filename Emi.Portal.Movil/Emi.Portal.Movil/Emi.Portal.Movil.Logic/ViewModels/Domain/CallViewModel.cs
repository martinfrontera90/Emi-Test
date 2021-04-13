namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;
    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using Xamarin.Forms;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using System.Linq;

    public class CallViewModel : ICallViewModel
    {
        IPhoneService phoneService;
        IDialogService dialogService;
        INavigationService navigationService;
        IApiService apiService;

        public List<ContactPhone> ContactPhones { get; set; }
        public List<ContactPhone> CustomerServices { get; set; }
        public List<ContactPhone> MedicalCare { get; set; }

        public ICommand CallCommand { get { return new RelayCommand(Call); } }
        public ICommand CallMedicalCenterCommand { get { return new RelayCommand(CallMedicalCenter); } }
        public ICommand CallCategoryCommand { get { return new RelayCommand(CallCategory); } }
        public ICommand OpenChatCommand { get { return new RelayCommand(OpenChat); } }

        private async void OpenChat()
        {
            await navigationService.Navigate(AppPages.ChatCustomerServicePage);
        }


        private async void Call()
        {
            if (await CheckPermissions())
            {
                phoneService.Call(AppConfigurations.MedicalCenterLine);
            }
        }

        private async void CallMedicalCenter()
        {
            if (await CheckPermissions())
            {
                phoneService.Call(AppConfigurations.MedicalCenterLine);
            }
        }

        public async void CallCategory()
        {
            string option = await dialogService.CallCategory();
            switch (option)
            {
                case "Servicio al cliente":
                    if (await CheckPermissions())
                    {
                        ContactPhone(AppResources.TitleCustomerServices, CustomerServices);
                    }
                    break;
                case "Atención médica":
                    if (await CheckPermissions())
                    {
                        ContactPhone(AppResources.TitleMedicalCare, MedicalCare);
                    }
                    break;
                default:
                    break;
            }
        }

        public async void ContactPhone(string title, List<ContactPhone> ContactsPhone)
        {
            string[] ContactPhoneArray = ContactsPhone.Select(x => x.Code).ToArray();
            string contactPhone = await dialogService.CallContactPhone(title, ContactPhoneArray);
            if (contactPhone != AppResources.CancelText)
            {
                if (await CheckPermissions())
                {
                    phoneService.Call(ContactsPhone.Where(x => x.Code == contactPhone).First().Code.Replace(" ", ""));
                }
            }
        }

        public void FilterCallLines()
        {
            CustomerServices = ContactPhones.Where(x => x.Category == CallType.CustomerServices).ToList();
            MedicalCare = ContactPhones.Where(x => x.Category == CallType.MedicalCare).ToList();
        }

        public async Task Init()
        {
            RequestContactPhones requestContactPhones = new RequestContactPhones();
            ResponseContactPhones responseContactPhones = await apiService.GetContactPhones(requestContactPhones);

            ContactPhones = responseContactPhones.ContactPhones;
            FilterCallLines();
        }

       

        private async Task<bool> CheckPermissions()
        {
            PermissionStatus permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
            IDialogService dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            IPhoneService phoneService = ServiceLocator.Current.GetInstance<IPhoneService>();

            permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
            bool request = false;

            string title = "Llamadas";
            string question = "Esta funcionalidad requiere permisos de llamada.";

            if (permissionStatus == PermissionStatus.Denied)
            {
                if (phoneService.IsiOS)
                {

                    Task<bool> task = Application.Current?.MainPage?.DisplayAlert(title, question, AppResources.YesButtonText, AppResources.NoButtonText);

                    if (task == null)
                        return false;

                    bool result = await task;

                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return false;
                }
                request = true;
            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                Dictionary<Permission, PermissionStatus> newStatus = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
                if (newStatus.ContainsKey(Permission.Phone) && newStatus[Permission.Phone] != PermissionStatus.Granted)
                {
                    Application.Current?.MainPage?.DisplayAlert(title, question, "Aceptar");
                    CrossPermissions.Current.OpenAppSettings();
                    return false;
                }
            }
            return true;
        }

        public CallViewModel(INavigationService navigationService, IDialogService dialogService, IPhoneService phoneService, IApiService apiService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.phoneService = phoneService;
        }
    }
}
