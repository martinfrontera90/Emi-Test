namespace Emi.Portal.Movil.Logic.ViewModels.Pages.CustomerService
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class FaqsPageViewModel : ViewModelBase, IFaqsPageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;

        private string titlePage;
        public string TitlePage
        {
            get { return titlePage; }
            set
            {
                titlePage = value;
                RaisePropertyChanged("TitlePage");
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    RaisePropertyChanged("SearchText");                    
                }
            }
        }

        private FaqCompleteViewModel subCategorySelected;
        public FaqCompleteViewModel SubCategorySelected
        {
            get { return subCategorySelected; }
            set
            {
                if (subCategorySelected != value)
                {
                    subCategorySelected = value;
                    RaisePropertyChanged("SubCategorySelected");
                }
            }
        }

        private ObservableCollection<FaqCompleteViewModel> allFaqs;
        public ObservableCollection<FaqCompleteViewModel> AllFaqs
        {
            get { return allFaqs; }
            set
            {
                if (allFaqs != value)
                {
                    allFaqs = value;
                    RaisePropertyChanged("AllFaqs");
                }
            }
        }

        private ObservableCollection<FaqCompleteViewModel> groupCategoriesSubcategories;
        public ObservableCollection<FaqCompleteViewModel> GroupCategoriesSubcategories
        {
            get { return groupCategoriesSubcategories; }
            set
            {
                if (groupCategoriesSubcategories != value)
                {
                    groupCategoriesSubcategories = value;
                    RaisePropertyChanged("GroupCategoriesSubcategories");
                }
            }
        }

        private ObservableCollection<FaqCompleteViewModel> faqsFilter;
        public ObservableCollection<FaqCompleteViewModel> FaqsFilter
        {
            get { return faqsFilter; }
            set
            {
                if (faqsFilter != value)
                {
                    faqsFilter = value;
                    RaisePropertyChanged("FaqsFilter");
                }
            }
        }

        private bool isVisibleFind;
        public bool IsVisibleFind
        {
            get { return isVisibleFind; }
            set
            {
                if (IsVisibleFind != value)
                {
                    isVisibleFind = value;
                    RaisePropertyChanged("IsVisibleFind");
                }
            }
        }

        public ICommand SearchCommand { get { return new RelayCommand(Search); } }

       

        private void Search()
        {
            dialogService.ShowProgress();
            GroupCategoriesSubcategories = new ObservableCollection<FaqCompleteViewModel>();
            if (string.IsNullOrEmpty(SearchText))
            {
                GroupCategoriesSubcategories = AllFaqs;
            }
            else
            {
                GroupCategoriesSubcategories = new ObservableCollection<FaqCompleteViewModel>();

                GroupCategoriesSubcategories = new ObservableCollection<FaqCompleteViewModel>(
                    AllFaqs.Where
                    (x =>
                        x.CategoryName.ToLower().Contains(SearchText.ToLower()) ||
                        x.SubCategoryName.ToLower().Contains(SearchText.ToLower()) ||
                        x.Question.ToLower().Contains(SearchText.ToLower()) ||
                        x.AnswerText.ToLower().Contains(SearchText.ToLower()))
                    );
            }
            IsVisibleFind = GroupCategoriesSubcategories.Count == 0;
            dialogService.HideProgress();
        }


        public async Task LoadFaqs()
        {
            SearchText = string.Empty;
            dialogService.ShowProgress();
            ResponseFaqs responseFaqs = await apiService.GetAllFaqs();
            dialogService.HideProgress();
            ValidateResponseAllFaqs(responseFaqs);
            Search();
        }

        private async void ValidateResponseAllFaqs(ResponseFaqs responseFaqs)
        {
            if (responseFaqs.Success && responseFaqs.StatusCode == 0)
            {
                GroupCategoriesSubcategories = new ObservableCollection<FaqCompleteViewModel>();
                AllFaqs = new ObservableCollection<FaqCompleteViewModel>();
                foreach (FaqComplete item in responseFaqs.Faqs)
                {
                    FaqCompleteViewModel faq = new FaqCompleteViewModel
                    {
                        AnswerText = item.AnswerText,
                        CategoryName = item.CategoryName,
                        Question = item.Question,
                        SubCategoryName = item.SubCategoryName,
                    };
                    GroupCategoriesSubcategories.Add(faq);
                }
                AllFaqs = GroupCategoriesSubcategories;
                IsVisibleFind = GroupCategoriesSubcategories.Count == 0;
            }
            else
            {
                await dialogService.ShowMessage(responseFaqs.Title, responseFaqs.Message);
            }
        }

        public FaqsPageViewModel(IApiService apiService, IDialogService dialogService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;            
            AllFaqs = new ObservableCollection<FaqCompleteViewModel>();
            GroupCategoriesSubcategories = new ObservableCollection<FaqCompleteViewModel>();
            IsVisibleFind = false;
        }
    }
}
