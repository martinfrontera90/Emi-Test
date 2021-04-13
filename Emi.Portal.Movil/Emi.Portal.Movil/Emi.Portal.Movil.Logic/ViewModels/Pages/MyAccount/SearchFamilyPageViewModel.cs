namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyAccount
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyAccount;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class SearchFamilyPageViewModel : ViewModelBase, ISearchFamilyPageViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;

        private string document;
        public string Document
        {
            get { return document; }
            set
            {
                if (document != value)
                {
                    document = value;
                    RaisePropertyChanged("Document");
                }
            }
        }

        public ObservableCollection<Document> Documents { get; set; }

        private Document documentSelected;
        public Document DocumentSelected
        {
            get { return documentSelected; }
            set
            {
                if (documentSelected != value)
                {
                    documentSelected = value;
                    RaisePropertyChanged("DocumentSelected");
                }
            }
        }

        private string errorDocument;
        public string ErrorDocument
        {
            get { return errorDocument; }
            set
            {
                if (errorDocument != value)
                {
                    errorDocument = value;
                    RaisePropertyChanged("ErrorDocument");
                }
            }
        }

        private string errorDocumentType;
        public string ErrorDocumentType
        {
            get { return errorDocumentType; }
            set
            {
                if (errorDocumentType != value)
                {
                    errorDocumentType = value;
                    RaisePropertyChanged("");
                }
            }
        }
        #endregion


        public ICommand SearchFamilyCommand { get { return new RelayCommand(SearchFamily); } }

        private async void SearchFamily()
        {
            ValidateDocument();
            if (string.IsNullOrEmpty(ErrorDocument) && string.IsNullOrEmpty(ErrorDocumentType))
            {
                dialogService.ShowProgress();
                RequestSearchMember request = new RequestSearchMember();
                request.Number = Document;
                request.DocumentType = DocumentSelected.Code;
                ResponseSearchMember response = await apiService.SearchMember(request);
                ValidateResponseSearchMember(response);
            }
        }

        private async void ValidateResponseSearchMember(ResponseSearchMember response)
        {
            dialogService.HideProgress();

            if (response.Success)
            {
                IAddFamilyPageViewModel addFamilyPageViewModel = ServiceLocator.Current.GetInstance<IAddFamilyPageViewModel>();
                addFamilyPageViewModel.Title = "Resultado de la búsqueda";

                if (response.StatusCode == 0)
                {
                    addFamilyPageViewModel.Member = response.Member;
                    addFamilyPageViewModel.Message = "Tu familiar fue encontrado con los siguientes datos.";
                }
                else
                {
                    addFamilyPageViewModel.Member = new Person
                    {
                        Document = Document,
                        DocumentType = DocumentSelected.Code,
                    };

                    addFamilyPageViewModel.Message = AppResources.FamilyDoesNotFind;
                    await dialogService.ShowMessage(response.Title, response.Message);
                }
                await navigationService.Navigate(AppPages.AddFamilyPage);
            }
        }

        private void ValidateDocument()
        {
            ErrorDocument = string.IsNullOrEmpty(Document) ? AppResources.DocumentRequired : string.Empty;
            ErrorDocumentType = DocumentSelected == null ? AppResources.DocumentTypeRequired : string.Empty;
        }

        public async Task LoadDocuments()
        {
            dialogService.ShowProgress();
            ResponseDocuments response = await apiService.GetDocuments(new RequestDocument());
            dialogService.HideProgress();
            Documents.Clear();
            if (response.Success && response.Documents != null)
            {
                foreach (Document item in response.Documents)
                    Documents.Add(item);
                return;
            }

            await dialogService.ShowMessage(response.Message, response.Title);
        }

        public SearchFamilyPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            Documents = new ObservableCollection<Document>();
        }
    }
}
