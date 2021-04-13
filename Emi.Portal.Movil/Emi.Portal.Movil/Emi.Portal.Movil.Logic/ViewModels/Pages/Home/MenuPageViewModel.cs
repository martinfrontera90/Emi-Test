namespace Emi.Portal.Movil.Logic.ViewModels.Pages.Home
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Home;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using Xamarin.Essentials;

    public class MenuPageViewModel : ViewModelBase, IMenuPageViewModel
    {
        ILoginViewModel loginViewModel;
        ISubMenuItemViewModel subMenuItemViewModel;
        IApiService apiService;

        public RPWithdrawalAndCard WithdrawalAndCard { get; set; }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    RaisePropertyChanged("UserName");
                }
            }
        }

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                RaisePropertyChanged("IsRunning");
            }
        }

        private string version;
        public string Version
        {
			get { return version; }
            set
            {
				if (version != value)
                {
					version = value;
					RaisePropertyChanged("Version");
                }
            }
        }

        private string appVersion;
        public string AppVersion
        {
            get { return appVersion; }
            set
            {
                appVersion = value;
                RaisePropertyChanged(nameof(AppVersion));
            }
        }

        ObservableCollection<MenuItemViewModel> menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get { return menuItems; }
            set
            {
                menuItems = value;
                RaisePropertyChanged("MenuItems");
            }
        }

        public async void ValidateOption()
        {
            try
            {

                Request request = new Request
                {
                    Document = loginViewModel.User.Document,
                    DocumentType = loginViewModel.User.DocumentType,
                    Controller = "withdrawalretired",
                    Action = "ValidateRPWithdrawalRetired"
                };
                var response = await apiService.ValidateRPWithdrawalRetired(request);
                if (response.Success)
                {
                    WithdrawalAndCard = response.ValidateRPWithdrawalAndCard;
                }
                else
                {
                    WithdrawalAndCard.ValidateCard = false;
                    WithdrawalAndCard.ValidateRP = false;
                }
                await OldMenu();
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
                WithdrawalAndCard.ValidateCard = false;
                WithdrawalAndCard.ValidateRP = false;
            }
        }

        

        private MenuItemViewModel selectedItem;
        public MenuItemViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                    if (SelectedItem != null)
                        subMenuItemViewModel.Items = SelectedItem.SubMenuItems;
                }
            }
        }

        public async void LoadMenu()
        {
            try
            {
                IsRunning = true;
                UserName = string.Format("{0} {1}", loginViewModel.User.NameOne, loginViewModel.User.LastNameOne);
                Email = loginViewModel.User.UserName;
                var request = new Request
                {
                    Action = "GetListAssociatedMenus",
                    Controller = "common",
                    Document = loginViewModel.User.Document,
                    DocumentType = loginViewModel.User.DocumentType,
                    Email = loginViewModel.User.UserName
                };
                var response = await apiService.GetMenu(request);
                MenuItems = new ObservableCollection<MenuItemViewModel>();
                foreach (var item in response.MenuItems)
                {
                    var subItems = new ObservableCollection<SubMenuItemViewModel>();
                    if (item.MenuChilds.Count > 0)
                    {

                        foreach (var subItem in item.MenuChilds)
                        {
                            subItems.Add(new SubMenuItemViewModel
                            {
                                Title = subItem.MenuName,
                                Page = (AppPages)int.Parse(subItem.ResourceApp.Trim())
                            });
                            if ((AppPages)int.Parse(subItem.ResourceApp.Trim()) == AppPages.ScheduledServicesPage)
                            {
                                IScheduledServicesPageViewModel scheduledServicesPage = ServiceLocator.Current.GetInstance<IScheduledServicesPageViewModel>();
                                scheduledServicesPage.TitlePage = subItem.MenuName;
                            }
                        }
                    }
                    MenuItems.Add(new MenuItemViewModel
                    {
                        Icon = item.ImageApp,
                        Page = (AppPages)int.Parse(item.ResourceApp.Trim()),
                        Title = item.MenuName,
                        SubMenuItems = subItems
                    });
                    if ((AppPages)int.Parse(item.ResourceApp.Trim()) == AppPages.ServicesPage)
                    {
                        IServicesPageViewModel servicesPageViewModel = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();
                        servicesPageViewModel.TitlePage = item.MenuName;
                    }
                };
            }
            catch(Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
            finally
            {
                MenuItemViewModel Logout = new MenuItemViewModel
                {
                    Page = AppPages.LoginPage,
                    Title = AppResources.TitleLogOut,
                    Icon = AppConfigurations.IconLogOut
                };
                MenuItems.Add(Logout);
                IsRunning = false;
            }

        }



        public async Task OldMenu()
        {
            MenuItemViewModel Landing = new MenuItemViewModel
            {
                Icon = AppConfigurations.IconHome,
                Page = AppPages.LandingPage,
                Title = AppResources.TitleLandingPage
            };

            MenuItemViewModel MyAccount = new MenuItemViewModel
            {
                Icon = AppConfigurations.IconUser,
                Page = AppPages.MyAccount,
                Title = AppResources.TitleMyAccount,
            };

            SubMenuItemViewModel PersonalInformation = new SubMenuItemViewModel { Page = AppPages.PersonalDataPage, Title = AppResources.TitlePersonalInformation };
            SubMenuItemViewModel Beneficiary = new SubMenuItemViewModel { Page = AppPages.BeneficiariesPage, Title = AppResources.TitleBenericiaries };
            SubMenuItemViewModel Family = new SubMenuItemViewModel { Page = AppPages.FamilyPage, Title = AppResources.TitleFamily };
            SubMenuItemViewModel Invoices = new SubMenuItemViewModel { Page = AppPages.SearchInvoicesPage, Title = AppResources.TitleMyInvoices };
            SubMenuItemViewModel ChangePassword = new SubMenuItemViewModel { Page = AppPages.ChangePasswordPage, Title = AppResources.TitleChangePassword };
            SubMenuItemViewModel ChangeEmail = new SubMenuItemViewModel { Page = AppPages.ChangeEmailPage, Title = AppResources.TitleChangeEmail };
            SubMenuItemViewModel DisableAccount = new SubMenuItemViewModel { Page = AppPages.DisableAccountPage, Title = AppResources.TitleDisableAccount };
            SubMenuItemViewModel Certificates = new SubMenuItemViewModel { Page = AppPages.CertificatesPage, Title = AppResources.TitleCertificates };
            SubMenuItemViewModel ProductsAndPlans = new SubMenuItemViewModel { Page = AppPages.ProductsAndPlans, Title = AppResources.TitleProductsAndPlans };
            SubMenuItemViewModel Cards = new SubMenuItemViewModel { Page = AppPages.CardsPage, Title = AppResources.TitleCard };

            if (loginViewModel.User.AffiliateType != AffiliateType.NoAffiliate)
            {
                MyAccount.SubMenuItems.Add(PersonalInformation);
            }

            if (WithdrawalAndCard.ValidateCard)
                MyAccount.SubMenuItems.Add(Cards);
            //if (loginViewModel.User.AffiliateType != AffiliateType.NoAffiliate)
            //{
            //    MyAccount.SubMenuItems.Add(Cards);
            //}

            if (loginViewModel.User.AffiliateType == AffiliateType.ResponsiblePayment)
            {
                MyAccount.SubMenuItems.Add(Beneficiary);
            }

            MyAccount.SubMenuItems.Add(ProductsAndPlans);

            MyAccount.SubMenuItems.Add(Family);

            if (loginViewModel.User.AffiliateType == AffiliateType.ResponsiblePayment)
            {
                MyAccount.SubMenuItems.Add(Invoices);
            }

            if (loginViewModel.User.AffiliateType != AffiliateType.NoAffiliate)
            {
                MyAccount.SubMenuItems.Add(Certificates);
            }

            MyAccount.SubMenuItems.Add(ChangePassword);
            MyAccount.SubMenuItems.Add(ChangeEmail);
            MyAccount.SubMenuItems.Add(DisableAccount);

            MenuItemViewModel MyServices = new MenuItemViewModel
            {
                Icon = AppConfigurations.IconMyHealth,
                Page = AppPages.MyHealth,
                Title = AppResources.TitleMyServices
            };

            SubMenuItemViewModel ServicesHistory = new SubMenuItemViewModel { Page = AppPages.SearchServicesHistoryPage, Title = AppResources.TitleServicesHistory };
            SubMenuItemViewModel SheduledServices = new SubMenuItemViewModel { Page = AppPages.ScheduledServicesPage, Title = AppResources.TitleSheduledServices };
            SubMenuItemViewModel ExpiredMedicalServices = new SubMenuItemViewModel { Page = AppPages.ExpiredMedicalServices, Title = AppResources.TitleExpiredMedicalServices };

            MyServices.SubMenuItems.Add(ServicesHistory);
            MyServices.SubMenuItems.Add(SheduledServices);
            if (loginViewModel.User.AffiliateType != AffiliateType.NoAffiliate)
                MyServices.SubMenuItems.Add(ExpiredMedicalServices);

            MenuItemViewModel RequestService = new MenuItemViewModel
            {
                Icon = "ic_add_circle",
                Page = AppPages.ServicesPage,
                Title = AppResources.TitleRequestRervice
            };

            MenuItemViewModel CustomService = new MenuItemViewModel
            {
                Icon = AppConfigurations.IconCustomerService,
                Page = AppPages.MyAccount,
                Title = AppResources.TitleCustomService
            };

            SubMenuItemViewModel Faqs = new SubMenuItemViewModel { Page = AppPages.FaqsPage, Title = AppResources.TitleFaqs };
            SubMenuItemViewModel PQRS = new SubMenuItemViewModel { Page = AppPages.PQRSPage, Title = AppResources.TitlePQRS };
            SubMenuItemViewModel TermsAndConditions = new SubMenuItemViewModel { Page = AppPages.TermsAndConditionsPage, Title = AppResources.TermsAndConditions };
            SubMenuItemViewModel DataManagementPolicy = new SubMenuItemViewModel { Page = AppPages.DataManagementPolicyPage, Title = AppResources.DataManagementPolicy };
            //SubMenuItemViewModel ChatCustomerService = new SubMenuItemViewModel { Page = AppPages.ChatCustomerServicePage, Title = AppResources.ChatCustomerService };

            //CustomService.SubMenuItems.Add(ChatCustomerService);

           
            CustomService.SubMenuItems.Add(Faqs);
            CustomService.SubMenuItems.Add(PQRS);
            CustomService.SubMenuItems.Add(TermsAndConditions);
            CustomService.SubMenuItems.Add(DataManagementPolicy);

            MenuItemViewModel NearbyClinicsPage = new MenuItemViewModel
            {
                Icon = AppConfigurations.IconGps,
                Page = AppPages.NearbyClinicsPage,
                Title = AppResources.TitleNearbyClinics
            };


            MenuItemViewModel Logout = new MenuItemViewModel
            {
                Page = AppPages.LoginPage,
                Title = AppResources.TitleLogOut,
                Icon = AppConfigurations.IconLogOut
            };

            MenuItems.Add(Landing);
            MenuItems.Add(MyAccount);
            MenuItems.Add(RequestService);
            MenuItems.Add(NearbyClinicsPage);
            MenuItems.Add(MyServices);
            MenuItems.Add(CustomService);
            MenuItems.Add(Logout);
        }

        public MenuPageViewModel(ILoginViewModel loginViewModel, IApiService apiService)
        {
            this.loginViewModel = loginViewModel;
            this.apiService = apiService;
            this.apiService = apiService;
            this.loginViewModel = loginViewModel;
            subMenuItemViewModel = ServiceLocator.Current.GetInstance<ISubMenuItemViewModel>();
            MenuItems = new ObservableCollection<MenuItemViewModel>();
            //ValidateOption();
            AppVersion = VersionTracking.CurrentVersion;
        }
    }
}
