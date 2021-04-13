namespace Emi.Portal.Movil.Logic.ViewModels.Domain
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
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

    public class ServicesEnabledViewModel : ViewModelBase, IServicesEnabledViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        IPhoneService phoneService;
        INavigationService navigationService;
        IServicesPageViewModel servicesPageViewModel;

        private Message message;
        public Message Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    RaisePropertyChanged("Message");
                }
            }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                if (code != value)
                {
                    code = value;
                    RaisePropertyChanged("Code");
                }
            }
        }

        private string estimatedTimeText;
        public string EstimatedTimeText
        {
            get { return estimatedTimeText; }
            set
            {
                if (estimatedTimeText != value)
                {
                    estimatedTimeText = value;
                    RaisePropertyChanged("EstimatedTimeText");
                }
            }
        }

        private string icon;
        public string Icon
        {
            get { return icon; }
            set
            {
                if (icon != value)
                {
                    icon = value;
                    RaisePropertyChanged("Icon");
                }
            }
        }

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

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        private ServiceType serviceType;
        public ServiceType ServiceType
        {
            get { return serviceType; }
            set
            {
                if (serviceType != value)
                {
                    serviceType = value;
                    RaisePropertyChanged("ServiceType");
                }
            }
        }

        private string estimatedTime;
        public string EstimatedTime
        {
            get { return estimatedTime; }
            set
            {
                if (estimatedTime != value)
                {
                    estimatedTime = value;
                    RaisePropertyChanged("EstimatedTime");
                }
            }
        }

        public ICommand RequestServiceCommand { get { return new RelayCommand(GetExistsMedicalHomeService); } }

        //private async Task<bool> ValidatePediatricServices()
        //{
        //    try
        //    {
        //        dialogService.ShowProgress();
        //        var request = new Request
        //        {
        //            Controller = "Services",
        //            Action = "ValidatePediatricServices",
        //            Document = servicesPageViewModel.PersonSelected.Document,
        //            DocumentType = servicesPageViewModel.PersonSelected.DocumentType
        //        };
        //        var response = await apiService.ValidatePediatricServices(request);
        //        dialogService.HideProgress();
        //        if (response.Success)
        //        {
        //            if (response.ValidatePediatricServicesResponse.StatusRequest)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                if (await dialogService.ShowConfirm("", response.ValidatePediatricServicesResponse.Response))
        //                {
        //                    await navigationService.Navigate(AppPages.ScheduledServicesPage);
        //                }
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            await dialogService.ShowMessage(response.Title, response.Message);
        //            return false;
        //        }
                
        //    }
        //    catch (Exception e)
        //    {
        //        dialogService.HideProgress();
        //        ServiceLocator.Current.GetInstance<IExceptionService>().RegisterException(e);
        //        return false;
        //    }
        //}

        public async void GetExistsMedicalHomeService()
        {
            servicesPageViewModel.ServiceSelected = this;
            servicesPageViewModel.DynamicTitlePage = this.Name;

            if (servicesPageViewModel.UserYoung.resultUserYoung && this.ServiceType == ServiceType.MedicalOrientationChat)
                servicesPageViewModel.DynamicTitlePage = "Chat de Orientación Médica Pediátrica";


            //if (ServiceType == ServiceType.SchedulePediatricVideoCall || ServiceType == ServiceType.PediatricVideoCall)
            //{
            //    if (!await ValidatePediatricServices())
            //        return;
            //}

            //if (ServiceType == ServiceType.SchedulePediatricVideoCall)
            //{
            //    IServicesPageViewModel servicesPage = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();
            //    servicesPage.LoadAgendas();
            //    return;
            //}

            if (ServiceType == ServiceType.HomeCare && servicesPageViewModel.AddressSelected.Coverage == false)
            {
                await dialogService.ShowMessage(Name, AppResources.AddressOutsideTheCoverageArea);
                return;
            }

            //if (ServiceType == ServiceType.Pediatric)
            //{
                
            //    await navigationService.Navigate(AppPages.PediatricPage);
            //    return;
            //}

            dialogService.ShowProgress();
            RequestExistsMedicalHomeService request = new RequestExistsMedicalHomeService
            {
                Document = servicesPageViewModel.PersonSelected.Document,
                DocumentType = servicesPageViewModel.PersonSelected.DocumentType,
                IdReference = servicesPageViewModel.PersonSelected.IdReference,
            };

            ResponseExistsMedicalHomeService response = await apiService.GetExistsMedicalHomeService(request);

            dialogService.HideProgress();

            if (response.ExistsMedicalHomeServiceResponse.CurrentService && Code == "01")
            {
                if (await dialogService.ShowConfirm("", $"Actualmente hay un servicio de:\n{response.ExistsMedicalHomeServiceResponse.ServiceTypeDescription} en curso para {servicesPageViewModel.PersonSelected.FullNames}\n¿Deseas conocer su estado?"))
                {
                    await navigationService.Navigate(AppPages.ScheduledServicesPage);
                }
                return;
            }

            RequestService();
        }

        private async void RequestService()
        {
            if (Message != null && Message.Code != 0)
            {
                await dialogService.ShowMessage(Message.Title, Message.Text);
                return;
            }


            if (ServiceType == ServiceType.Coordination || ServiceType == ServiceType.Urgency)
            {
                INearbyClinicsPageViewModel nearbyClinicsPageViewModel = ServiceLocator.Current.GetInstance<INearbyClinicsPageViewModel>();
                IMedicalCenterCoordinationPageViewModel medicalCenter = ServiceLocator.Current.GetInstance<IMedicalCenterCoordinationPageViewModel>();

                switch (ServiceType)
                {
                    case ServiceType.Coordination:
                        INewMedicalCenterCoordinationPageViewModel newMedicalCenterCoordinationPageView = ServiceLocator.Current.GetInstance<INewMedicalCenterCoordinationPageViewModel>();
                        newMedicalCenterCoordinationPageView.PersonSelected = servicesPageViewModel.PersonSelected;

                        if (string.IsNullOrEmpty(servicesPageViewModel.PersonSelected.Email) || string.IsNullOrEmpty(servicesPageViewModel.PersonSelected.CellPhone))
                        {
                            await navigationService.Navigate(AppPages.AditionalDataPage);
                            return;
                        }

                        nearbyClinicsPageViewModel.CurrentLocation = null;
                        medicalCenter.IsVisibleCoordination = true;

                        await navigationService.Navigate(AppPages.MedicalCenterCoordinationPage);
                        return;

                    default:

                        medicalCenter.IsVisibleCoordination = false;
                        nearbyClinicsPageViewModel.IsEmergency= true;
                        await navigationService.Navigate(AppPages.NearbyClinicsPage);
                        return;
                }
            }

            servicesPageViewModel.ServiceSelected = this;

            await navigationService.Navigate(AppPages.HomeMedicalCarePage);
            await Task.Delay(1000);
            servicesPageViewModel.ReadyLocation = true;
        }

        public ServicesEnabledViewModel()
        {
            Message = new Message();
            apiService = ServiceLocator.Current.GetInstance<IApiService>();
            dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
            navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            phoneService = ServiceLocator.Current.GetInstance<IPhoneService>();
            servicesPageViewModel = ServiceLocator.Current.GetInstance<IServicesPageViewModel>();
        }
    }
}
