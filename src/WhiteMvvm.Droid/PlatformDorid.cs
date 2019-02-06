using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WhiteMvvm.Droid
{
    public class PlatformDorid 
    {
        internal static bool IsInitialized { get; private set; }
        public static void Init(Context context,Bundle bundle)
        {
            IsInitialized = true;
            var activity = context as Activity;
            Rg.Plugins.Popup.Popup.Init(context, bundle);
            UserDialogs.Init(activity);
        }
    }
}