using System.Windows.Input;
using Emi.Portal.Movil.Logic.Contracts.Services;
using Emi.Portal.Movil.Logic.Models.Domain;
using Emi.Portal.Movil.Logic.Resources;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CommonServiceLocator;

namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    public class AddressViewModel : ViewModelBase, IAddressViewModel
    {
        IDialogService dialogService;
        INavigationService navigationService;

        public bool Coverage { get; set; }
        public string Country { get; set; }

        private string department;
        public string Department
        {
            get { return department; }
            set
            {
                if (department != value)
                {
                    department = value;
                    RaisePropertyChanged("Department");
                }
            }
        }

        private string titleCity;
        public string TitleCity
        {
            get { return titleCity; }
            set
            {
                if (titleCity != value)
                {
                    titleCity = value;
                    RaisePropertyChanged("TitleCity");
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    RaisePropertyChanged("City");
                }
            }
        }

        private string neighborhood;
        public string Neighborhood
        {
            get { return neighborhood; }
            set
            {
                if (neighborhood != value)
                {
                    neighborhood = value;
                    RaisePropertyChanged("Neighborhood");
                }
            }
        }

        private string street;
        public string Street
        {
            get { return street; }
            set
            {
                if (street != value)
                {
                    street = value;
                    RaisePropertyChanged("Street");
                }
            }
        }

        private string streetSO;
        public string StreetSO
        {
            get { return streetSO; }
            set
            {
                if (streetSO != value)
                {
                    streetSO = value;
                    RaisePropertyChanged("StreetSO");
                }
            }
        }

        private string doorNumber;
        public string DoorNumber
        {
            get { return doorNumber; }
            set
            {
                if (doorNumber != value)
                {
                    doorNumber = value;
                    RaisePropertyChanged("DoorNumber");
                    CreateDetailAddress();
                }
            }
        }

        private string errorDoorNumber;
        public string ErrorDoorNumber
        {
            get { return errorDoorNumber; }
            set
            {
                if (errorDoorNumber != value)
                {
                    errorDoorNumber = value;
                    RaisePropertyChanged("ErrorDoorNumber");
                }
            }
        }


        private string bis;
        public string Bis
        {
            get { return bis; }
            set
            {
                if (bis != value)
                {
                    bis = value;
                    RaisePropertyChanged("Bis");
                }
            }
        }

        private string apartment;
        public string Apartment
        {
            get { return apartment; }
            set
            {
                if (apartment != value)
                {
                    apartment = value;
                    RaisePropertyChanged("Apartment");
                }
            }
        }

        private string corner;
        public string Corner
        {
            get { return corner; }
            set
            {
                if (corner != value)
                {
                    corner = value;
                    RaisePropertyChanged("Corner");
                }
            }
        }

        private string addressDetails;
        public string AddressDetails
        {
            get { return addressDetails; }
            set
            {
                if (addressDetails != value)
                {
                    addressDetails = value;
                    RaisePropertyChanged("AddressDetails");
                }
            }
        }

        private string numberApto;
        public string NumberApto
        {
            get { return numberApto; }
            set
            {
                if (numberApto != value)
                {
                    numberApto = value;
                    RaisePropertyChanged("NumberApto");
                }
            }
        }

        private string errorStreet;
        public string ErrorStreet
        {
            get { return errorStreet; }
            set
            {
                if (errorStreet != value)
                {
                    errorStreet = value;
                    RaisePropertyChanged("ErrorStreet");
                }
            }
        }

        private string errorDepartment;
        public string ErrorDepartment
        {
            get { return errorDepartment; }
            set
            {
                if (errorDepartment != value)
                {
                    errorDepartment = value;
                    RaisePropertyChanged("ErrorDepartment");
                }
            }
        }

        private string errorDirection;
        public string ErrorDirection
        {
            get { return errorDirection; }
            set
            {
                if (errorDirection != value)
                {
                    errorDirection = value;
                    RaisePropertyChanged("ErrorDirection");
                }
            }
        }

        private string errorCity;
        public string ErrorCity
        {
            get { return errorCity; }
            set
            {
                if (errorCity != value)
                {
                    errorCity = value;
                    RaisePropertyChanged("ErrorCity");
                }
            }
        }

        private string errorNeighborhood;
        public string ErrorNeighborhood
        {
            get { return errorNeighborhood; }
            set
            {
                if (errorNeighborhood != value)
                {
                    errorNeighborhood = value;
                    RaisePropertyChanged("ErrorNeighborhood");
                }
            }
        }

        private string direction;
        public string Direction
        {
            get { return direction; }
            set
            {
                if (direction != value)
                {
                    direction = value;
                    RaisePropertyChanged("Direction");
                }
            }
        }

        public string StandardizedAddress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        private Via viaSelected;
        public Via ViaSelected
        {
            get { return viaSelected; }
            set
            {
                if (viaSelected != value)
                {
                    viaSelected = value;
                    RaisePropertyChanged("ViaSelected");
                    CreateDetailAddress();
                }
            }
        }

        private string errorMainRoadType;
        public string ErrorMainRoadType
        {
            get { return errorMainRoadType; }
            set
            {
                if (errorMainRoadType != value)
                {
                    errorMainRoadType = value;
                    RaisePropertyChanged("ErrorMainRoadType");
                }
            }
        }

        private string numberMainRoad;
        public string NumberMainRoad
        {
            get { return numberMainRoad; }
            set
            {
                if (numberMainRoad != value)
                {
                    numberMainRoad = value;
                    RaisePropertyChanged("NumberMainRoad");
                    CreateDetailAddress();
                }
            }
        }

        private string errorNumberMainRoad;
        public string ErrorNumberMainRoad
        {
            get { return errorNumberMainRoad; }
            set
            {
                if (errorNumberMainRoad != value)
                {
                    errorNumberMainRoad = value;
                    RaisePropertyChanged("ErrorNumberMainRoad");
                }
            }
        }

        private string letterMainRoad;
        public string LetterMainRoad
        {
            get { return letterMainRoad; }
            set
            {
                if (letterMainRoad != value)
                {
                    letterMainRoad = value;
                    RaisePropertyChanged("LetterMainRoad");
                    CreateDetailAddress();
                }
            }
        }

        private Quadrant quadrantMainRoad;
        public Quadrant QuadrantMainRoad
        {
            get { return quadrantMainRoad; }
            set
            {
                if (quadrantMainRoad != value)
                {
                    quadrantMainRoad = value;
                    RaisePropertyChanged("QuadrantMainRoad");
                    CreateDetailAddress();
                }
            }
        }

        private string numberNomenclature;
        public string NumberNomenclature
        {
            get { return numberNomenclature; }
            set
            {
                if (numberNomenclature != value)
                {
                    numberNomenclature = value;
                    RaisePropertyChanged("NumberNomenclature");
                    CreateDetailAddress();
                }
            }
        }

        private string errorNumberNomenclature;
        public string ErrorNumberNomenclature
        {
            get { return errorNumberNomenclature; }
            set
            {
                if (errorNumberNomenclature != value)
                {
                    errorNumberNomenclature = value;
                    RaisePropertyChanged("ErrorNumberNomenclature");
                }
            }
        }

        private string letterNomenclature;
        public string LetterNomenclature
        {
            get { return letterNomenclature; }
            set
            {
                if (letterNomenclature != value)
                {
                    letterNomenclature = value;
                    RaisePropertyChanged("LetterNomenclature");
                    CreateDetailAddress();
                }
            }
        }

        private Quadrant quadrantGeneratingSource;
        public Quadrant QuadrantGeneratingSource
        {
            get { return quadrantGeneratingSource; }
            set
            {
                if (quadrantGeneratingSource != value)
                {
                    quadrantGeneratingSource = value;
                    RaisePropertyChanged("QuadrantGeneratingSource");
                    CreateDetailAddress();
                }
            }
        }

        private string errorCountry;
        public string ErrorCountry
        {
            get { return errorCountry; }
            set
            {
                if (errorCountry != value)
                {
                    errorCountry = value;
                    RaisePropertyChanged("ErrorCountry");
                }
            }
        }

        private void CreateDetailAddress()
        {
            Direction =
            string.Format(
                    "{0} {1}{2} {3} # {4}{5} {6} {7}",
                    ViaSelected != null ? ViaSelected.Name : string.Empty,
                    NumberMainRoad,
                    LetterMainRoad,
                    QuadrantMainRoad != null ? QuadrantMainRoad.Name : string.Empty,
                    NumberNomenclature,
                    LetterNomenclature,
                    QuadrantGeneratingSource != null ? QuadrantGeneratingSource.Name : string.Empty,
                    DoorNumber);

            StreetSO = string.Format(
                    "{0} {1}{2}{3} {4}{5}{6} {7}",
                    ViaSelected != null ? ViaSelected.Id : string.Empty,
                    NumberMainRoad,
                    LetterMainRoad,
                    QuadrantMainRoad != null ? QuadrantMainRoad.Name : string.Empty,
                    NumberNomenclature,
                    LetterNomenclature,
                    QuadrantGeneratingSource != null ? QuadrantGeneratingSource.Name : string.Empty,
                    DoorNumber);
        }

        public ICommand SaveAddressDetailCommand { get { return new RelayCommand(SaveAddressDetail); } }

        public ICommand CancelAddressDetailCommand { get { return new RelayCommand(CancelAddressDetail); } }

        private async void CancelAddressDetail()
        {
            Direction = string.Empty;
            StreetSO = string.Empty;
            await navigationService.Back();

        }

        private async void SaveAddressDetail()
        {
            if (ValidateAddressDetail())
            {
                await navigationService.Back();
            }
            else
            {
                await dialogService.ShowMessage(AppResources.TitleRequestRervice, AppResources.CompleteData);
            }
        }

        private bool ValidateAddressDetail()
        {
            ErrorMainRoadType = ViaSelected == null ? AppResources.ErrorMainRoadType : string.Empty;
            ErrorNumberMainRoad = string.IsNullOrEmpty(NumberMainRoad) ? AppResources.ErrorNumberMainRoad : string.Empty;
            ErrorNumberNomenclature = string.IsNullOrEmpty(NumberNomenclature) ? AppResources.ErrorNumberNomenclature : string.Empty;
            ErrorDoorNumber = string.IsNullOrEmpty(DoorNumber) ? AppResources.ErrorDoorNumber : string.Empty;

            return
                string.IsNullOrEmpty(ErrorMainRoadType) &&
                string.IsNullOrEmpty(ErrorNumberMainRoad) &&
                string.IsNullOrEmpty(ErrorNumberNomenclature) &&
                string.IsNullOrEmpty(ErrorDoorNumber);
        }

        public AddressViewModel()
        {
            dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }
    }
}
