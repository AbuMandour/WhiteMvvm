using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WhiteSolution.Droid
{
    [Activity(Label = "WhiteSolution", Icon = "@drawable/juventusLogo", MainLauncher = true, NoHistory = true,
    Theme = "@style/MyTheme.Splash")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }   
    }
}