namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;
    using Plugin.Permissions.Abstractions;

    public class CoordinationViewModel : ViewModelBase, ICoordinationViewModel, ICallMedicalCenterViewModel
    {
        #region Properties
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IPhoneService phoneService;
        IPermissionService permissionService;

        private string agendaName;
        public string AgendaName
        {
            get { return agendaName; }
            set
            {
                if (agendaName != value)
                {
                    agendaName = value;
                    RaisePropertyChanged("AgendaName");
                }
            }
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private string typeCoordination;
        public string TypeCoordination
        {
            get { return typeCoordination; }
            set
            {
                if (typeCoordination != value)
                {
                    typeCoordination = value;
                    RaisePropertyChanged("TypeCoordination");
                }
            }
        }

        private string specialityCode;
        public string SpecialityCode
        {
            get { return specialityCode; }
            set
            {
                if (specialityCode != value)
                {
                    specialityCode = value;
                    RaisePropertyChanged("SpecialityCode");
                }
            }
        }

        private string nameSpecialty;
        public string NameSpecialty
        {
            get { return nameSpecialty; }
            set
            {
                if (nameSpecialty != value)
                {
                    nameSpecialty = value;
                    RaisePropertyChanged("NameSpecialty");
                }
            }
        }

        private string clinicCode;
        public string ClinicCode
        {
            get { return clinicCode; }
            set
            {
                if (clinicCode != value)
                {
                    clinicCode = value;
                    RaisePropertyChanged("ClinicCode");
                }
            }
        }

        private string clinicName;
        public string ClinicName
        {
            get { return clinicName; }
            set
            {
                if (clinicName != value)
                {
                    clinicName = value;
                    RaisePropertyChanged("ClinicName");
                }
            }
        }

        private string _RDACode;
        public string RDACode
        {
            get { return _RDACode; }
            set
            {
                if (_RDACode != value)
                {
                    _RDACode = value;
                    RaisePropertyChanged("RDACode");
                }
            }
        }

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

        private string hour;
        public string Hour
        {
            get { return hour; }
            set
            {
                if (hour != value)
                {
                    hour = value;
                    RaisePropertyChanged("Hour");
                }
            }
        }

        private string day;
        public string Day
        {
            get { return day; }
            set
            {
                if (day != value)
                {
                    day = value;
                    RaisePropertyChanged("Day");
                }
            }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set
            {
                if (names != value)
                {
                    names = value;
                    RaisePropertyChanged("Names");
                }
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }

        private object cost;
        public object Cost
        {
            get { return cost; }
            set
            {
                if (cost != value)
                {
                    cost = value;
                    RaisePropertyChanged("Cost");
                }
            }
        }

        private string fullAddress;
        public string FullAddress
        {
            get { return fullAddress; }
            set
            {
                if (fullAddress != value)
                {
                    fullAddress = value;
                    RaisePropertyChanged("FullAddress");
                }
            }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    RaisePropertyChanged("Date");
                }
            }
        }

        private string recommendations;
        public string Recommendations
        {
            get { return recommendations; }
            set
            {
                if (recommendations != value)
                {
                    recommendations = value;
                    RaisePropertyChanged("Recommendations");
                }
            }
        }

        public string YearMonthDay { get; set; }
        public string Time { get; set; }

        private string price;
        public string Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    RaisePropertyChanged("Price");
                }
            }
        }

        private string titleButton;
        public string TitleButton
        {
            get { return titleButton; }
            set
            {
                if (titleButton != value)
                {
                    titleButton = value;
                    RaisePropertyChanged("TitleButton");
                }
            }
        }

        private bool isVisibleRecommendation;
        public bool IsVisibleRecommendation
        {
            get { return isVisibleRecommendation; }
            set
            {
                if (isVisibleRecommendation != value)
                {
                    isVisibleRecommendation = value;
                    RaisePropertyChanged("IsVisibleRecommendation");
                }
            }
        }

        private bool isVisiblePay;
        public bool IsVisiblePay
        {
            get { return isVisiblePay; }
            set
            {
                if (isVisiblePay != value)
                {
                    isVisiblePay = value;
                    RaisePropertyChanged("IsVisiblePay");
                }
            }
        }


        #endregion

        public ICommand SelectCommand { get { return new RelayCommand(Select); } }
        public ICommand CancelCoordinationCommand { get { return new RelayCommand(CancelCoordination); } }
        public ICommand CallMedicalCenterCommand { get { return new RelayCommand(CallMedicalCenterLine); } }
        public ICommand CallCategoryCommand { get { return new RelayCommand(CallCategory); } }
        public ICommand ConfirmationCommand { get { return new RelayCommand(Confirmation); } }

        private void Confirmation()
        {
            INewMedicalCenterCoordinationPageViewModel newMedicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
            if (TitleButton == AppResources.TitleConfirmNewCoordination)
            {
                newMedicalCenterCoordinationPageViewModel.ConfirmNewCoordination();
            }
            else
            {
                newMedicalCenterCoordinationPageViewModel.GetPaymentMethods();
            }
        }

        private async void CancelCoordination()
        {
            if (await dialogService.ShowConfirm(AppResources.CancelCoordinationTittle, AppResources.CancelCoordination))
            {
                dialogService.ShowProgress();
                RequestCancelPendingCoordination requestCancelPendingCoordination = new RequestCancelPendingCoordination
                {
                    PendingCoordination = ThisPendingCoordination(),
                };
                ResponseCancelCoordination response = await apiService.CancelPendingCoordination(requestCancelPendingCoordination);
                dialogService.HideProgress();
                ValidateResponseCancelCoordination(response);
            }
        }

        private PendingCoordination ThisPendingCoordination()
        {
            ILoginViewModel loginViewModel = ServiceLocator.Current.GetInstance<ILoginViewModel>();
            return new PendingCoordination
            {
                AgendaType = TypeCoordination,
                Day = Day,
                Document = Document,
                Email = loginViewModel.User.UserName,
                MedicalCenter = new MedicalCenter { ClinicCode = ClinicCode },
                Phone = loginViewModel.User.CellPhone,
                RDACode = RDACode,
                SpecialityCode = SpecialityCode,                
                Time = Time,
                YearMonthDay = YearMonthDay,                
                UserEmail = loginViewModel.User.UserName,
                Price = Price
            };
        }

        private async void ValidateResponseCancelCoordination(ResponseCancelCoordination response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);
            if (response.Success && response.StatusCode == 0)
            {
                IMedicalCenterCoordinationPageViewModel medicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
                await medicalCenterCoordinationPageViewModel.LoadCoordinations();
                await navigationService.Back();
            }
        }

        private void Select()
        {
            IMedicalCenterCoordinationPageViewModel medicalCenterCoordinationPageViewModel = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();
            medicalCenterCoordinationPageViewModel.CoordinationSelected = this;
            navigationService.Navigate(AppPages.MedicalCenterCoordinationDetailPage);
        }

        public async void DeleteCoordination()
        {
            dialogService.ShowProgress();
            RequestCancelPendingCoordination requestCancelPendingCoordination = new RequestCancelPendingCoordination
            {
                PendingCoordination = ThisPendingCoordination(),
            };
            ResponseCancelCoordination response = await apiService.CancelPendingCoordination(requestCancelPendingCoordination);
            dialogService.HideProgress();
            ValidateResponseCancelCoordination(response);
        }

        public CoordinationViewModel()
        {
            apiService = ServiceLocator.Current.GetInstance<IApiService>();
            dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            phoneService = ServiceLocator.Current.GetInstance<IPhoneService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionService>();
            IsVisiblePay = false;
            IsVisibleRecommendation = true;
        }

        private async void CallCategory()
        {
            if (await permissionService.CheckPermissions(Permission.Phone))
            {
                ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
                callViewModel.CallCategory();
            }
        }

        private async void CallMedicalCenterLine()
        {
            if (await permissionService.CheckPermissions(Permission.Phone))
            {
                phoneService.Call(AppConfigurations.MedicalCenterLine);
            }
        }
    }
}
