using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace RoutePackage
{
    public class CustomMap : Map
    {
        public List<Position> RouteCoordinates { get; set; }
        public CustomCircle Circle { get; set; }

        public CustomMap()
        {
            RouteCoordinates = new List<Position>();
        }
    }

    public class CustomCircle
    {
        public Position Position { get; set; }
        public double Radius { get; set; }
    }
}

