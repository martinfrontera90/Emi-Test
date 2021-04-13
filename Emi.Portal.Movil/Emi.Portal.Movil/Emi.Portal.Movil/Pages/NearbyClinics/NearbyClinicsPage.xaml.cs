namespace Emi.Portal.Movil.Pages
{
    using Emi.Portal.Movil.Controls;
    using Emi.Portal.Movil.Logic.Resources;
    using Emi.Portal.Movil.Logic.ViewModels.Pages;
    using Plugin.Geolocator;
    using System.ComponentModel;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NearbyClinicsPage : TabbedPage
    {
        ExtendedMap map;
        NearbyClinicsPageViewModel nearbyClinicsPageViewModel;
        double latitude = -34.810853;
        double longitude = -56.111428;

        public NearbyClinicsPage()
        {
            InitializeComponent();
            if (Device.iOS == Device.RuntimePlatform)
            {
                NavigationPage.SetTitleIcon(this, AppConfigurations.IconNavigationBar);
            }

            nearbyClinicsPageViewModel = (NearbyClinicsPageViewModel)BindingContext;

            map = new ExtendedMap(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromKilometers(20)))
            {
                IsShowingUser = CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled,
                ItemsSource = nearbyClinicsPageViewModel.Clinics,
                ZoomDistance = 2
            };

            stackMap.Children.Add(map);
            nearbyClinicsPageViewModel.PropertyChanged += NearbyClinicsPageViewModelPropertyChanged;

        }

        private void NearbyClinicsPageViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReady")
            {
                map.CurrentLocation = new Position(latitude, longitude);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromKilometers(15)));
                map.IsShowingUser = CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled;
            }

            if (e.PropertyName == "CurrentLocation" && nearbyClinicsPageViewModel.CurrentLocation != null)
            {
                map.CurrentLocation = new Position(nearbyClinicsPageViewModel.CurrentLocation.Latitude, nearbyClinicsPageViewModel.CurrentLocation.Longitude);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(nearbyClinicsPageViewModel.CurrentLocation.Latitude, nearbyClinicsPageViewModel.CurrentLocation.Longitude), Distance.FromKilometers(15)));
                map.IsShowingUser = CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled;
            }
        }
    }
}