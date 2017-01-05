using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using FFImageLoading;

namespace KrosmagaUniverse.Droid
{
    [Activity(Label = "KrosmagaUniverse", Theme = "@style/Theme.Main", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                FFImageLoading.Forms.Droid.CachedImageRenderer.Init();



                global::Xamarin.Forms.Forms.Init(this, bundle);

                LoadApplication(new App());
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }
          
        }
    }
    public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
    {
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }

        public void Error(string errorMessage, Exception ex)
        {
            Error(errorMessage + System.Environment.NewLine + ex.ToString());
        }
    }
  
}

