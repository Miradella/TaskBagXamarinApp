using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using TaskBag.Interfaces;
using Xamarin.Essentials;

namespace TaskBag
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "notes.db";
        public static NotesRepository database;
       // public static ILocationUpdateService LocationUpdateService;
       // public static Location location;
        public static NotesRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new NotesRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            //location = new Location(0, 0);
            //LocationUpdateService.LocationChanged += LocationUpdateService_LocationChanged; ;
        }

     /*   private void LocationUpdateService_LocationChanged(object sender, ILocationEventArgs e)
        {
            //Here you can get the user's location from "e" -> new Location(e.Latitude, e.Longitude);
            //new Location is from Xamarin.Essentials Location object.
            location= new Location(e.Latitude, e.Longitude);
        }*/
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
