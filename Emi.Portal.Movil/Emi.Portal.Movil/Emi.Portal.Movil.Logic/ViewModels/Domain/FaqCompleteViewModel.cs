namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.CustomerService;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class FaqCompleteViewModel : ViewModelBase, IFaqCompleteViewModel
    {
        INavigationService navigationService;
        IDialogService dialogService;

        private string answer;
        public string AnswerText
        {
            get { return answer; }
            set
            {
                if (answer != value)
                {
                    answer = value;
                    RaisePropertyChanged("AnswerText");
                }
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                if (categoryName != value)
                {
                    categoryName = value;
                    RaisePropertyChanged("CategoryName");
                }
            }
        }

        private string categoriesFaqsId;
        public string CategoriesFaqsId
        {
            get { return categoriesFaqsId; }
            set
            {
                if (categoriesFaqsId != value)
                {
                    categoriesFaqsId = value;
                    RaisePropertyChanged("CategoriesFaqsId");
                }
            }
        }

        private string faqsId;
        public string FaqsId
        {
            get { return faqsId; }
            set
            {
                if (faqsId != value)
                {
                    faqsId = value;
                    RaisePropertyChanged("FaqsId");
                }
            }
        }

        private string subCategoryName;
        public string SubCategoryName
        {
            get { return subCategoryName; }
            set
            {
                if (subCategoryName != value)
                {
                    subCategoryName = value;
                    RaisePropertyChanged("SubCategoryName");
                }
            }
        }

        private string subcategoriesFaqsId;
        public string SubcategoriesFaqsId
        {
            get { return subcategoriesFaqsId; }
            set
            {
                if (subcategoriesFaqsId != value)
                {
                    subcategoriesFaqsId = value;
                    RaisePropertyChanged("SubcategoriesFaqsId");
                }
            }
        }

        private string question;
        public string Question
        {
            get { return question; }
            set
            {
                if (question != value)
                {
                    question = value;
                    RaisePropertyChanged("Question");
                }
            }
        }

        public ICommand SelectedSubcategoryCommand { get { return new RelayCommand(SelectedSubcategory); } }

        private async void SelectedSubcategory()
        {
            dialogService.ShowProgress();
            IFaqsPageViewModel faqsPageView = ServiceLocator.Current.GetInstance<IFaqsPageViewModel>();
            faqsPageView.SubCategorySelected = this;
            var List = faqsPageView.GroupCategoriesSubcategories.Where(x => x.subCategoryName == SubCategoryName).ToList();
            faqsPageView.FaqsFilter = new ObservableCollection<FaqCompleteViewModel>();
            foreach (var item in List)
            {
                if (faqsPageView.FaqsFilter.Where(x=> x.AnswerText == item.AnswerText && x.Question == item.Question).FirstOrDefault() == null)
                {
                    FaqCompleteViewModel faqCompleteViewModel = new FaqCompleteViewModel
                    {
                        AnswerText = item.AnswerText,
                        Question = item.Question
                    };
                    faqsPageView.FaqsFilter.Add(faqCompleteViewModel);
                }

            }
            dialogService.HideProgress();
            await navigationService.Navigate(AppPages.FaqsDetailPage);
        }

        public FaqCompleteViewModel()
        {
            dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }
    }
}
