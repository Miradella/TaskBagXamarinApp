using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace TaskBag
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "notes.db";
        public static NotesRepository database;
        public static string User_email=null;
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
        }

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
