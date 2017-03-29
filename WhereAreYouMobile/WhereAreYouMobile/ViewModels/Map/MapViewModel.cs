using System.Collections.Generic;
using System.Threading.Tasks;
using WhereAreYouMobile.Abstractions;
using WhereAreYouMobile.Services.Common;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace WhereAreYouMobile.ViewModels.Map
{
    public class MapViewModel
    {
        private readonly CustomMap _customMap;
        private readonly ILocationService _locationService;
        private readonly List<CustomPin> _pins = new List<CustomPin>();
        private readonly IMapService _mapService;
        private readonly IEventAgregatorService _eventAgregatorService;

        public MapViewModel(CustomMap customMap)
        {
            _customMap = customMap;
            _locationService = DependencyService.Get<ILocationService>();
            _mapService = DependencyService.Get<IMapService>();
            _eventAgregatorService = DependencyService.Get<IEventAgregatorService>();
            _mapService.SetMap(customMap);
           
            _locationService.UpdateMeLocation((location) =>
            {
                _mapService.MoveToRegion(location.Latitude,location.Longitude);
            });

            CenterMap();
        }

        private void SubscribirEvents()
        {
            //_eventAgregatorService.Subscribe(EventAgregatorTypeEnum.UpdateMyFriends, );
        }

        private async Task CenterMap()
        {
            var position = await _locationService.GetCurrentPosition();
            if (position != null)
                _customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1.0)));
        }
    }

    public class CustomMap : Xamarin.Forms.Maps.Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }
    public class CustomPin
    {
        public Pin Pin { get; set; }

        public string Id { get; set; }

        public string Url { get; set; }
    }
}
