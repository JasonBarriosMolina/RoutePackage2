using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using RoutePackage.Models.Database;

namespace RoutePackage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewPlace : ContentPage
    {
        Geocoder geoCoder;
        CustomMap customMap;
        public DBContext Contexto { get; set; }

        public AddNewPlace(DBContext contexto)
        {

            Contexto = contexto;

            InitializeComponent();

            geoCoder = new Geocoder();
            customMap = new CustomMap()
            {
                MapType = MapType.Hybrid,
                IsShowingUser = true
            };

            CurrentPosition(customMap);

            mapContent.Children.Add(customMap);
        }

        private async void CurrentPosition(CustomMap map)
        {

            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(10000);

            if (position != null)
            {
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));
            }

        }

        private void OnStreetClicked(object sender, EventArgs e) =>
            customMap.MapType = MapType.Street;

        private void OnHybridClicked(object sender, EventArgs e) =>
            customMap.MapType = MapType.Hybrid;

        private void OnSatelliteClicked(object sender, EventArgs e) =>
            customMap.MapType = MapType.Satellite;

        private async void OnAddPinClicked(object sender, EventArgs e)
        {
            var point = customMap.VisibleRegion.Center;
            var item = (await geoCoder.GetAddressesForPositionAsync(point)).FirstOrDefault();

            var name = item ?? "Unknown";

            customMap.Pins.Add(new Pin
            {
                Label = name,
                Position = point,
                Type = PinType.Generic
            });

            await Navigation.PushAsync(new NavigationPage(new NewItemPage()));
        }

        private void OnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            var zoomLevel = e.NewValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            customMap.MoveToRegion(new MapSpan(customMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }
    }


}
