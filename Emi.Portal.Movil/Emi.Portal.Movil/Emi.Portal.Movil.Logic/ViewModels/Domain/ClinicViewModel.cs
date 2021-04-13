namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Controls;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight.Command;
    
	using Plugin.Permissions;
	using Plugin.Permissions.Abstractions;
	using Xamarin.Forms;
    using CommonServiceLocator;

	public class ClinicViewModel : LocationViewModel, ILocation, ICallMedicalCenterViewModel
    {
        INavigationService navigationService;
        IPhoneService phoneService;

        public ClinicViewModel()
        {
            navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            phoneService = ServiceLocator.Current.GetInstance<IPhoneService>();
            Icon = AppConfigurations.IconLocation;
        }

        public string Schedule { get; set; }
        public List<string> Services { get; set; }

        public bool HasInteraction { get; set; } = true;
        public string Description { get; set; }
        public string Icon { get; set; }

        string adultTime;
        public string AdultTime
        {
            get { return adultTime; }
            set
            {
                adultTime = value;
                RaisePropertyChanged("AdultTime");
            }
        }

        string pediatricTime;
        public string PediatricTime
        {
            get { return pediatricTime; }
            set
            {
                pediatricTime = value;
                RaisePropertyChanged("PediatricTime");
            }
        }


        double distance;
        public double Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                RaisePropertyChanged("Distance");
            }
        }

        public ICommand SelectCommand { get { return new RelayCommand(Select); } }
        public ICommand GoToWazeCommand { get { return new RelayCommand(GoToWaze); } }

        private void Select()
        {
            if (HasInteraction)
            {
                INearbyClinicsPageViewModel NearbyClinicsPageViewModel = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
                NearbyClinicsPageViewModel.ClinicSelected = this;
                navigationService.Navigate(AppPages.NearbyClinicDetailPage);                     
            }
        }
        private void GoToWaze()
        {
            INearbyClinicsPageViewModel storesPage = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
            storesPage.ClinicSelected = this;

            if (phoneService.IsiOS)
                GoToWazeIOS();
            else
                GoToWazeAndroid();
        }
        private void GoToWazeAndroid()
        {
            try
            {
                string Latitudestr = Latitude.ToString().Replace(phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ".");
                string Longitudestr = Longitude.ToString().Replace(phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ".");
                phoneService.OpenUrl(string.Format(AppConfigurations.WazeNavigationUrl, Latitudestr, Longitudestr));
            }
            catch
            {
                phoneService.OpenUrl(AppConfigurations.GooglePlayWaze);
            }
        }
        private void GoToWazeIOS()
        {
            if (phoneService.CanOpenUrl("waze://"))
            {                
                string Latitudestr = Latitude.ToString().Replace(phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ".");
                string Longitudestr = Longitude.ToString().Replace(phoneService.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ".");
                phoneService.OpenUrl(string.Format(AppConfigurations.WazeNavigationUrl, Latitudestr, Longitudestr));
            }
            else
                phoneService.OpenUrl("http://itunes.apple.com/us/app/id323229106");
        }

		private async Task<bool> CheckPermissions()
        {
            PermissionStatus permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
            IDialogService dialogService = ServiceLocator.Current.GetInstance<IDialogService>();            

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

        private async void CallMedicalCenterLine()
        {
            if (await CheckPermissions())
			{
				phoneService.Call(AppConfigurations.MedicalCenterLine);
			}
		}

		public ICommand CallMedicalCenterCommand { get { return new RelayCommand(CallMedicalCenterLine); } }
    }
}
