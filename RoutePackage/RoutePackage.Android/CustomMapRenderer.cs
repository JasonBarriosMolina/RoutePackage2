using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using RoutePackage;
using RoutePackage.Android;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace RoutePackage.Android
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        GoogleMap map;
        List<Position> routeCoordinates;
        CustomCircle circle;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                routeCoordinates = formsMap.RouteCoordinates;
                this.circle = formsMap.Circle;

                ((MapView)Control).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;

            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(0x66FF0000);

            foreach (var position in routeCoordinates)
            {
                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }

            map.AddPolyline(polylineOptions);

        }

        public void OnMapLoaded(GoogleMap googleMap) {

            var circleOptions = new CircleOptions();
            circleOptions.InvokeCenter(new LatLng(this.circle.Position.Latitude, this.circle.Position.Longitude));
            circleOptions.InvokeRadius(this.circle.Radius);
            //circleOptions.InvokeFillColor(0X66FF0000);
            circleOptions.InvokeStrokeColor(0X66FF0000);
            circleOptions.InvokeStrokeWidth(3);

            map.AddCircle(circleOptions);


        }
    }
}
