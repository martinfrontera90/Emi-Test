namespace Emi.Portal.Movil.Pages.Services
{
    using System;
    using System.Reflection;
    using CommonServiceLocator;
    using Emi.Portal.Movil.Logic;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Emi.Portal.Movil.Logic.Enumerations;
    using Emi.Portal.Movil.Logic.Models.Requests;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.VideoCall;
    using Newtonsoft.Json;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMedicalVideoCallPage : ContentPage
    {
        IDialogService dialogService;
        VideoCallPageViewModel videoCallPageViewModel;
        RequestMedicalCallOpentok requestMedicalCall = new RequestMedicalCallOpentok();

        public HomeMedicalVideoCallPage()
        {
            try {
                InitializeComponent();
                if (Device.iOS == Device.RuntimePlatform)
                {
                    NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
                }

                videoCallPageViewModel = (VideoCallPageViewModel)BindingContext;
                var assembly = typeof(App).GetTypeInfo().Assembly;
                var stream = assembly.GetManifestResourceStream("Emi.Portal.Movil.Audio.ring_patient.mp3");
                videoCallPageViewModel.Ringtone = stream;
                dialogService = ServiceLocator.Current.GetInstance<IDialogService>();
                videoCallPageViewModel.VidoCallText = AppResources.WaitPlease;
                videoCallPageViewModel.IsMessageCalled = true;
                videoCallPageViewModel.IsWaiting = true;
            }
            catch(Exception e)
            {
            }
            

            //MessageJustifyLayout.Children.Add(videoCallPageViewModel.HtmlSourceText);

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is IMedicalVideoCallViewModel viewModel)
                viewModel.RemovePage();
        }

        private void OnEndCall(object sender, EventArgs e)
        {
            if (BindingContext is IMedicalVideoCallViewModel viewModel)
                viewModel.EndCallCommand.Execute(null);
        }
    }
}