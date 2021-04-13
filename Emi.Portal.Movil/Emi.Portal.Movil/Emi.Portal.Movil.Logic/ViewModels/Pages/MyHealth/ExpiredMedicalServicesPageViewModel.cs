namespace Emi.Portal.Movil.Logic.ViewModels.Pages.MyHealth
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MyHealth;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class ExpiredMedicalServicesPageViewModel : ViewModelBase, IExpiredMedicalServicesPageViewModel
    {
        IApiService apiService;
        ILoginViewModel loginViewModel;
        IDialogService dialogService;
        INavigationService navigationService;

        private ObservableCollection<ProductExpired> ProductsSaved;

        private ObservableCollection<PersonViewModel> People;

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

        private bool isVisibleEmpty;
        public bool IsVisibleEmpty
        {
            get { return isVisibleEmpty; }
            set
            {
                isVisibleEmpty = value;
                RaisePropertyChanged(nameof(IsVisibleEmpty));
            }
        }

        private bool isVisiblePatient;
        public bool IsVisiblePatient
        {
            get { return isVisiblePatient; }
            set
            {
                isVisiblePatient = value;
                RaisePropertyChanged(nameof(IsVisiblePatient));
            }
        }


        private ObservableCollection<ProductExpired> products;
        public ObservableCollection<ProductExpired> Products
        {
            get { return products; }
            set
            {
                products = value;
                RaisePropertyChanged(nameof(Products));
            }
        }

        private ObservableCollection<ModelBase> patients;
        public ObservableCollection<ModelBase> Patients
        {
            get { return patients; }
            set
            {
                patients = value;
                RaisePropertyChanged(nameof(Patients));
            }
        }

        private ModelBase patientSelected;
        public ModelBase PatientSelected
        {
            get { return patientSelected; }
            set
            {
                if (patientSelected != value)
                {
                    patientSelected = value;
                    RaisePropertyChanged(nameof(PatientSelected));
                    if(PatientSelected!=null)
                        SearchPatient();
                }


            }
        }

        private ObservableCollection<ModelBase> statuses;
        public ObservableCollection<ModelBase> Statuses
        {
            get { return statuses; }
            set
            {
                statuses = value;
                RaisePropertyChanged(nameof(Statuses));
            }
        }

        private ModelBase statusSelected;
        public ModelBase StatusSelected
        {
            get { return statusSelected; }
            set
            {
                if (statusSelected != value)
                {
                    statusSelected = value;
                    RaisePropertyChanged(nameof(StatusSelected));
                    if(statusSelected!=null)
                        SearchPatient();
                }

            }
        }

        public ICommand ClearFiltersCommand { get { return new RelayCommand(ClearFilters); } }
        public ICommand SearchCommand { get { return new RelayCommand(SearchPatient); } }
        public ICommand CoordinateCommand { get { return new RelayCommand<ProductExpired>(Coordinate); } }

        private async void Coordinate(ProductExpired product)
        {
            switch (product.Coordinate)
            {

                case "1":
                    INewMedicalCenterCoordinationPageViewModel newMedicalCenter = ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
                    foreach (var person in People)
                    {
                        if (person.Document == product.Document)
                        {
                            newMedicalCenter.PersonSelected = person;
                        }
                    }
                    await navigationService.Navigate(AppPages.NewMedicalCenterCoordinationPage);
                    break;
                case "2":
                    if (await dialogService.ShowConfirm("", "Concurre al Centro Médico de tu preferencia en el horario estipulado. O comunícate con nuestro chat de Servicio al Cliente.", "Chat", "Cerrar"))
                        await navigationService.Navigate(AppPages.ChatCustomerServicePage);

                    break;
                default:
                    break;
            }
        }

        public async void LoadPeople()
        {
            try
            {
                RequestPeople request = new RequestPeople
                {
                    IdReference = loginViewModel.User.IdReference,
                    Document = loginViewModel.User.Document,
                    DocumentType = loginViewModel.User.DocumentType
                };
                ResponseBeneficiaries response = await apiService.GetPersons(request);
                foreach (var person in response.Beneficiaries)
                {
                    PersonViewModel personViewModel = new PersonViewModel();
                    ViewModelHelper.SetPersonToPersonViewModel(personViewModel, person);
                    People.Add(personViewModel);
                }
            }
            catch (Exception e)
            {
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }

        }

        public async void SearchPatient()
        {
            try
            {
                Products = new ObservableCollection<ProductExpired>();
                dialogService.ShowProgress();
                if (PatientSelected == null && loginViewModel.User.AffiliateType == AffiliateType.ResponsiblePayment)
                {
                    dialogService.HideProgress();
                    await dialogService.ShowMessage("", "Seleccione un paciente");
                    return;
                }
                else
                { 
                    RequestExpiredMedicalServices request = new RequestExpiredMedicalServices
                    {
                        Action = "GetExpiredProducts",
                        Controller = "expiredmedicalservices",
                        Document = patientSelected.Code,
                        DocumentType = PatientSelected.Description,
                        Type = StatusSelected != null ? StatusSelected.Code : "1"
                    };
                    ResponseExpiredMedicalServices response = await apiService.GetExpiredProducts(request);
                    dialogService.HideProgress();
                    if (response.ExpiredProducts.Products != null)
                        if (response.ExpiredProducts.Products.Count > 0)
                        {
                            Products = new ObservableCollection<ProductExpired>(response.ExpiredProducts.Products);
                            IsVisibleEmpty = false;
                            return;
                        }
                    IsVisibleEmpty = true;
                }
            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }


        }

        private void ClearFilters()
        {
            StatusSelected = new ModelBase();
            PatientSelected = new ModelBase();
            Products = ProductsSaved;
        }

        public void FillList()
        {
            Statuses = new ObservableCollection<ModelBase>
            {
                new ModelBase
                {
                    Code = "1",
                    Name = "Todos"
                },
                new ModelBase
                {
                    Code = "2",
                    Name = "Vencido"
                },
                new ModelBase
                {
                    Code = "3",
                    Name = "Vigente"
                }
            };
        }



        public async void LoadData()
        {
            try
            {
                dialogService.ShowProgress();
                Clean();
                if (loginViewModel.User.AffiliateType == AffiliateType.ResponsiblePayment)
                {
                    RequestBeneficiaries request = new RequestBeneficiaries
                    {
                        Document = loginViewModel.User.Document,
                        DocumentType = loginViewModel.User.DocumentType
                    };
                    var response = await apiService.GetBeneficiaries(request);
                    if (response.Success)
                    {
                        
                        foreach (var beneficiary in response.Beneficiaries)
                        {
                            Patients.Add(new ModelBase
                            {
                                Code = beneficiary.Document,
                                Name = beneficiary.FullNames,
                                Description = beneficiary.DocumentType

                            });
                        }
                    }
                }
                else
                {
                    PatientSelected = new ModelBase
                    {
                        Code = loginViewModel.User.Document,
                        Description = loginViewModel.User.DocumentType
                    };
                    SearchPatient();
                    IsVisiblePatient = false;
                }
                LoadPeople();
                FillList();
                dialogService.HideProgress();
                await navigationService.Navigate(AppPages.ExpiredMedicalServices);
            }
            catch (Exception e)
            {
                dialogService.HideProgress();
                ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
            }
        }

        private void Clean()
        {
            try
            {
                Products = new ObservableCollection<ProductExpired>();
                Patients = new ObservableCollection<ModelBase>();
                PatientSelected = null;
                StatusSelected = null;
                IsVisibleEmpty = false;
                People = new ObservableCollection<PersonViewModel>();
                IsVisiblePatient = true;
            }
            catch (Exception e)
            {

            }
        }


        public ExpiredMedicalServicesPageViewModel(IApiService apiService, ILoginViewModel loginViewModel, IDialogService dialogService, INavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.loginViewModel = loginViewModel;
            this.apiService = apiService;
        }
    }
}
