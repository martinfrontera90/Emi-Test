namespace Emi.Portal.Movil.Logic.ViewModels.Pages.Loader
{
    using System;
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Home;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Loader;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using CommonServiceLocator;
    using Xamarin.Forms;
    using Xamarin.Essentials;
    using Plugin.StoreReview;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.Popup;

    public class LoaderPageViewModel : ILoaderPageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        IExceptionService exceptionService;
        IFileService fileService;
        INavigationService navigationService;
        ILoginViewModel loginViewModel;
        IPhoneService phoneService;

        public async Task Start(string id, string message, string code, string url)
        {
            try
            {
                dialogService.ShowProgress();

                await ReviewVersion();

                if (await fileService.FileExists(string.Format("{0} User", AppConfigurations.Brand)))
                {
                    ResponseLogin userSaved = await fileService.LoadAsync<ResponseLogin>(string.Format("{0} User", AppConfigurations.Brand));

                    if (userSaved != null && userSaved.Expires != null && userSaved.Expires_in > 0)
                    {
                        dialogService.ShowProgress();

                        loginViewModel.User = new ResponseLogin();
                        ViewModelHelper.CloneUser(loginViewModel, userSaved);
                        apiService.Token = loginViewModel.User.Access_token;

                        var req = new RequestEnabledSession
                        {
                            Code = loginViewModel.User.SessionCode,
                            Document = loginViewModel.User.Document,
                            DocumentType = loginViewModel.User.DocumentType,
                            Controller = "Account",
                            Action = "EnabledSession"
                        };
                        RequestSoftwareVersion request = new RequestSoftwareVersion();
                        ResponseSoftwareVersion response = await apiService.GetSoftwareVersion(request);
                        IMenuPageViewModel menuPageViewModel = ServiceLocator.Current.GetInstance<IMenuPageViewModel>();
                        if (response.Success && response.StatusCode == 0)
                        {
                            menuPageViewModel.Version = response.Value;
                        }
                        menuPageViewModel.LoadMenu();
                        await navigationService.Navigate(AppPages.MenuPage);
                        ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
                        await callViewModel.Init();

                        
                        
                        INotificationService notificationService = ServiceLocator.Current.GetInstance<INotificationService>();
                        notificationService.RegisterNotifications();

                        //if (id != null)
                        //{
                        //    switch (id)
                        //    {
                        //        case "1":
                        //            await navigationService.Navigate(AppPages.MedicalCenterCoordinationPage);
                        //            break;
                        //        case "2":
                        //            await navigationService.Navigate(AppPages.QualifyServicesPage, false, code);
                        //            break;
                        //        case "3":
                        //            await navigationService.Navigate(AppPages.ScheduledServicesPage);
                        //            break;                                
                        //    }
                        //    return;
                        //}

                        await navigationService.Navigate(AppPages.LandingPage);
                        dialogService.HideProgress();
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            await Launcher.OpenAsync(url);
                        }
                        IContingencyMessagePageViewModel contingencyMessage = ServiceLocator.Current.GetInstance<IContingencyMessagePageViewModel>();
                        contingencyMessage.LoadData();
                        return;


                    }
                }

                await navigationService.Navigate(AppPages.LoginPage);
            }
            catch (Exception ex)
            {
                exceptionService.RegisterException(ex);
                INotificationService notificationsService = ServiceLocator.Current.GetInstance<INotificationService>();
                ILoginPageViewModel loginPageViewModel = ServiceLocator.Current.GetInstance<ILoginPageViewModel>();
                INotificationService notificationService = ServiceLocator.Current.GetInstance<INotificationService>();

                loginViewModel.User = null;
                loginPageViewModel.Email = string.Empty;
                loginPageViewModel.Password = string.Empty;
                notificationsService.UnregisterNotifications();

                await fileService.SaveAsync(string.Format("{0} User", AppConfigurations.Brand), loginViewModel.User);
                await navigationService.Navigate(AppPages.LoginPage);
            }
            finally
            {
                dialogService.HideProgress();
            }
        }

        public async Task ReviewVersion()
        {
            try
            {
                dialogService.ShowProgress();
                ResponseVersion responseVersion = await apiService.ValidateVersion();
                dialogService.HideProgress();

                if (responseVersion.Success && responseVersion.StatusCode == 0)
                {
                    while (Double.Parse(VersionTracking.CurrentVersion) < Double.Parse(responseVersion.Version))
                    {
                        fileService.Delete(string.Format("{0} User", AppConfigurations.Brand));
                        await navigationService.Navigate(AppPages.LoginPage);
                        await dialogService.ShowMessage(AppResources.Update, AppResources.UpdateRequired);
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            CrossStoreReview.Current.OpenStoreReviewPage("net.runid.ucm");
                        }
                        else
                        {
                            CrossStoreReview.Current.OpenStoreListing("1034898650");
                        }

                        //ICloseApplication closeApplication = ServiceLocator.Current.GetInstance<ICloseApplication>();
                        //closeApplication.closeApplication();
                    }

                }

            }
            catch (Exception ex)
            {
                exceptionService.RegisterException(ex);
                return;
            }
        }

        public LoaderPageViewModel(IApiService apiService, IDialogService dialogService, IExceptionService exceptionService, IFileService fileService, INavigationService navigationService, ILoginViewModel loginViewModel, IPhoneService phoneService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.exceptionService = exceptionService;
            this.fileService = fileService;
            this.navigationService = navigationService;
            this.loginViewModel = loginViewModel;
            this.phoneService = phoneService;
        }
    }
}
