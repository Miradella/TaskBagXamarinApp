using System;
using System.Globalization;
using System.Threading;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TestBag
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.InstalledApp("com.companyname.taskbag").StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}