namespace Emi.Portal.Movil.Converts
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Xamarin.Forms;
    public class GroupListViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<FaqCompleteViewModel> allCategories = value != null ? value as ObservableCollection<FaqCompleteViewModel> : new ObservableCollection<FaqCompleteViewModel>();
            allCategories = allCategories ?? new ObservableCollection<FaqCompleteViewModel>();

            List<KeyedList<string, FaqCompleteViewModel>> allSubcategoriesByCategory = new List<KeyedList<String, FaqCompleteViewModel>>();

            IEnumerable<IGrouping<string, FaqCompleteViewModel>> groupedCategories = from i in allCategories group i by i.CategoryName;

            IEnumerable<FaqCompleteViewModel> filter;

            foreach (IGrouping<string, FaqCompleteViewModel> item in groupedCategories)
            {
                List<FaqCompleteViewModel> subCategoriesByCategory = allCategories.Where(i => i.CategoryName == item.Key).ToList();

                filter = new List<FaqCompleteViewModel>();
                filter = subCategoriesByCategory.
                GroupBy(x => x.SubCategoryName).Select(x => x.First());

                allSubcategoriesByCategory.Add(new KeyedList<String, FaqCompleteViewModel>(item.Key, filter));
            }

            return allSubcategoriesByCategory;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
