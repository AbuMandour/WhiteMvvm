using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;


namespace WhiteSolution.Droid
{
    [Activity(Label = "WhiteSolution", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this,savedInstanceState);
            UserDialogs.Init(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}