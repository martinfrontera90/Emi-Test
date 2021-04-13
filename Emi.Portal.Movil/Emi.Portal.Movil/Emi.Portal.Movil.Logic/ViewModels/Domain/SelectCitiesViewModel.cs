namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using GalaSoft.MvvmLight;

    public class SelectCitiesViewModel : ViewModelBase
    {
        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                RaisePropertyChanged(nameof(Code));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }


    }
}
