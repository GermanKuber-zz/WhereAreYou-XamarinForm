using System;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions.Config;
using WhereAreYouMobile.Services;
using Xamarin.Forms;
using Plugin.Geolocator;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.ViewModels.Map;
using Xamarin.Forms.Maps;

//#define OFFLINE_SYNC_ENABLED
[assembly: Dependency(typeof(MapService))]

namespace WhereAreYouMobile.Services
{
    public class MapService : IMapService
    {
        private CustomMap _customMap;
        public MapService()
        {

        }

        public void SetMap(CustomMap customMap)
        {
            this._customMap = customMap;
        }

        public void MoveToRegion(double latitude,double longitude)
        {
            _customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMiles(1.0)));
        }
    }

}