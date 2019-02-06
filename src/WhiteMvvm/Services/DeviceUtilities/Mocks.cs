using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Services.DeviceUtilities
{
    public class Mocks
    {
        public class ConnectivityMockService : IConnectivity
        {

            Xamarin.Essentials.NetworkAccess IConnectivity.NetworkAccess => Xamarin.Essentials.NetworkAccess.Internet;

            IEnumerable<Xamarin.Essentials.ConnectionProfile> IConnectivity.ConnectionProfiles => new List<Xamarin.Essentials.ConnectionProfile>() { Xamarin.Essentials.ConnectionProfile.WiFi };

            event EventHandler<Xamarin.Essentials.ConnectivityChangedEventArgs> IConnectivity.ConnectivityChanged
            {
                add => Xamarin.Essentials.Connectivity.ConnectivityChanged += value;
                remove => Xamarin.Essentials.Connectivity.ConnectivityChanged -= value;
            }
        }
    }
}
