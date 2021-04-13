namespace Emi.Portal.Movil.Pages.MedicalCenterCoordination
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Emi.Portal.Movil.Controls;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Domain;
    using Emi.Portal.Movil.Logic.ViewModels.Pages;
    using Plugin.Geolocator;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicalCenterCoordinationDetailPage : ContentPage
    {
        ExtendedMap map;
        MedicalCenterCoordinationPageViewModel pendingCoordination;
        CoordinationViewModel CoordinationSelected;
        public MedicalCenterCoordinationDetailPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }

            pendingCoordination = (MedicalCenterCoordinationPageViewModel)BindingContext;
            CoordinationSelected = pendingCoordination.CoordinationSelected;

            map = new ExtendedMap(MapSpan.FromCenterAndRadius(new Position(CoordinationSelected.Latitude, CoordinationSelected.Longitude), Distance.FromKilometers(1)))
            {
                IsShowingUser = CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled,
                ItemsSource = new List<ClinicViewModel>()
                {
                    new ClinicViewModel()
                    {
                        Icon = "gps",
                        HasInteraction = false,
                        Latitude = CoordinationSelected.Latitude,
                        Longitude = CoordinationSelected.Longitude,
                        Name = CoordinationSelected.ClinicName,
                    }
                }
            };
            frameMap.Content = map;
            pendingCoordination.PropertyChanged += pendingCoordinationPropertyChanged;
        }

        private void pendingCoordinationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReady")
            {
                map.CurrentLocation = new Position(CoordinationSelected.Latitude, CoordinationSelected.Longitude);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(CoordinationSelected.Latitude, CoordinationSelected.Longitude), Distance.FromKilometers(0.5)));                
            }
        }
    }
}