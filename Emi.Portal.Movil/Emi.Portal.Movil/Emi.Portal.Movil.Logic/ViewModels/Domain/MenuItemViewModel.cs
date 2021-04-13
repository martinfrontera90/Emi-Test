namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class MenuItemViewModel : ViewModelBase, IMenuItemViewModel
    {
        public MenuItemViewModel()
        {
            SubMenuItems = new ObservableCollection<SubMenuItemViewModel>();
        }

        private ObservableCollection<SubMenuItemViewModel> subMenuItems;
        public ObservableCollection<SubMenuItemViewModel> SubMenuItems
        {
            get { return subMenuItems; }
            set
            {
                if (subMenuItems != value)
                {
                    subMenuItems = value;
                    RaisePropertyChanged("SubMenuItems");
                }
            }
        }

        public string Title { get; set; }
        public string Icon { get; set; }
        public AppPages Page { get; set; }
        public ICommand SelectCommand { get { return new RelayCommand(Select); } }
        private async void Select()
        {
            INavigationService navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            ISubMenuItemViewModel subMenuItem = ServiceLocator.Current.GetInstance<ISubMenuItemViewModel>();
            subMenuItem.Title = Title;

            await navigationService.BackToRoot();

            switch (Page)
            {
                case AppPages.LandingPage:
					await navigationService.BackToRoot();
                    break;

                case AppPages.MyAccount:
                case AppPages.MyHealth:
                case AppPages.CustomerService:
                    await navigationService.Navigate(AppPages.SubMenuPage);
                    break;

                case AppPages.NearbyClinicsPage:
                    INearbyClinicsPageViewModel nearbyClinicsPageViewModel = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
                    nearbyClinicsPageViewModel.CurrentLocation = null;
                    nearbyClinicsPageViewModel.IsEmergency = false;
                    nearbyClinicsPageViewModel.TitlePage = Title;
                    IMedicalCenterCoordinationPageViewModel medicalCenter = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
                    medicalCenter.IsVisibleCoordination = true;

                    await navigationService.Navigate(AppPages.NearbyClinicsPage);
                    break;

                case AppPages.LoginPage:
                    await navigationService.Navigate(AppPages.LoginPage);
                    break;

                case AppPages.ServicesPage:
                    IServicesPageViewModel servicesPageViewModel = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();
                    servicesPageViewModel.TitlePage = Title;
                    await servicesPageViewModel.LoadPersons();
                    break;
            }
        }
    }
}
