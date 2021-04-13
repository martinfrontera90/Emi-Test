namespace Emi.Portal.Movil.Pages
{
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
    public partial class NearbyClinicDetailPage : ContentPage
    {
        ExtendedMap map;
        NearbyClinicsPageViewModel NearbyClinicsPageViewModel;
        ClinicViewModel ClinicSelected;
        public NearbyClinicDetailPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }
            NearbyClinicsPageViewModel = (NearbyClinicsPageViewModel)BindingContext;
            ClinicSelected = NearbyClinicsPageViewModel.ClinicSelected;

            map = new ExtendedMap(MapSpan.FromCenterAndRadius(new Position(ClinicSelected.Latitude, ClinicSelected.Longitude), Distance.FromKilometers(1)))
            {
                IsShowingUser = CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled,
                ItemsSource = new List<ClinicViewModel>()
                {
                    new ClinicViewModel()
                    {
                        Icon = "gps2",
                        HasInteraction = false,
                        Latitude = ClinicSelected.Latitude,
                        Longitude = ClinicSelected.Longitude,
                        Name = ClinicSelected.Name,
                        Description = ClinicSelected.Description
                    }
                },
            };
            stackMap.Content = map;
            NearbyClinicsPageViewModel.PropertyChanged += NearbyClinicsPageViewModelPropertyChanged;
        }

        private void NearbyClinicsPageViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReady")
            {
                map.CurrentLocation = new Position(ClinicSelected.Latitude, ClinicSelected.Longitude);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(ClinicSelected.Latitude, ClinicSelected.Longitude), Distance.FromKilometers(0.5)));
                map.IsShowingUser = CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled;
            }
        }
    }
}