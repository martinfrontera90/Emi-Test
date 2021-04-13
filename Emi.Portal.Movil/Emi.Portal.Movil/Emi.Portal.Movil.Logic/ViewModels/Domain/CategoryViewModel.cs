using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    public class CategoryViewModel : ViewModelBase
    { 
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private ObservableCollection<SubcategoryViewModel> subcategories;
        public ObservableCollection<SubcategoryViewModel> Subcategories
        {
            get { return subcategories; }
            set
            {
                if (subcategories != value)
                {
                    subcategories = value;
                    RaisePropertyChanged("Subcategories");
                }
            }
        }


    }
}
