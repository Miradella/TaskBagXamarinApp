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
        public ObservableCollection<Plugin.Geolocator.Abstractions.Position> Positions { get; } = new ObservableCollection<Plugin.Geolocator.Abstractions.Position>();
        public Geolocation()
        {
            InitializeComponent();
            Tracking.Text = Resource.Tracking;
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
       
   

    }
}