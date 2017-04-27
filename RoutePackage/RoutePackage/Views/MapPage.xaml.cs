
using RoutePackage.Models;
using RoutePackage.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;

namespace RoutePackage
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        Geocoder geoCoder;
        CustomMap customMap;
        Position currentPosition;

        public DBContext Contexto { get; set; }

        public MapPage(DBContext contexto)
        {

            Contexto = contexto;

            InitializeComponent();

            geoCoder = new Geocoder();
            customMap = new CustomMap() {
                MapType = MapType.Hybrid,
                IsShowingUser = true
            };

            CurrentPosition(customMap);
            
            mapContent.Children.Add(customMap);

        }

        private void OnStreetClicked(object sender, EventArgs e) =>
            customMap.MapType = MapType.Street;

        private void OnHybridClicked(object sender, EventArgs e) =>
            customMap.MapType = MapType.Hybrid;

        private void OnSatelliteClicked(object sender, EventArgs e) =>
            customMap.MapType = MapType.Satellite;

        private void OnGoToClicked(object sender, EventArgs e)
        {

            if (EntryLocation.Text != String.Empty)
                SearchCategory(EntryLocation.Text);
            else
                DisplayAlert("Search", "Filtro vacio ...", "Ok");


            //var item = (await geoCoder.GetPositionsForAddressAsync(EntryLocation.Text)).FirstOrDefault();
            //if (item == null)
            //{
            //    await DisplayAlert("Error", "Unable to decode position", "OK");
            //    return;
            //}

            //var zoomLevel = SliderZoom.Value; // between 1 and 18
            //var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            //customMap.MoveToRegion(new MapSpan(item, latlongdegrees, latlongdegrees));
        }

        private void OnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            var zoomLevel = e.NewValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            customMap.MoveToRegion(new MapSpan(customMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

        //private void OnSliderDistanceChanged(object sender, ValueChangedEventArgs e)
        //{
        //    var zoomLevel = e.NewValue; // between 1 and 18
        //    var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
        //    customMap.MoveToRegion(new MapSpan(customMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        //}

        private async void CurrentPosition(CustomMap map) {

            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(2000);

            currentPosition = new Position(position.Latitude,position.Longitude);

            if (position != null)
            {
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(2)));
            }

        }

        private void DrawPoints(List<SearchPlaces> places)
        {
            foreach (var place in places)
            {
                foreach (var item in place.searchplace)
                {
                    var position = new Position(item.Latitude,item.Longitude); // Latitude, Longitude

                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = item.Name,
                        Address = item.Description
                    };
                    customMap.Pins.Add(pin);

                    customMap.RouteCoordinates.Add(new Position(item.Latitude,item.Longitude));
                }
                
            }

        }


        private async void SearchCategory(string category) {

            try
            {
                var service = new Services.ServicioRest();
                var cat = await service.GetCategories();

                if (cat.searchcategory.Any())
                {
                    var cats = cat.searchcategory.Where(x => x.Name.StartsWith(category.ToUpper()) || x.Name.Contains(category.ToUpper())).ToList();

                    if (cats.Any())
                    {

                        var places = new List<SearchPlaces>();

                        foreach (var item in cats)
                        {
                            var place = await service.GetPlacesByCategoryId(item.Id);
                            places.Add(place);
                        }

                        Contexto.Seleccionar<SearchPlaceDB>();

                        DrawPoints(places);
                    }
                }
                else {
                    await DisplayAlert("Search", "No existe la opcion " + category, "Ok");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
