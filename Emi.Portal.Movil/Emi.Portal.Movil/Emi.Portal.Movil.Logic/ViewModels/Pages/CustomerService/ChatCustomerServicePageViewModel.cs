namespace Emi.Portal.Movil.Logic.ViewModels.Pages.CustomerService
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class ChatCustomerServicePageViewModel : ViewModelBase, IChatCustomerServicePageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        ILoginViewModel loginViewModel;
        IPhoneService phoneService;
        IValidatorService validatorService;
        INavigationService navigationService;

        private UrlWebViewSource htmlSource;
        public UrlWebViewSource HtmlSource
        {
            get { return htmlSource; }
            set
            {
                if (htmlSource != value)
                {
                    htmlSource = value;
                    RaisePropertyChanged("HtmlSource");
                }
            }
        }

        public ICommand ExitChatCommand { get { return new RelayCommand(ExitChat); } }

        private async void ExitChat()
        {
            if (await dialogService.ShowConfirm(AppResources.ChatCustomerService, AppResources.ExitChat))
            {
                await navigationService.Back();
            }
        }

        public ChatCustomerServicePageViewModel(ILoginViewModel loginViewModel, IDialogService dialogService, IPhoneService phoneService, IApiService apiService, IValidatorService validatorService, INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.loginViewModel = loginViewModel;
            this.phoneService = phoneService;
            this.validatorService = validatorService;
            this.navigationService = navigationService;

            HtmlSource = new UrlWebViewSource();
        }

        public async Task LoadChatPage()
        {
            phoneService.DeleteCookie();

            RequestServiceChatAgent request = new RequestServiceChatAgent();
            ResponseServiceChatAgent response = await apiService.GetServiceChatAgent(request);
            ValidateResponseServiceChatAgent(response);
        }

        private async void ValidateResponseServiceChatAgent(ResponseServiceChatAgent response)
        {

            if (await validatorService.ValidateResponse(response) == false)
            {
                return;
            }

            string url = string.Format("{0}?name={1}", response.ChatAgent.UrlChatApp, string.Format("{0}({1}-{2})", loginViewModel.User.NameOne, loginViewModel.User.DocumentTypeName, loginViewModel.User.Document));

            if (string.IsNullOrEmpty(loginViewModel.User.UserName) == false)
            {
                url += string.Format("&email={0}", loginViewModel.User.UserName);
            }

            if (string.IsNullOrEmpty(loginViewModel.User.CellPhone) == false)
            {
                url += string.Format("&phone={0}", loginViewModel.User.CellPhone);
            }

            url += $"&urlService={AppConfigurations.UrlBaseMiddleware}/{AppConfigurations.ServicesController}/{AppConfigurations.ServiceChatAgent}";
            url += $"&urlServiceApiKey={AppConfigurations.Subscriptionkey}";
            url += $"&serviceType={(int)ServiceType.Other}";
            url += $"&environment={AppConfigurations.Environment}";

            byte[] utf8bytes = Encoding.UTF8.GetBytes(url);
            string r = Encoding.UTF8.GetString(utf8bytes, 0, utf8bytes.Length);

            HtmlSource.Url = r;

        }
    }
}
