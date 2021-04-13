namespace Emi.Portal.Movil.Services
{
    using System;
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.LegalContent;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.QualifyServices;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Contracts.Views;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Pages.MyAccount.Certificates;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Pages.CalificateServices;
    using Emi.Portal.Movil.Pages.CustomerService;
    using Emi.Portal.Movil.Pages.Home;
    using Emi.Portal.Movil.Pages.LegalContent;
    using Emi.Portal.Movil.Pages.Login;
    using Emi.Portal.Movil.Pages.Controls;
    using Emi.Portal.Movil.Pages.MedicalCenterCoordination;
    using Emi.Portal.Movil.Pages.MyAccount;
    using Emi.Portal.Movil.Pages.MyHealth;
    using Emi.Portal.Movil.Pages.NotificationsCenter;
    using Emi.Portal.Movil.Pages.Register;
    using Emi.Portal.Movil.Pages.Services;
    using CommonServiceLocator;
    using Pages;
    using Xamarin.Forms;
    using Emi.Portal.Movil.Pages.MyAccount.Cards;
    using Rg.Plugins.Popup.Services;
    using Emi.Portal.Movil.Pages.Popup;
    using Emi.Portal.Movil.Logic.Contracts.Domain;

    public class NavigationService : INavigationService
    {
        INearbyClinicsPageViewModel nearbyClinics;
        IMedicalCenterCoordinationPageViewModel medicalCenterCoordinationPageViewModel;        
        ICurrentServiceViewViewModel currentService;
        INewMedicalCenterCoordinationPageViewModel newMedicalCenterCoordinationPageViewModel;

        public async Task Back(bool IsMasterDetail = true)
        {
            if (IsMasterDetail)
            {
                await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        public async Task BackModal()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async Task BackToRoot(bool IsMasterDetail = true)
        {
            if (IsMasterDetail)
            {
                await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PopToRootAsync();
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        public async Task ClosedModal()
        {
            if (Application.Current.MainPage.Navigation.ModalStack.Count > 0)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        public async Task Navigate(AppPages page, bool IsMainPage = false, string code = null)
        {
            await ValidateCurrentService(page);

            switch (page)
            {
                #region MyAccountPage
                case AppPages.AddFamilyPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new AddFamilyPage());
                    IAddFamilyPageViewModel addFamilyPageViewModel = ServiceLocator.Current.GetInstance<IAddFamilyPageViewModel>();
                    addFamilyPageViewModel.ShowResult();
                    break;
                case AppPages.BeneficiariesPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new BeneficiariesPage());
                    break;

                case AppPages.ProductsAndPlans:
                    IProductsAndPlansPageViewModel productsAndPlans = ServiceLocator.Current.GetInstance<IProductsAndPlansPageViewModel>();
                    productsAndPlans.LoadData();
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ProductsAndPlansPage());
                    break;

                case AppPages.CardsPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new CardsPage());
                    break;
                case AppPages.CardDetailPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new CardDetailPage());
                    break;
                case AppPages.PQRSPage:
                    IPQRSPageViewModel pQRSPageViewModel = ServiceLocator.Current.GetInstance<IPQRSPageViewModel>();
                    pQRSPageViewModel.LoadData();
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new PQRSPage());
                    break;
                case AppPages.ChangePasswordPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ChangePasswordPage());
                    break;
                case AppPages.TripCertificatePage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new TripCertificatePage());
                    break;
                case AppPages.PdfPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new PdfPage());
                    break;
                case AppPages.CertificatesPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new CertificatesPage());
                    break;
                case AppPages.ChangeEmailPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ChangeEmailPage());
                    break;
                case AppPages.DisableAccountPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new DisableAccountPage());
                    break;
                case AppPages.EditFamilyPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new EditFamilyPage());
                    break;
                case AppPages.FamilyPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new FamilyPage());
                    IFamilyPageViewModel Family = ServiceLocator.Current.GetInstance<IFamilyPageViewModel>();
                    await Family.LoadFamily();
                    break;
                case AppPages.PersonalDataPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new PersonalDataPage());
                    IPersonalDataPageViewModel personalDataPage = ServiceLocator.Current.GetInstance<IPersonalDataPageViewModel>();
                    await personalDataPage.LoadPersonalData();
                    break;
                case AppPages.SearchFamilyPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SearchFamilyPage());
                    ISearchFamilyPageViewModel search = ServiceLocator.Current.GetInstance<ISearchFamilyPageViewModel>();
                    await search.LoadDocuments();
                    break;
                case AppPages.NewPQRSPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewPQRSPage());
                    break;
                case AppPages.SearchInvoicesPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SearchInvoicesPage());
                    IInvoicesPageViewModel invoicesPageViewModel = ServiceLocator.Current.GetInstance<IInvoicesPageViewModel>();
                    await invoicesPageViewModel.LoadData();
                    break;
                case AppPages.InvoicesPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new InvoicesPage());
                    break;
                case AppPages.InvoiceDetailPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new InvoiceDetailPage());
                    IInvoiceDetailPageViewModel invoiceDetail = ServiceLocator.Current.GetInstance<IInvoiceDetailPageViewModel>();
                    await invoiceDetail.GetInvoiceDetail();
                    break;
                #endregion

                case AppPages.LandingPage:
                    (Application.Current.MainPage as MasterDetailPage).Detail = new MainPage((Page)Activator.CreateInstance(typeof(LandingPage)));
                    break;
                #region LoginPage
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

                    await fileService.SaveAsync(string.Format("{0} User", AppConfigurations.Brand), loginViewModel.User);
                    Application.Current.MainPage = new LoginPage();

                    break;
                #endregion

                #region RegisterPage
                case AppPages.DataManagementPolicyPage:
                    ILegalContentPageViewModel legalContentPP = ServiceLocator.Current.GetInstance<ILegalContentPageViewModel>();
                    await legalContentPP.LoadContentLegal(AppConfigurations.TagPPC);
                    if (IsMainPage)
                    {
                        await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new LegalContentPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new LegalContentPage());
                    }
                    break;
                case AppPages.RegisterDataPersonalPage:
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisterDataPersonalPage());
                    break;
                case AppPages.RegisterDocumentPage:

                    IRegisterPageViewModel Register = ServiceLocator.Current.GetInstance<IRegisterPageViewModel>();
                    Register.Clean();
                    await Register.LoadPage();
                    await Register.LoadDocuments();

                    if (Register != null && Register.Documents != null && Register.Documents.Count > 0)
                    {
                        Application.Current.MainPage = new LoginNavigationPage(new RegisterDocumentPage());
                    }
                    break;
                case AppPages.RegisterNamePage:
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisterNamePage());
                    break;
                case AppPages.RegisterPasswordPage:
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisterPasswordPage());
                    break;
                case AppPages.RegisterUpdateCellPhonePage:
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisterUpdateCellPhonePage());
                    break;
                case AppPages.RegisterVerificationCodePage:
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisterVerificationCodePage());
                    break;
                case AppPages.TermsAndConditionsPage:
                    ILegalContentPageViewModel legalContentTYC = ServiceLocator.Current.GetInstance<ILegalContentPageViewModel>();
                    await legalContentTYC.LoadContentLegal(AppConfigurations.TagTYCC);
                    if (IsMainPage)
                    {
                        await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new LegalContentPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new LegalContentPage());
                    }
                    break;
                #endregion

                #region RememberPasswordPage
                case AppPages.RememberPasswordPage:
                    await Application.Current.MainPage.Navigation.PushAsync(new RememberPasswordPage());
                    break;
                #endregion

                #region MenuPage
                case AppPages.MenuPage:
                    Application.Current.MainPage = new MenuPage();
                    break;
                #endregion

                #region MedicalCenterCoordinationPage
                case AppPages.MedicalCenterCoordinationPage:                    
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new MedicalCenterCoordinationPage());
                    medicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();                    
                    await medicalCenterCoordinationPageViewModel.LoadCoordinations();                    
                    break;
                case AppPages.MedicalCenterCoordinationDetailPage:
                    medicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new MedicalCenterCoordinationDetailPage());
                    await Task.Delay(1000);
                    medicalCenterCoordinationPageViewModel.IsReady = true;
                    await Task.Delay(1000);
                    break;
                case AppPages.NewMedicalCenterCoordinationPage:
                    newMedicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
                    newMedicalCenterCoordinationPageViewModel.ClearData();                    
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NewMedicalCenterCoordinationPage());
                    await newMedicalCenterCoordinationPageViewModel.LoadData();
                    await newMedicalCenterCoordinationPageViewModel.LoadPersons();                                                               
                    break;
                case AppPages.SchedulesMedicalCenterCoordinationPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SchedulesMedicalCenterCoordinationPage());
                    break;
                case AppPages.CoordinationPaymentMethodPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new CoordinationPaymentMethodPage());
                    break;
                #endregion

                #region NearbyClinicsPage
                case AppPages.NearbyClinicsPage:
                    nearbyClinics = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
                    await nearbyClinics.LoadClinincs();
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NearbyClinicsPage());
                    await Task.Delay(1000);
                    nearbyClinics.IsReady = true;
                    await Task.Delay(1000);
                    break;

                case AppPages.NearbyClinicDetailPage:
                    nearbyClinics = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NearbyClinicDetailPage());
                    await Task.Delay(1000);
                    nearbyClinics.IsReady = true;
                    await Task.Delay(1000);
                    break;
                #endregion

                case AppPages.FaqsPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new FaqsPage());
                    IFaqsPageViewModel faqsPageViewModel = ServiceLocator.Current.GetInstance<IFaqsPageViewModel>();
                    await faqsPageViewModel.LoadFaqs();
                    break;

                case AppPages.FaqsDetailPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new FaqsDetailPage());
                    break;

                case AppPages.SearchServicesHistoryPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SearchServicesHistoryPage());
                    IServicesHistoryPageViewModel servicesHistoryPageViewModel = ServiceLocator.Current.GetInstance<IServicesHistoryPageViewModel>();
                    await servicesHistoryPageViewModel.LoadData();
                    break;

                case AppPages.ServicesHistoryPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ServicesHistoryPage());
                    break;

                case AppPages.ChatCustomerServicePage:
                    IChatCustomerServicePageViewModel chatCustomerServicePageViewModel = ServiceLocator.Current.GetInstance<IChatCustomerServicePageViewModel>();
                    await chatCustomerServicePageViewModel.LoadChatPage();
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ChatCustomerServicePage());
                    break;

                case AppPages.ScheduledServicesPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ScheduledServicesPage());
                    IScheduledServicesPageViewModel scheduledServicesPageViewModel = ServiceLocator.Current.GetInstance<IScheduledServicesPageViewModel>();
                    scheduledServicesPageViewModel.LoadScheduledServices();
                    break;

                case AppPages.QualifyServicesPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new QualifyServicesPage());
                    IQualifyServicesPageViewModel calificateServicesPageViewModel = ServiceLocator.Current.GetInstance<IQualifyServicesPageViewModel>();
                    await calificateServicesPageViewModel.LoadCalificate(code);
                    break;

                case AppPages.NotificationsCenterPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new NotificationsCenterPage());
                    break;

                case AppPages.ServicesPage:
                    newMedicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
                    newMedicalCenterCoordinationPageViewModel.PersonSelected = null;
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ServicesPage());
                    break;

                case AppPages.HomeMedicalCarePage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new HomeMedicalCarePage());
                    break;

                case AppPages.AddNewAddressPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new AddNewAddressPage());
                    break;
                    
                case AppPages.AditionalDataPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new AditionalDataPage());
                    break;

                case AppPages.SubMenuPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SubMenuPage());
                    break;

                case AppPages.AdvanceLocationPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new AdvanceLocationPage());
                    break;
                case AppPages.HomeMedicalVideoCall:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new HomeMedicalVideoCallPage());
                    break;
                case AppPages.Precall:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new PreCallPage());
                    break;
                case AppPages.Queuing:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new QueuingPage());
                    break;
                case AppPages.EvaluateVideoCallPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new EvaluateVideoCallPage());
                    break;
                case AppPages.SurveyQueuingPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SurveyQueuingPage());
                    break;
                case AppPages.ExpiredMedicalServices:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new ExpiredMedicalServicesPage());
                    break;
                case AppPages.PediatricPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new PediatricPage());
                    break;
                case AppPages.RegisterMinorPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new RegisterMinorPage());
                    break;
                case AppPages.SchedulePediatricPage:
                    await (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new SchedulePediatricPage());
                    break; 
                case AppPages.ContingencyMessagePage:
                    await PopupNavigation.Instance.PushAsync(new ContingencyMessagePage());
                    break;

            }
        }

        private async Task ValidateCurrentService(AppPages page)
        {
            if ((page < AppPages.RegisterDataPersonalPage || page > AppPages.RegisterVerificationCodePage) &&
                 page != AppPages.LoginPage &&
                 page != AppPages.TermsAndConditionsPage &&
                 page != AppPages.PediatricPage &&
                 page != AppPages.SchedulePediatricPage &&
                 page != AppPages.ScheduledServicesPage &&
                 page != AppPages.DataManagementPolicyPage &&
                 page != AppPages.RegisterMinorPage)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    currentService = ServiceLocator.Current.GetInstance<ICurrentServiceViewViewModel>();
                    await currentService.GetMedicalHomeService();
                });
            }
        }

        public async Task ShowModal(AppPages page)
        {
            switch (page)
            {
                case AppPages.RememberPasswordPage:
                    await Application.Current.MainPage.Navigation.PushModalAsync(new RememberPasswordPage(), true);
                    break;

                case AppPages.TermsAndConditionsPQRSPage:
                    await Application.Current.MainPage.Navigation.PushModalAsync(new TermsAndConditionsPQRSPage(), true);
                    break;

                case AppPages.EditPasswordPage:
                    await Application.Current.MainPage.Navigation.PushModalAsync(new EditPasswordPage(), true);
                    break;

                case AppPages.ChatMedicalServicePage:
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ChatMedicalServicePage(), true);
                    break;

                case AppPages.SurveyQueuingPage:
                    await Application.Current.MainPage.Navigation.PushModalAsync(new SurveyQueuingPage(), true);
                    break;
                case AppPages.FirstChangePasswordPage:
                    await Application.Current.MainPage.Navigation.PushModalAsync(new FirstChangePasswordPage(), true);
                    break;
            }
        }

        public void CloseMenu()
        {
            (Application.Current.MainPage as MasterDetailPage).IsPresented = false;
        }

        public void RemovePage(int count)
        {
            if ((Application.Current.MainPage as MasterDetailPage)?.Detail?.Navigation?.NavigationStack == null ||
                (Application.Current.MainPage as MasterDetailPage)?.Detail?.Navigation?.NavigationStack?.Count <= 1)
                return;

            for (int i = 1; i <= count; i++)
            {
                var page = (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.NavigationStack[(Application.Current.MainPage as MasterDetailPage).Detail.Navigation.NavigationStack.Count - 2];
                if (page is LandingPage)
                    return;

                (Application.Current.MainPage as MasterDetailPage).Detail.Navigation.RemovePage(page);
            }
        }
    }
}
