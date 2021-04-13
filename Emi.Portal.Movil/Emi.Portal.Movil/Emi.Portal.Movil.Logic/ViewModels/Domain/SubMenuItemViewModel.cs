namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.LegalContent;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.QualifyServices;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;

    public class SubMenuItemViewModel : ViewModelBase, ISubMenuItemViewModel
    {
        public string Title { get; set; }
        public AppPages Page { get; set; }
        public ObservableCollection<SubMenuItemViewModel> Items { get; set; }

        private SubMenuItemViewModel selectedItem;
        public SubMenuItemViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

        public ICommand SelectCommand { get { return new RelayCommand(Select); } }

        private async void Select()
        {
            INavigationService navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            switch (Page)
            {
                case AppPages.ProductsAndPlans:
                    await navigationService.Navigate(AppPages.ProductsAndPlans);
                    break;

                case AppPages.BeneficiariesPage:
                    IBeneficiariesPageViewModel beneficiariesPageViewModel = ServiceLocator.Current.GetInstance<IBeneficiariesPageViewModel>();
                    beneficiariesPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.BeneficiariesPage);
                    break;

                case AppPages.CardsPage:
                    ICardsPageViewModel cardsPageViewModel = ServiceLocator.Current.GetInstance<ICardsPageViewModel>();
                    await cardsPageViewModel.LoadData();
                    break;

                case AppPages.ChangePasswordPage:
                    IChangePasswordPageViewModel changePasswordPageViewModel = ServiceLocator.Current.GetInstance<IChangePasswordPageViewModel>();
                    changePasswordPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.ChangePasswordPage);
                    break;

                case AppPages.ChangeEmailPage:
                    IChangeEmailPageViewModel changeEmailPageViewModel = ServiceLocator.Current.GetInstance<IChangeEmailPageViewModel>();
                    changeEmailPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.ChangeEmailPage);
                    break;

                case AppPages.CertificatesPage:
                    ICertificatesPageViewModel certificatesPageViewModel = ServiceLocator.Current.GetInstance<ICertificatesPageViewModel>();
                    certificatesPageViewModel.TitlePage = Title;
                    await certificatesPageViewModel.LoadData();
                    break;

                case AppPages.DisableAccountPage:
                    IDisableAccountPageViewModel disableAccountPageView = ServiceLocator.Current.GetInstance<IDisableAccountPageViewModel>();
                    disableAccountPageView.TitlePage = Title;
                    await disableAccountPageView.LoadTypes();
                    await navigationService.Navigate(AppPages.DisableAccountPage);
                    break;

                case AppPages.PQRSPage:
                    IPQRSPageViewModel pQRSPageViewModel = ServiceLocator.Current.GetInstance<IPQRSPageViewModel>();
                    pQRSPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.PQRSPage);
                    break;

                case AppPages.DataManagementPolicyPage:
                    ChangeIconLegalContent();
                    await navigationService.Navigate(AppPages.DataManagementPolicyPage, true);
                    break;

                case AppPages.FaqsPage:
                    IFaqsPageViewModel faqsPageViewModel = ServiceLocator.Current.GetInstance<IFaqsPageViewModel>();
                    faqsPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.FaqsPage);
                    break;

                case AppPages.FamilyPage:
                    IFamilyPageViewModel familyPageViewModel = ServiceLocator.Current.GetInstance<IFamilyPageViewModel>();
                    familyPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.FamilyPage);
                    break;

                case AppPages.LandingPage:
                    await navigationService.Navigate(AppPages.LandingPage);
                    break;

                case AppPages.LoginPage:
                    INotificationService notificationsService = ServiceLocator.Current.GetInstance<INotificationService>();
                    ILoginPageViewModel loginPageViewModel = ServiceLocator.Current.GetInstance<ILoginPageViewModel>();
                    ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
                    INotificationService notificationService = ServiceLocator.Current.GetInstance<INotificationService>();
                    IFileService fileService = ServiceLocator.Current.GetInstance<IFileService>();

                    loginViewModel.User = null;
                    loginPageViewModel.Email = string.Empty;
                    loginPageViewModel.Password = string.Empty;
                    notificationsService.UnregisterNotifications();
                    notificationService.RegisterNotifications();
                    await fileService.SaveAsync(string.Format("{0} User", AppConfigurations.Brand), loginViewModel.User);
                    await navigationService.Navigate(AppPages.LoginPage);
                    break;

                case AppPages.MedicalCenterCoordinationPage:
                    IMedicalCenterCoordinationPageViewModel medicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
                    medicalCenterCoordinationPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.MedicalCenterCoordinationPage);
                    break;

                case AppPages.NearbyClinicsPage:
                    INearbyClinicsPageViewModel nearbyClinics = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
                    nearbyClinics.CurrentLocation = null;
                    nearbyClinics.TitlePage = Title;
                    IMedicalCenterCoordinationPageViewModel medicalCenter = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
                    medicalCenter.IsVisibleCoordination = true;
                    await navigationService.Navigate(AppPages.NearbyClinicsPage);
                    break;

                case AppPages.PersonalDataPage:
                    IPersonalDataPageViewModel personalDataPageViewModel = ServiceLocator.Current.GetInstance<IPersonalDataPageViewModel>();
                    personalDataPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.PersonalDataPage);
                    break;

                case AppPages.SearchServicesHistoryPage:
                    IServicesHistoryPageViewModel servicesHistoryPageViewModel = ServiceLocator.Current.GetInstance<IServicesHistoryPageViewModel>();
                    servicesHistoryPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.SearchServicesHistoryPage);
                    break;

                case AppPages.TermsAndConditionsPage:
                    ChangeIconLegalContent();
                    await navigationService.Navigate(AppPages.TermsAndConditionsPage, true);
                    break;

                case AppPages.ChatCustomerServicePage:
                    await navigationService.Navigate(AppPages.ChatCustomerServicePage);
                    break;

                case AppPages.ScheduledServicesPage:
                    IScheduledServicesPageViewModel scheduledServicesPageViewModel = ServiceLocator.Current.GetInstance<IScheduledServicesPageViewModel>();
                    scheduledServicesPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.ScheduledServicesPage);
                    break;

                case AppPages.QualifyServicesPage:
                    IQualifyServicesPageViewModel calificateServicesPageViewModel = ServiceLocator.Current.GetInstance<IQualifyServicesPageViewModel>();
                    calificateServicesPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.QualifyServicesPage);
                    break;

                case AppPages.ServicesPage:
                    IServicesPageViewModel servicesPageViewModel = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();
                    servicesPageViewModel.TitlePage = Title;
                    await servicesPageViewModel.LoadPersons();
                    break;

                case AppPages.SearchInvoicesPage:
                    IInvoicesPageViewModel invoicesPageViewModel = ServiceLocator.Current.GetInstance<IInvoicesPageViewModel>();
                    invoicesPageViewModel.TitlePage = Title;
                    await navigationService.Navigate(AppPages.SearchInvoicesPage);
                    break;

                case AppPages.ExpiredMedicalServices:
                    IExpiredMedicalServicesPageViewModel expiredMedicalServices = ServiceLocator.Current.GetInstance<IExpiredMedicalServicesPageViewModel>();
                    expiredMedicalServices.TitlePage = Title;
                    expiredMedicalServices.LoadData();
                    break;

                case AppPages.RegisterMinorPage:
                    var registerMinor = ServiceLocator.Current.GetInstance<IRegisterMinorPageViewModel>();
                    registerMinor.TitlePage = Title;
                    registerMinor.LoadData();
                    break;
            }
        }

        private void ChangeIconLegalContent()
        {
            ILegalContentPageViewModel legalContentPageViewModel = ServiceLocator.Current.GetInstance<ILegalContentPageViewModel>();
            legalContentPageViewModel.Icon = "phone";
            legalContentPageViewModel.FromRegister = false;
        }

        public SubMenuItemViewModel()
        {
            Items = new ObservableCollection<SubMenuItemViewModel>();
        }
    }
}
