using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using TaskBag.Models;
using TaskBag.Interfaces;
using Xamarin.Essentials;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Collections.ObjectModel;
using Plugin.Permissions.Abstractions;

namespace TaskBag.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Geolocation : ContentPage
    {
        int count;
        bool tracking;
        Plugin.Geolocator.Abstractions.Position savedPosition;
        GeolocationPoint geolocationPoint = new GeolocationPoint();
        Plugin.Geolocator.Abstractions.Position oldPoint = new Plugin.Geolocator.Abstractions.Position( 0,0);
        //public static ILocationUpdateService LocationUpdateService;
        public ObservableCollection<Plugin.Geolocator.Abstractions.Position> Positions { get; } = new ObservableCollection<Plugin.Geolocator.Abstractions.Position>();
        public Geolocation()
        {
            InitializeComponent();
            
            /*double z = 0;
            var timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Start();
            timer.Elapsed += (sender, args) =>
            {
                timer.Enabled = false;
                Position position = new Position(App.location.Latitude+z,App.location.Longitude+z);
                Pin pin = new Pin
                {
                    Label = "Santa Cruz",
                    Address = "The city with a boardwalk",
                    Type = PinType.Place,
                    Position = position
                };
                map.Pins.Add(pin);
                //setPoint();
                timer.Enabled = true;
            };
            var timer = new System.Timers.Timer();
                timer.Interval = 1000;
                timer.Start();
                timer.Elapsed += (sender, args) =>
                {
                    timer.Enabled = false;
                    Position position = new Position(X++, -122.0194722);
                    Pin pin = new Pin
                    {
                        Label = "Santa Cruz",
                        Address = "The city with a boardwalk",
                        Type = PinType.Place,
                        Position = position
                    };
                    map.Pins.Add(pin);
                    //setPoint();
                    timer.Enabled = true;
                }; */
        }
        private async void ButtonTrack_Clicked(object sender, EventArgs e)
        {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                    return;

                if (tracking)
                {
                    CrossGeolocator.Current.PositionChanged -= CrossGeolocator_Current_PositionChanged;
                    CrossGeolocator.Current.PositionError -= CrossGeolocator_Current_PositionError;
                }
                else
                {
                    CrossGeolocator.Current.PositionChanged += CrossGeolocator_Current_PositionChanged;
                    CrossGeolocator.Current.PositionError += CrossGeolocator_Current_PositionError;
                }

                if (CrossGeolocator.Current.IsListening)
                {
                    await CrossGeolocator.Current.StopListeningAsync();
                    //labelGPSTrack.Text = "Stopped tracking";
                   // ButtonTrack.Text = "Start Tracking";
                    tracking = false;
                    count = 0;
                }
                else
                {
                    Positions.Clear();
                    if (await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(0), 10,
                        true, new ListenerSettings
                        {
                            ActivityType = (ActivityType)0,
                            AllowBackgroundUpdates = true,
                            DeferLocationUpdates = true,
                            DeferralDistanceMeters = 50,
                            DeferralTime = TimeSpan.FromSeconds(60),
                            ListenForSignificantChanges = true,
                            PauseLocationUpdatesAutomatically = true
                        }))
                    {
                        //labelGPSTrack.Text = "Started tracking";
                        //ButtonTrack.Text = "Stop Tracking";
                        tracking = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
            }
        }

        void CrossGeolocator_Current_PositionError(object sender, PositionErrorEventArgs e)
        {

            //
        }
        Xamarin.Forms.Maps.Position oldposition;
        bool isFirst = true;
        void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                var position = e.Position;
                Positions.Add(position);
                count++;
                /*  LabelCount.Text = $"{count} updates";
                  labelGPSTrack.Text = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                      position.Timestamp, position.Latitude, position.Longitude,
                      position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);*/
                if (isFirst)
                {
                    oldposition = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                    isFirst = false;
                    map.MoveToRegion(new MapSpan(oldposition, 0.002, 0.002));
                }
                else
                {
                    Xamarin.Forms.Maps.Position newP = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                    Polyline polyline = new Polyline
                    {
                        StrokeColor = Color.Red,
                        StrokeWidth = 12,
                        Geopath = { oldposition, newP }
                    };
                    map.MapElements.Add(polyline);

                    oldposition = newP;
                }
            });
        }
        /*private void LocationUpdateService_LocationChanged(object sender, ILocationEventArgs e)
        {
            //Here you can get the user's location from "e" -> new Location(e.Latitude, e.Longitude);
            //new Location is from Xamarin.Essentials Location object.
            Location l= new Location(e.Latitude, e.Longitude);
            Position position = new Position(l.Latitude+z, l.Longitude+z);
           // z += 1;
            Pin pin = new Pin
            {
                Label = "Santa Cruz",
                Address = "The city with a boardwalk",
                Type = PinType.Place,
                Position = position
            };

            map.Pins.Add(pin);
        } */
        double z = 0;
   /*     private async void setPoint()
        {
            geolocationPoint.Geolacation();
            //if (geolocationPoint.isNew())
            //{
                Position newP = new Position(geolocationPoint.GetPoint().x+z, geolocationPoint.GetPoint().y+z);
                z += 10;
                Polyline polyline = new Polyline
                {
                    StrokeColor = Color.Red,
                    StrokeWidth = 12,
                    Geopath = { oldPoint, newP }
                };
                map.MapElements.Add(polyline);
                oldPoint = newP;
            //}
        }*/

    }
}