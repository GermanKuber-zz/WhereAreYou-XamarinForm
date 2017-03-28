using System;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions.Config;
using WhereAreYouMobile.Services;
using Xamarin.Forms;
using Plugin.Geolocator;
using WhereAreYouMobile.Abstractions;

//#define OFFLINE_SYNC_ENABLED
[assembly: Dependency(typeof(LocationService))]

namespace WhereAreYouMobile.Services
{
    public class LocationService : ILocationService
    {
        private bool _initialize = false;
        public LocationService()
        {

        }

        public async Task<LocationPosition> GetCurrentPosition()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(1000);
            if (position == null)
                return new LocationPosition(position.Latitude, position.Longitude);

            return null;
        }

        public async Task UpdateMeLocation(Action<LocationPosition> callBack)
        {

            if (!_initialize)
            {
                try
                {
                    await CrossGeolocator.Current.StartListeningAsync(2000, 20);
                    _initialize = true;
                }
                catch (Exception e)
                {
                    _initialize = false;
                    throw;
                }
            }

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            locator.PositionChanged += (sender, args) =>
            {
                callBack.Invoke(new LocationPosition(args.Position.Latitude, args.Position.Longitude));
            };

        }
    }

    public class LocationPosition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LocationPosition(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}