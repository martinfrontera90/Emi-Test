namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Emi.Portal.Movil.Logic.Models.Domain;

    public class PlanBeneficiaryGroup : ObservableCollection<HiredProductGroup>, INotifyPropertyChanged
    {
        public ObservableCollection<HiredProduct> Products { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public string DocumentType { get; set; }

        public int HeightProduct
        {
            get
            {
                if (IsExpanded)
                {
                    return Products.Count * 60;
                }
                return 60;
            }
        }

        public int IconRotation
        {
            get
            {
                if (IsExpanded)
                {
                    return 270;
                }
                return 90;
            }
        }

        bool isExpanded;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsExpanded)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IconRotation)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(HeightProduct)));
                if (IsExpanded)
                {
                    Add(new HiredProductGroup { HiredProducts = Products });
                }
                else
                {
                    Clear();
                }
            }
        }


        public PlanBeneficiaryGroup(string document, string documentType, string name, ObservableCollection<HiredProduct> products)
        {
            DocumentType = documentType;
            Document = document;
            Products = products;
            Name = name;
            IsExpanded = false;
        }
    }
}
