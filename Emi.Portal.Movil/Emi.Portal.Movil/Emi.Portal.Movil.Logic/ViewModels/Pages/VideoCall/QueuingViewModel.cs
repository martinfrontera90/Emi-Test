namespace Emi.Portal.Movil.Logic.ViewModels.Pages.VideoCall
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic.Contracts.Domain;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels;
    using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Domain;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Models.Responses;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.VideoCall;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Xamarin.Forms;

    public class QueuingViewModel : ViewModelBase, IQueuingViewModel
    {
        readonly IDialogService dialogService;

        readonly INavigationService navigationService;

        readonly IApiService apiService;

        readonly IQueingFirebaseService queingFirebase;

        public RequestMedicalService requestMedicalService { get; set; }

        RealTimeConfigurationResponse realTimeConfiguration;

        RequestMedicalCallOpentok requestMedicalCall = new RequestMedicalCallOpentok();

        List<ReasonsAbandonment> AllReasons { get; set; }

        string textQueuing;

        public string TextQueuing
        {
            get => textQueuing;
            set
            {
                textQueuing = value;
                RaisePropertyChanged();
            }
        }

        string messageOpentok;
        public string MessageOpentok
        {
            get { return messageOpentok; }
            set
            {
                if (messageOpentok != value)
                {
                    messageOpentok = value;
                    RaisePropertyChanged();
                }
            }
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

        ISurveyQueuingViewModel surveyQueuing = ServiceLocator.Current.GetInstance<ISurveyQueuingViewModel>();

        public QueuingViewModel(IDialogService dialogService,
            INavigationService navigationService,
            IApiService apiService,
            IQueingFirebaseService queingFirebase)
        {
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.apiService = apiService;
            this.queingFirebase = queingFirebase;
            OldHasDoctor = true;
        }

        private PatientRealTime patientRealTime { get; set; }

        public PostPatientServiceTypeResponse patient { get; set; }

        public bool Qualify { get; set; } = false;

        public int? OldPosition { get; set; }

        public bool OldHasDoctor { get; set; } = true;

        public async Task AllInOrder()
        {
            try
            {
                IMedicalVideoCallViewModel _medicalVideoCall = ServiceLocator.Current.GetInstance<IMedicalVideoCallViewModel>();
                _medicalVideoCall.LoadCallHistoryId(null);

                dialogService.ShowProgress();
                ReasonsAbandonmentResponse response = await apiService.GetReasonsAbandonment();
                dialogService.HideProgress();

                if (response.Success)
                {
                    AllReasons = response.DataList;
                }

                TextQueuing = string.Empty;
                dialogService.ShowProgress();
                realTimeConfiguration = await apiService.GetFirebaseConfiguration();
                dialogService.HideProgress();
                queingFirebase.InitializeApp(realTimeConfiguration);
                dialogService.ShowProgress();
                patient = await apiService.PostPatientServiceType(new RequestPostPatientServiceType
                {
                    DocumentType = requestMedicalService.PatientDocumentType,
                    Document = requestMedicalService.PatientDocument
                });

                dialogService.HideProgress();

                queingFirebase.Connect();

                OldHasDoctor = true;
                ValideDoctor = true;

                SubscribeQueuing();
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    surveyQueuing.ReasonSelect = AllReasons.First(x => x.Type == "C");
                    surveyQueuing.requestMedicalService = requestMedicalService;
                    surveyQueuing.patient = patient;
                    await surveyQueuing.SendReason(false);
                });
            }
        }

        public bool ValideDoctor { get; set; } = true;

        private void SubscribeQueuing()
        {
            queingFirebase.GetDoctors(patient.PatientServiceType);
            MessagingCenter.Subscribe<string, string>(this, queingFirebase.GetDoctorKey(), async (sender, args) =>
            {
                var HasDoctor = int.TryParse(args, out int result);
                if (HasDoctor == false || result == 0)
                {
                    if (OldHasDoctor != HasDoctor)
                    {
                        if (ValideDoctor)
                        {
                            ValideDoctor = false;
                            SubscribeQueuing();
                            return;
                        }

                        OldHasDoctor = HasDoctor;

                        MessagingCenter.Unsubscribe<string, string>(this, queingFirebase.GetDoctorKey());
                        await dialogService.ShowMessage(AppResources.TitleDoctorNotAvailable, AppResources.DoctorNotAvailable);
                        surveyQueuing.ReasonSelect = new ReasonsAbandonment
                        {
                            Type = "O",
                            Code = AllReasons.First(x => x.Type == "O").Code,
                            Comment = "No hay médicos disponibles"
                        };
                        surveyQueuing.requestMedicalService = requestMedicalService;
                        surveyQueuing.patient = patient;
                        Qualify = true;
                        await surveyQueuing.SendReason(false);
                    }
                }
                else
                {
                    queingFirebase.LoadSesionWaitingRoom(patient.PatientServiceType, requestMedicalService.PatientDocument);
                    MessagingCenter.Subscribe<string, PatientRealTime>(this, queingFirebase.GetMessageKey(), async (sender2, args2) =>
                    {
                        OldHasDoctor = true;
                        ValideDoctor = true;

                        if (args2 != null)
                        {
                            if (OldPosition != args2?.Position)
                            {
                                OldPosition = args2?.Position;

                                if (args2?.Position == 0 && args2?.Room != null)
                                {
                                    queingFirebase.GetOutSesionWaitingRoom(patient.PatientServiceType, requestMedicalService.PatientDocument);
                                    patientRealTime = args2;

                                    requestMedicalService.Controller = AppConfigurations.AffiliateController;
                                    requestMedicalService.Action = AppConfigurations.GetOpenTokDataForAffiliate;
                                    requestMedicalService.DoctorId = (int)patientRealTime.Doctor;
                                    requestMedicalService.IdSessionTokbox = patientRealTime.Room;

                                    var responseOpenTokClient = await apiService.GetOpenTokDataForAffiliate(requestMedicalService);
                                    if (responseOpenTokClient.StatusCode == 0)
                                    {
                                        requestMedicalCall.type = (int)CallStatus.Waiting;

                                        CrossOpenTok.Current.ApiKey = responseOpenTokClient.ApiKey.ToString(); // keys.ApiKey;
                                        CrossOpenTok.Current.SessionId = responseOpenTokClient.IdSessionTokbox; // keys.SessionId;
                                        CrossOpenTok.Current.UserToken = responseOpenTokClient.OpenTokToken; // keys.Token;

                                        MessageOpentok = JsonConvert.SerializeObject(requestMedicalCall);
                                        MessagingCenter.Unsubscribe<string, string>(this, queingFirebase.GetMessageKey());

                                        if (!CrossOpenTok.Current.TryStartSession(false))
                                        {
                                            await navigationService.Back();
                                            return;
                                        }

                                        await navigationService.Navigate(AppPages.HomeMedicalVideoCall);
                                    }
                                    else
                                    {
                                        await dialogService.ShowMessage(AppResources.VideoCallTitle, responseOpenTokClient.Message);
                                        await navigationService.Back();
                                    }
                                }
                                else
                                {
                                    TextQueuing = args2?.Position == 1 ? AppResources.NextAtetion : $"{AppResources.NumberAtetion}{args2?.Position}";
                                }
                            }
                        }
                    });
                }
            });
        }

        public ICommand GetOutCommand { get { return new RelayCommand(async () => GetOutSesion()); } }

        public async Task GetOutSesion()
        {
            surveyQueuing.requestMedicalService = requestMedicalService;
            surveyQueuing.patient = patient;
            Qualify = true;
            await surveyQueuing.LoadPage(AllReasons);
        }

        public void SendMessage()
        {
            _ = CrossOpenTok.Current.SendMessageAsync(MessageOpentok, "call", false);
        }

        public void LoadRequestQueuing(RequestMedicalService request, RequestMedicalCallOpentok requestMedicalCall)
        {
            requestMedicalService = request;
            this.requestMedicalCall = requestMedicalCall;
        }

        public void RemovePage() => navigationService.RemovePage(1);
    }
}
