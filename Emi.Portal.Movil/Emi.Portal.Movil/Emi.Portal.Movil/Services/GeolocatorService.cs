namespace Emi.Portal.Movil.Services
{
    using System.Threading.Tasks;
    using Emi.Portal.Movil.Logic.Contracts.Services;
    using Plugin.Geolocator;
    using Plugin.Geolocator.Abstractions;

    public class GeolocatorService : IGeolocatorService
    {
        public bool IsGeolocationAvailable
        {
            get { return CrossGeolocator.Current.IsGeolocationAvailable; }
        }

        public bool IsGeolocationEnabled
        {
            get { return CrossGeolocator.Current.IsGeolocationEnabled; }
        }


        public async Task<Position> GetPositionAsync()
        {
            Position position = await CrossGeolocator.Current.GetLastKnownLocationAsync();

            if (position == null)
            {
                position=  await CrossGeolocator.Current.GetPositionAsync(null, null, false);
            }

            return position;
        }
    }
}

