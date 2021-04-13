namespace Emi.Portal.Movil.Logic.ViewModels.Pages.QualifyServices
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.QualifyServices;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Helpers;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using CommonServiceLocator;

    public class QualifyServicesPageViewModel : ViewModelBase, IQualifyServicesPageViewModel
    {
        IApiService apiService;
        IDialogService dialogService;
        INavigationService navigationService;
        IValidatorService validatorService;

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

        private ObservableCollection<QuestionViewModel> questions;
        public ObservableCollection<QuestionViewModel> Questions
        {
            get { return questions; }
            set
            {
                if (questions != value)
                {
                    questions = value;
                    RaisePropertyChanged("Questions");
                }
            }
        }

        private string serviceName;
        public string ServiceName
        {
            get { return serviceName; }
            set
            {
                if (serviceName != value)
                {
                    serviceName = value;
                    RaisePropertyChanged("ServiceName");
                }
            }
        }


        public ICommand SendCalificateCommand { get { return new RelayCommand(SendCalificate); } }

        private async void SendCalificate()
        {
            dialogService.ShowProgress();
            List<ServiceQualification> responseQuestions = new List<ServiceQualification>();

            foreach (QuestionViewModel item in Questions)
            {
                string Value = string.Empty;
                switch (item.QuestionType)
                {
                    case QuestionType.OneTen:
                        Value = item.ValueOneTen.ToString();
                        break;
                    case QuestionType.OneFive:
                        Value = item.ValueOneFive.ToString();
                        break;
                    case QuestionType.YesNo:
                        Value = item.IsTogglesYesNo.ToString();
                        break;
                }
                ServiceQualification service = new ServiceQualification
                {
                    Code = item.Code,
                    Value = Value
                };
                responseQuestions.Add(service);
            }

            RequestQualifyQuestion request = new RequestQualifyQuestion
            {
                ServiceQualification = responseQuestions
            };
            ResponseQualifyQuestion response = await apiService.QualifyQuestion(request);
            dialogService.HideProgress();
            ValidateResponseQualifyQuestion(response);
        }

        private async void ValidateResponseQualifyQuestion(ResponseQualifyQuestion response)
        {
            await dialogService.ShowMessage(response.Title, response.Message);
            if (response.Success && response.StatusCode == 0)
            {
                await navigationService.Navigate(AppPages.LandingPage);
            }
        }

        public async Task LoadCalificate(string code)
        {
            dialogService.ShowProgress();
            RequestServiceQualify request = new RequestServiceQualify
            {
                Code = code,
                User = ServiceLocator.Current.GetInstance<ILoginViewModel>().User.UserName
            };
            ResponseServiceQualify response = await apiService.GetServiceQualify(request);
            dialogService.HideProgress();
            ValidateResponseServiceQualify(response);
        }

        private async void ValidateResponseServiceQualify(ResponseServiceQualify response)
        {
            Questions = new ObservableCollection<QuestionViewModel>();

            if (await validatorService.ValidateResponse(response) == false)
            {
                await navigationService.BackToRoot();
                return;
            }

            ServiceName = response.ServiceQualify.ServiceName;
            foreach (Question item in response.ServiceQualify.Questions)
            {
                QuestionViewModel questionViewModel = new QuestionViewModel();
                ViewModelHelper.SetQuestionToQuestionViewModel(questionViewModel, item);
                Questions.Add(questionViewModel);
            }
        }

        public QualifyServicesPageViewModel(IApiService apiService, IDialogService dialogService, INavigationService navigationService, IValidatorService validatorService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.validatorService = validatorService;

            Questions = new ObservableCollection<QuestionViewModel>();
        }
    }
}
