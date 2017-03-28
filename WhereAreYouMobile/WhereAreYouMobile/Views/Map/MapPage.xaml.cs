using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using TK.CustomMap;
using WhereAreYouMobile.ViewModels.Map;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace WhereAreYouMobile.Views.Map
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private readonly List<CustomPin> _pins = new List<CustomPin>();
        public MapPage()
        {
            InitializeComponent();
            this.BindingContext = new MapViewModel(this.customMap);
            //var pin = new CustomPin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.Place,
            //        Position = new Position(37.79752, -122.40183),
            //        Label = "Xamarin San Francisco Office",
            //        Address = "394 Pacific Ave, San Francisco CA"
            //    },
            //    Id = "Xamarin",
            //    Url = "http://xamarin.com/about/"
            //};
            //var pin2 = new CustomPin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.Place,
            //        Position = new Position(37.79752, -122.40183),
            //        Label = "Xamarin San Francisco Office",
            //        Address = "394 Pacific Ave, San Francisco CA"
            //    },
            //    Id = "Xamarin",
            //    Url = "http://xamarin.com/about/"
            //};
            //_pins.Add(pin);
            //_pins.Add(pin2);
            ////customMap.IsShowingUser = true;
            //customMap.CustomPins = new List<CustomPin>();
            //customMap.CustomPins.Add(pin);
            //customMap.CustomPins.Add(pin2);
            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMiles(1.0)));

        }

        private async Task Update()
        {
            try
            {
                await CrossGeolocator.Current.StartListeningAsync(2000,20);
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                locator.PositionChanged += (sender, args) =>
                {

                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(args.Position.Latitude, args.Position.Longitude), Distance.FromMiles(1.0)));
                    _pins[0].Pin.Position = new Position(args.Position.Latitude, args.Position.Longitude);
                    customMap.Pins.Clear();
                    var sum = 0.00100;
                    foreach (var customPin in _pins)
                    {
                        customPin.Pin.Position = new Position(args.Position.Latitude, args.Position.Longitude + sum);
                        customMap.CustomPins.Add(customPin);
                        sum = sum + 0.00200;
                    }
                    
                };
                var position = await locator.GetPositionAsync(1111);
                if (position == null)
                    return;

       
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }
        }

    }

}
