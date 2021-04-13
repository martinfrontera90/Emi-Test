using System;
using System.Collections.Generic;
using CommonServiceLocator;
using Emi.Portal.Movil.Logic.Contracts.PageViewModels.MedicalVideoCall;
using Emi.Portal.Movil.Logic.Contracts.Services;
using Emi.Portal.Movil.Logic.Resources;
using Xamarin.Forms;

namespace Emi.Portal.Movil.Pages.Services
{
    public partial class QueuingPage : ContentPage
    {
        bool VisiblePage = true;

        public QueuingPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IQueuingViewModel viewModel)
            {
                viewModel.RemovePage();
                if (VisiblePage)
                {
                    viewModel.OldHasDoctor = true;
                    viewModel.OldPosition = null;
                    viewModel.ValideDoctor = true;
                    viewModel.AllInOrder();
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            VisiblePage = false;

            if (BindingContext is IQueuingViewModel viewModel)
            {
                if (viewModel.Qualify == false)
                {
                    IQueingFirebaseService queingFirebase = ServiceLocator.Current.GetInstance<IQueingFirebaseService>();
                    queingFirebase.GetOutSesionWaitingRoom(viewModel.patient.PatientServiceType, viewModel.requestMedicalService.PatientDocument);
                }
            }
        }
    }
}
