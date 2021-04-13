using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonServiceLocator;
using Emi.Portal.Movil.Logic.Contracts.Domain;
using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
using Emi.Portal.Movil.Logic.Contracts.Services;
using Emi.Portal.Movil.Logic.Models.Domain;
using Emi.Portal.Movil.Logic.Models.Requests;
using Emi.Portal.Movil.Logic.Models.Responses;
using Emi.Portal.Movil.Logic.Resources;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Emi.Portal.Movil.Logic.ViewModels.Pages.VideoCall
{
    public class SurveyQueuingViewModel : ViewModelBase, ISurveyQueuingViewModel
    {
        readonly IDialogService dialogService;

        readonly INavigationService navigationService;

        readonly IApiService apiService;

        readonly IQueingFirebaseService queingFirebase;

        public RequestMedicalService requestMedicalService { get; set; }

        public PostPatientServiceTypeResponse patient { get; set; }

        List<ReasonsAbandonment> reasons;
        public List<ReasonsAbandonment> Reasons
        {
            get { return reasons; }
            set
            {
                if (reasons != value)
                {
                    reasons = value;
                    RaisePropertyChanged();
                }
            }
        }

        ReasonsAbandonment reasonSelect;
        public ReasonsAbandonment ReasonSelect
        {
            get { return reasonSelect; }
            set
            {
                if (reasonSelect != value)
                {
                    reasonSelect = value;
                    if (Reasons != null)
                    {
                        foreach (var item in Reasons)
                        {
                            item.IsVisible = false;
                            if (item.Type == "O" && value.Type == "O")
                            {
                                item.IsVisible = true;
                            }
                        }
                    }
                }
            }
        }

        public ICommand SendReasonCommand { get { return new RelayCommand(async () => await SendReason(true)); } }

        public async Task SendReason(bool showModal)
        {
            if (ReasonSelect != null)
            {
                ReasonSelect.ErrorOther = string.Empty;
                if (ReasonSelect.Type == "O" && ValidateData() == false)
                {
                    return;
                }

                queingFirebase.GetOutSesionWaitingRoom(patient.PatientServiceType, requestMedicalService.PatientDocument);

                dialogService.ShowProgress();
                requestMedicalService.Controller = AppConfigurations.VideoCallController;
                requestMedicalService.Action = AppConfigurations.PostIntent;
                requestMedicalService.ReasonsAbandonmentId = int.Parse(ReasonSelect.Code);
                requestMedicalService.ReasonsAbandonmentComment = ReasonSelect.Comment;
                PostIntentResponse response = await apiService.PostIntent(requestMedicalService);
                dialogService.HideProgress();
                if (showModal)
                {
                    await navigationService.BackModal();

                    await navigationService.BackToRoot();
                    await navigationService.Navigate(Enumerations.AppPages.ServicesPage);
                }
                else
                {
                    await navigationService.Back();
                }
            }
        }

        public ICommand CancelarCommand { get { return new RelayCommand(async () => await Cancel()); } }

        public async Task Cancel()
        {
            await navigationService.BackModal();
        }

        public ICommand CallCategoryCommand
        {
            get
            {
                return new RelayCommand(CallCategory);
            }
        }

        void CallCategory()
        {
            ICallViewModel callViewModel = ServiceLocator.Current.GetInstance<ICallViewModel>();
            callViewModel.CallCategory();
        }

        public SurveyQueuingViewModel(IDialogService dialogService,
            INavigationService navigationService,
            IApiService apiService,
            IQueingFirebaseService queingFirebaseService)
        {
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.apiService = apiService;
            this.queingFirebase = queingFirebaseService;
        }

        public async Task LoadPage(List<ReasonsAbandonment> reasons)
        {
            Reasons = reasons.Where(x => x.Type == "U" || x.Type == "O")
                .Select(x => new ReasonsAbandonment
                {
                    Code = x.Code,
                    IsVisible = false,
                    Name = x.Name,
                    Type = x.Type,
                    Comment = x.Comment,
                    Description = x.Description
                }).ToList();
            await navigationService.ShowModal(Enumerations.AppPages.SurveyQueuingPage);
        }

        private bool ValidateData()
        {
            ReasonSelect.ErrorOther = string.IsNullOrEmpty(ReasonSelect.Comment) ? AppResources.FieldRequired : string.Empty;

            return string.IsNullOrEmpty(ReasonSelect.ErrorOther);
        }

        public void RemovePage() => navigationService.RemovePage(1);

    }
}
