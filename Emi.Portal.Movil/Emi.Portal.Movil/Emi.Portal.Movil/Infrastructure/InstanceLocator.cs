namespace Emi.Portal.Movil.Infrastructure
{
    using Autofac;
    using Autofac.Builder;
    using Autofac.Extras.CommonServiceLocator;
    using Emi.Portal.Movil.Logic;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Home;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.LegalContent;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Loader;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Notifications;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.QualifyServices;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Contracts.Views;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Emi.Portal.Movil.Logic.ViewModels.Pages;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.CustomerService;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.Home;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.LegalContent;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.Loader;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.MedicalCenterCoordination;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.MyHealth;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.Notifications;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.QualifyServices;
    using Emi.Portal.Movil.Services;
    using CommonServiceLocator;
    using Xamarin.Forms;
    using Emi.Portal.Movil.Logic.ViewModels.Views;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.VideoCall;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Login;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.Popup;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Controls;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount.Certificates;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.Controls;
    using Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount.Cards;

    public class InstanceLocator
    {
        private LocalizedStrings resources;
        public LocalizedStrings Resources { get { return resources; } }

        private LocalConfigurations configurations;
        public LocalConfigurations Configurations { get { return configurations; } }

        public InstanceLocator()
        {
            RegisterInstances();
        }

        private void RegisterInstances()
        {
            //test
            resources = new LocalizedStrings();
            configurations = new LocalConfigurations();
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<RememberPasswordPageViewModel>().As<IRememberPasswordPageViewModel>().SingleInstance();
            builder.RegisterType<BeneficiariesPageViewModel>().As<IBeneficiariesPageViewModel>().SingleInstance();
            builder.RegisterType<ChangePasswordPageViewModel>().As<IChangePasswordPageViewModel>().SingleInstance();
            builder.RegisterType<ChangeEmailPageViewModel>().As<IChangeEmailPageViewModel>().SingleInstance();
            builder.RegisterType<DisableAccountPageViewModel>().As<IDisableAccountPageViewModel>().SingleInstance();
            builder.RegisterType<MenuPageViewModel>().As<IMenuPageViewModel>().SingleInstance();
            builder.RegisterType<SubMenuItemViewModel>().As<ISubMenuItemViewModel>().SingleInstance();
            builder.RegisterType<PersonalDataPageViewModel>().As<IPersonalDataPageViewModel>().SingleInstance();

            builder.RegisterType<ApiService>().As<IApiService>().SingleInstance();
            builder.RegisterType<CoordinationViewModel>().As<ICoordinationViewModel>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<LandingPageViewModel>().As<ILandingPageViewModel>().SingleInstance();
            builder.RegisterType<LoginPageViewModel>().As<ILoginPageViewModel>().SingleInstance();
            builder.RegisterType<LoginViewModel>().As<ILoginViewModel>().SingleInstance();
            builder.RegisterType<MedicalCenterCoordinationPageViewModel>().As<IMedicalCenterCoordinationPageViewModel>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<NearbyClinicsPageViewModel>().As<INearbyClinicsPageViewModel>().SingleInstance();
            builder.RegisterType<NetworkService>().As<INetworkService>().SingleInstance();
            builder.RegisterType<NewMedicalCenterCoordinationPageViewModel>().As<INewMedicalCenterCoordinationPageViewModel>().SingleInstance();
            builder.RegisterType<RegisterPageViewModel>().As<IRegisterPageViewModel>().SingleInstance();
            builder.RegisterType<CallViewModel>().As<ICallViewModel>().SingleInstance();
            builder.RegisterType<CallMedicalCenterViewModel>().As<ICallMedicalCenterViewModel>();
            builder.RegisterType<PQRSPageViewModel>().As<IPQRSPageViewModel>().SingleInstance();
            builder.RegisterType<GeolocatorService>().As<IGeolocatorService>().SingleInstance();
            builder.RegisterType<PrecallViewModel>().As<IPrecallViewModel>().SingleInstance();
            builder.RegisterType<EvaluateVideoCallViewModel>().As<IEvaluateVideoCallViewModel>().SingleInstance();
            builder.RegisterType<QueuingViewModel>().As<IQueuingViewModel>().SingleInstance();
            builder.RegisterType<SurveyQueuingViewModel>().As<ISurveyQueuingViewModel>().SingleInstance();

            builder.RegisterInstance(DependencyService.Get<IPhoneService>());
            builder.RegisterInstance(DependencyService.Get<INotificationService>());
            builder.RegisterInstance(DependencyService.Get<IFileService>());
            builder.RegisterInstance(DependencyService.Get<ICloseApplication>());
            builder.RegisterInstance(DependencyService.Get<IQueingFirebaseService>());

            builder.RegisterType<AddFamilyPageViewModel>().As<IAddFamilyPageViewModel>().SingleInstance();
            builder.RegisterType<SearchFamilyPageViewModel>().As<ISearchFamilyPageViewModel>().SingleInstance();
            builder.RegisterType<PersonViewModel>().As<IPersonViewModel>().SingleInstance();
            builder.RegisterType<FamilyPageViewModel>().As<IFamilyPageViewModel>().SingleInstance();
            builder.RegisterType<LegalContentPageViewModel>().As<ILegalContentPageViewModel>().SingleInstance();
            builder.RegisterType<FaqsPageViewModel>().As<IFaqsPageViewModel>().SingleInstance();  
            builder.RegisterType<FaqCompleteViewModel>().As<IFaqCompleteViewModel>().SingleInstance();
            builder.RegisterType<ServicesHistoryPageViewModel>().As<IServicesHistoryPageViewModel>().SingleInstance();
            builder.RegisterType<NotificationsPageViewModel>().As<INotificationsPageViewModel>().SingleInstance();
            builder.RegisterType<CertificatesPageViewModel>().As<ICertificatesPageViewModel>().SingleInstance();
            builder.RegisterType<ChatCustomerServicePageViewModel>().As<IChatCustomerServicePageViewModel>().SingleInstance();
            builder.RegisterType<ExceptionService>().As<IExceptionService>().SingleInstance();
            builder.RegisterType<ScheduledServicesPageViewModel>().As<IScheduledServicesPageViewModel>().SingleInstance();
            builder.RegisterType<FileSelectService>().As<IFileSelectService>().SingleInstance();
            builder.RegisterType<QualifyServicesPageViewModel>().As<IQualifyServicesPageViewModel>().SingleInstance();
            builder.RegisterType<LoaderPageViewModel>().As<ILoaderPageViewModel>().SingleInstance();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();
            builder.RegisterType<ServicesPageViewModel>().As<IServicesPageViewModel>().SingleInstance();
            builder.RegisterType<ValidatorService>().As<IValidatorService>().SingleInstance();
            builder.RegisterType<AddressViewModel>().As<IAddressViewModel>().SingleInstance();
            builder.RegisterType<PdfPageViewModel>().As<IPdfPageViewModel>().SingleInstance();
            builder.RegisterType<ProductsAndPlansPageViewModel>().As<IProductsAndPlansPageViewModel>().SingleInstance();
            builder.RegisterType<PermissionService>().As<IPermissionService>().SingleInstance();
            builder.RegisterType<InvoicesPageViewModel>().As<IInvoicesPageViewModel>().SingleInstance();
            builder.RegisterType<InvoiceDetailPageViewModel>().As<IInvoiceDetailPageViewModel>().SingleInstance();
            builder.RegisterType<CurrentServiceViewViewModel>().As<ICurrentServiceViewViewModel>().SingleInstance();
            builder.RegisterType<VideoCallPageViewModel>().As<IMedicalVideoCallViewModel>().SingleInstance();
            builder.RegisterType<EditPasswordPageViewModel>().As<IEditPasswordPageViewModel>().SingleInstance();
            builder.RegisterType<UserInactiveViewModel>().As<IUserInactiveViewModel>().SingleInstance();
            builder.RegisterType<CardsPageViewModel>().As<ICardsPageViewModel>().SingleInstance();
            builder.RegisterType<ExpiredMedicalServicesPageViewModel>().As<IExpiredMedicalServicesPageViewModel>().SingleInstance();
            builder.RegisterType<RegisterMinorPageViewModel>().As<IRegisterMinorPageViewModel>().SingleInstance();
            builder.RegisterType<ContingencyMessagePageViewModel>().As<IContingencyMessagePageViewModel>().SingleInstance();
            var container = builder.Build(ContainerBuildOptions.None);
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
        }
        public IAddFamilyPageViewModel AddFamilyPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IAddFamilyPageViewModel>();
            }
        }

        public IProductsAndPlansPageViewModel ProductsAndPlansPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IProductsAndPlansPageViewModel>();
            }
        }

        public IBeneficiariesPageViewModel BeneficiariesPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IBeneficiariesPageViewModel>();
            }
        }

        public IPdfPageViewModel PdfPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IPdfPageViewModel>();
            }
        }

        public ICertificatesPageViewModel CertificatesPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ICertificatesPageViewModel>();
            }
        }

        public ICallViewModel Call
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ICallViewModel>();
            }
        }

        public IChangePasswordPageViewModel ChangePasswordPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IChangePasswordPageViewModel>();
            }
        }

        public IChangeEmailPageViewModel ChangeEmailPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IChangeEmailPageViewModel>();
            }
        }

        public IDisableAccountPageViewModel DisableAccountPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IDisableAccountPageViewModel>();
            }
        }

        public IPQRSPageViewModel PQRSPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IPQRSPageViewModel>();
            }
        }

        public IEditPasswordPageViewModel EditPasswordPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IEditPasswordPageViewModel>();
            }
        }

        public IFaqsPageViewModel Faqs
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IFaqsPageViewModel>();
            }
        }
        public IFamilyPageViewModel FamilyPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IFamilyPageViewModel>();
            }
        }
        public IMenuPageViewModel MenuPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IMenuPageViewModel>();
            }
        }
        public ILandingPageViewModel LandingPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ILandingPageViewModel>();
            }
        }
        public ILegalContentPageViewModel LegalContent
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ILegalContentPageViewModel>();
            }
        }
        public ILoginPageViewModel LoginPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ILoginPageViewModel>();
            }
        }
        public IMedicalCenterCoordinationPageViewModel MedicalCenterCoordinationPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
            }
        }
        public INearbyClinicsPageViewModel NearbyClinicsPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
            }
        }
        public INewMedicalCenterCoordinationPageViewModel NewMedicalCenterCoordinationPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
            }
        }
        public IPersonalDataPageViewModel PersonalDataPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IPersonalDataPageViewModel>();
            }
        }
        public IRegisterPageViewModel RegisterPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IRegisterPageViewModel>();
            }
        }
        public IRememberPasswordPageViewModel RememberPasswordPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IRememberPasswordPageViewModel>();
            }
        }
        public ISearchFamilyPageViewModel SearchFamilyPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISearchFamilyPageViewModel>();
            }
        }
        public IServicesHistoryPageViewModel ServicesHistoryPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IServicesHistoryPageViewModel>();
            }
        }
        public INotificationsPageViewModel NotificationsPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<INotificationsPageViewModel>();
            }
        }

        public IChatCustomerServicePageViewModel ChatCustomerServicePage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IChatCustomerServicePageViewModel>();
            }
        }

        public IScheduledServicesPageViewModel ScheduledServicesPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IScheduledServicesPageViewModel>();
            }
        }

        public IQualifyServicesPageViewModel QualifyServicesPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IQualifyServicesPageViewModel>();
            }
        }
        public ILoaderPageViewModel LoaderPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ILoaderPageViewModel>();
            }
        }
        public ISettingsService SettingsService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISettingsService>();
            }
        }
        public IServicesPageViewModel ServicesPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IServicesPageViewModel>();
            }
        }
        public ISubMenuItemViewModel SubMenuPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISubMenuItemViewModel>();
            }
        }
        public ICurrentServiceViewViewModel CurrentServiceView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ICurrentServiceViewViewModel>();
            }
        }
        public IInvoicesPageViewModel InvoicesPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IInvoicesPageViewModel>();
            }
        }

        public IInvoiceDetailPageViewModel InvoiceDetailPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IInvoiceDetailPageViewModel>();
            }
        }

        public IMedicalVideoCallViewModel MedicalVideoCall
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IMedicalVideoCallViewModel>();
            }
        }

        public IPrecallViewModel PrecallViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IPrecallViewModel>();
            }
        }

        public IQueuingViewModel QueuingViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IQueuingViewModel>();
            }
        }

        public IEvaluateVideoCallViewModel EvaluateVideoCall
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IEvaluateVideoCallViewModel>();
            }
        }

        public IUserInactiveViewModel UserInactiveViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IUserInactiveViewModel>();
            }
        }

        public ISurveyQueuingViewModel SurveyQueuingViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ISurveyQueuingViewModel>();
            }
        }

        public ICardsPageViewModel CardsPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ICardsPageViewModel>();
            }
        }

        public IExpiredMedicalServicesPageViewModel ExpiredMedicalServices
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IExpiredMedicalServicesPageViewModel>();
            }
        }

        public IRegisterMinorPageViewModel RegisterMinorPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IRegisterMinorPageViewModel>();
            }
        }
        
        public IContingencyMessagePageViewModel ContingencyMessage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IContingencyMessagePageViewModel>();
            }
        }
    }
}
