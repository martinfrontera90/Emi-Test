namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.ObjectModel;
    using GalaSoft.MvvmLight;

    public class SubcategoryViewModel : ViewModelBase
    {
        private string name;

        public string SubName
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("SubName");
            }
        }

        private ObservableCollection<FaqViewModel> faqs;
        public ObservableCollection<FaqViewModel> Faqs
        {
            get { return faqs; }
            set
            {
                if (faqs != value)
                {
                    faqs = value;
                    RaisePropertyChanged("Faqs");
                }
            }
        }

    }
}
