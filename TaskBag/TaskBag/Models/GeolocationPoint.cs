using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
namespace TaskBag.Models
{
    class GeolocationPoint
    {
        private GeoPoint point;
        private bool newPoint;
        public GeolocationPoint()
        {
            point = new GeoPoint(0,0);
            newPoint = false;
        }
        public  async void Geolacation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if ((point.x != location.Latitude) || (point.y != location.Longitude))
            {
                point = new GeoPoint(location.Latitude, location.Latitude);
                newPoint = true;
            }
            else newPoint = false;
        }
        public GeoPoint GetPoint()
        {
            return point;
        }
        public bool isNew()
        {
            return newPoint;
        }
    }
}
