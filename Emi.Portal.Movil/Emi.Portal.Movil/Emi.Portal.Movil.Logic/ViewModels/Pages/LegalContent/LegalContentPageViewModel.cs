namespace Emi.Portal.Movil.Logic.ViewModels.Pages.LegalContent
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.LegalContent;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;
    using Xamarin.Forms;

    public class LegalContentPageViewModel : ViewModelBase, ILegalContentPageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;

        private HtmlWebViewSource htmlSource;
        public HtmlWebViewSource HtmlSource
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

        private string icon;
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        private string titleLegalContent;
        public string TitleLegalContent
        {
            get { return titleLegalContent; }
            set
            {
                if (titleLegalContent != value)
                {
                    titleLegalContent = value;
                    RaisePropertyChanged("TitleLegalContent");
                }
            }
        }
        public ICommand CancelCallCommand { get { return new RelayCommand(CancelCall); } }

        private bool fromRegister;
        public bool FromRegister
        {
            get { return fromRegister; }
            set
            {
                if (fromRegister != value)
                {
                    fromRegister = value;
                    RaisePropertyChanged("FromRegister");
                }
            }
        }

        private void CancelCall()
        {
            if (FromRegister)
            {
                IRegisterPageViewModel registerPageViewModel = ServiceLocator.Current.GetInstance<IRegisterPageViewModel>();
                registerPageViewModel.Cancel();
                return;
            }

            ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
            callViewModel.CallCategory();
        }

        public async Task LoadContentLegal(string Tag)
        {
            dialogService.ShowProgress();
            HtmlSource.Html = string.Empty;
            TitleLegalContent = string.Empty;
            RequestLegalContent request = new RequestLegalContent
            {
                Tag = Tag
            };
            ResponseLegalContent response = await apiService.GetLegalContent(request);
            dialogService.HideProgress();
            if (response.Success && response.StatusCode == 0)
            {
                TitleLegalContent = Tag == AppConfigurations.TagPPC ? "Política de tratamiento de datos" : "Términos y condiciones de uso";
                HtmlSource.Html = response.LegalContent.Content;
            }
            else
            {
                await dialogService.ShowMessage(response.Title, response.Message);
                await navigationService.Navigate(AppPages.LandingPage);
            }
        }
        public LegalContentPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            HtmlSource = new HtmlWebViewSource();
        }
    }
}
