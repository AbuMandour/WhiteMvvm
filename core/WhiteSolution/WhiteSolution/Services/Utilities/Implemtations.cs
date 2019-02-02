using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhiteSolution.Services.Utilities
{
	public class AccelerometerService :  IAccelerometer
    { 	
		void IAccelerometer.Start(Xamarin.Essentials.SensorSpeed sensorSpeed)
			 => Xamarin.Essentials.Accelerometer.Start(sensorSpeed);
		
		void IAccelerometer.Stop()
			 => Xamarin.Essentials.Accelerometer.Stop();
		
		bool IAccelerometer.IsMonitoring
			 => Xamarin.Essentials.Accelerometer.IsMonitoring;
	}
	public class AppInfoService :  IAppInfo { 
	
		void IAppInfo.ShowSettingsUI()
			 => Xamarin.Essentials.AppInfo.ShowSettingsUI();
		
		string IAppInfo.PackageName
			 => Xamarin.Essentials.AppInfo.PackageName;
		
		string IAppInfo.Name
			 => Xamarin.Essentials.AppInfo.Name;
		
		string IAppInfo.VersionString
			 => Xamarin.Essentials.AppInfo.VersionString;
		
		Version IAppInfo.Version
			 => Xamarin.Essentials.AppInfo.Version;
		
		string IAppInfo.BuildString
			 => Xamarin.Essentials.AppInfo.BuildString;
	}
	public class BarometerService :  IBarometer { 
	
		void IBarometer.Start(Xamarin.Essentials.SensorSpeed sensorSpeed)
			 => Xamarin.Essentials.Barometer.Start(sensorSpeed);
		
		void IBarometer.Stop()
			 => Xamarin.Essentials.Barometer.Stop();
		
		bool IBarometer.IsMonitoring
			 => Xamarin.Essentials.Barometer.IsMonitoring;
	}
	public class BatteryService :  IBattery { 
	
		double IBattery.ChargeLevel
			 => Xamarin.Essentials.Battery.ChargeLevel;

        Xamarin.Essentials.BatteryState IBattery.State
			 => Xamarin.Essentials.Battery.State;

        Xamarin.Essentials.BatteryPowerSource IBattery.PowerSource
			 => Xamarin.Essentials.Battery.PowerSource;

        Xamarin.Essentials.EnergySaverStatus IBattery.EnergySaverStatus
			 => Xamarin.Essentials.Battery.EnergySaverStatus;
		
		event EventHandler<Xamarin.Essentials.BatteryInfoChangedEventArgs> IBattery.BatteryInfoChanged
		{
			 add => Xamarin.Essentials.Battery.BatteryInfoChanged += value; 
			 remove => Xamarin.Essentials.Battery.BatteryInfoChanged -= value; 
		 }
		
		event EventHandler<Xamarin.Essentials.EnergySaverStatusChangedEventArgs> IBattery.EnergySaverStatusChanged
		{
			 add => Xamarin.Essentials.Battery.EnergySaverStatusChanged += value; 
			 remove => Xamarin.Essentials.Battery.EnergySaverStatusChanged -= value; 
		 }
	}
	public class BrowserService :  IBrowser { 
	
		Task IBrowser.OpenAsync(string uri)
			 => Xamarin.Essentials.Browser.OpenAsync(uri);
		
		Task IBrowser.OpenAsync(string uri, Xamarin.Essentials.BrowserLaunchMode launchMode)
			 => Xamarin.Essentials.Browser.OpenAsync(uri, launchMode);
		
		Task IBrowser.OpenAsync(Uri uri)
			 => Xamarin.Essentials.Browser.OpenAsync(uri);
		
		Task<bool> IBrowser.OpenAsync(Uri uri, Xamarin.Essentials.BrowserLaunchMode launchMode)
			 => Xamarin.Essentials.Browser.OpenAsync(uri, launchMode);
	}
	public class ClipboardService :  IClipboard { 
	
		Task IClipboard.SetTextAsync(string text)
			 => Xamarin.Essentials.Clipboard.SetTextAsync(text);
		
		Task<string> IClipboard.GetTextAsync()
			 => Xamarin.Essentials.Clipboard.GetTextAsync();
		
		bool IClipboard.HasText
			 => Xamarin.Essentials.Clipboard.HasText;
	}
	public class CompassService :  ICompass { 
	
		void ICompass.Start(Xamarin.Essentials.SensorSpeed sensorSpeed)
			 => Xamarin.Essentials.Compass.Start(sensorSpeed);
		
		void ICompass.Start(Xamarin.Essentials.SensorSpeed sensorSpeed, bool applyLowPassFilter)
			 => Xamarin.Essentials.Compass.Start(sensorSpeed, applyLowPassFilter);
		
		void ICompass.Stop()
			 => Xamarin.Essentials.Compass.Stop();
		
		bool ICompass.IsMonitoring
			 => Xamarin.Essentials.Compass.IsMonitoring;
	}
	public class ConnectivityService :  IConnectivity {

        Xamarin.Essentials.NetworkAccess IConnectivity.NetworkAccess
        {
            get
            {
                return Xamarin.Essentials.Connectivity.NetworkAccess;
            }
        }

        IEnumerable<Xamarin.Essentials.ConnectionProfile> IConnectivity.ConnectionProfiles
			 => Xamarin.Essentials.Connectivity.ConnectionProfiles;
		
		event EventHandler<Xamarin.Essentials.ConnectivityChangedEventArgs> IConnectivity.ConnectivityChanged
		{
			 add => Xamarin.Essentials.Connectivity.ConnectivityChanged += value; 
			 remove => Xamarin.Essentials.Connectivity.ConnectivityChanged -= value; 
		 }
	}
	public class DeviceDisplayService :  IDeviceDisplay { 
	
		bool IDeviceDisplay.KeepScreenOn
		{
			 get { return Xamarin.Essentials.DeviceDisplay.KeepScreenOn; }
			 set { Xamarin.Essentials.DeviceDisplay.KeepScreenOn = value; }
		}

        Xamarin.Essentials.DisplayInfo IDeviceDisplay.MainDisplayInfo
			 => Xamarin.Essentials.DeviceDisplay.MainDisplayInfo;
		
		event EventHandler<Xamarin.Essentials.DisplayInfoChangedEventArgs> IDeviceDisplay.MainDisplayInfoChanged
		{
			 add => Xamarin.Essentials.DeviceDisplay.MainDisplayInfoChanged += value; 
			 remove => Xamarin.Essentials.DeviceDisplay.MainDisplayInfoChanged -= value; 
		 }
	}
	public class DeviceInfoService :  IDeviceInfo { 
	
		string IDeviceInfo.Model
			 => Xamarin.Essentials.DeviceInfo.Model;
		
		string IDeviceInfo.Manufacturer
			 => Xamarin.Essentials.DeviceInfo.Manufacturer;
		
		string IDeviceInfo.Name
			 => Xamarin.Essentials.DeviceInfo.Name;
		
		string IDeviceInfo.VersionString
			 => Xamarin.Essentials.DeviceInfo.VersionString;
		
		Version IDeviceInfo.Version
			 => Xamarin.Essentials.DeviceInfo.Version;

        Xamarin.Essentials.DevicePlatform IDeviceInfo.Platform
			 => Xamarin.Essentials.DeviceInfo.Platform;

        Xamarin.Essentials.DeviceIdiom IDeviceInfo.Idiom
			 => Xamarin.Essentials.DeviceInfo.Idiom;

        Xamarin.Essentials.DeviceType IDeviceInfo.DeviceType
			 => Xamarin.Essentials.DeviceInfo.DeviceType;
	}
	public class EmailService :  IEmail { 
	
		Task IEmail.ComposeAsync()
			 => Xamarin.Essentials.Email.ComposeAsync();
		
		Task IEmail.ComposeAsync(string subject, string body, params string[] to)
			 => Xamarin.Essentials.Email.ComposeAsync(subject, body, to);
		
		Task IEmail.ComposeAsync(Xamarin.Essentials.EmailMessage message)
			 => Xamarin.Essentials.Email.ComposeAsync(message);
	}
	public class FileSystemService :  IFileSystem { 
	
		Task<Stream> IFileSystem.OpenAppPackageFileAsync(string filename)
			 => Xamarin.Essentials.FileSystem.OpenAppPackageFileAsync(filename);
		
		string IFileSystem.CacheDirectory
			 => Xamarin.Essentials.FileSystem.CacheDirectory;
		
		string IFileSystem.AppDataDirectory
			 => Xamarin.Essentials.FileSystem.AppDataDirectory;
	}
	public class FlashlightService :  IFlashlight { 
	
		Task IFlashlight.TurnOnAsync()
			 => Xamarin.Essentials.Flashlight.TurnOnAsync();
		
		Task IFlashlight.TurnOffAsync()
			 => Xamarin.Essentials.Flashlight.TurnOffAsync();
	}
	public class GeocodingService :  IGeocoding { 
	
		Task<IEnumerable<Xamarin.Essentials.Placemark>> IGeocoding.GetPlacemarksAsync(Xamarin.Essentials.Location location)
			 => Xamarin.Essentials.Geocoding.GetPlacemarksAsync(location);
		
		Task<IEnumerable<Xamarin.Essentials.Placemark>> IGeocoding.GetPlacemarksAsync(double latitude, double longitude)
			 => Xamarin.Essentials.Geocoding.GetPlacemarksAsync(latitude, longitude);
		
		Task<IEnumerable<Xamarin.Essentials.Location>> IGeocoding.GetLocationsAsync(string address)
			 => Xamarin.Essentials.Geocoding.GetLocationsAsync(address);
	}
	public class GeolocationService :  IGeolocation { 
	
		Task<Xamarin.Essentials.Location> IGeolocation.GetLastKnownLocationAsync()
			 => Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();
		
		Task<Xamarin.Essentials.Location> IGeolocation.GetLocationAsync()
			 => Xamarin.Essentials.Geolocation.GetLocationAsync();
		
		Task<Xamarin.Essentials.Location> IGeolocation.GetLocationAsync(Xamarin.Essentials.GeolocationRequest request)
			 => Xamarin.Essentials.Geolocation.GetLocationAsync(request);
		
		Task<Xamarin.Essentials.Location> IGeolocation.GetLocationAsync(Xamarin.Essentials.GeolocationRequest request, CancellationToken cancelToken)
			 => Xamarin.Essentials.Geolocation.GetLocationAsync(request, cancelToken);
	}
	public class GyroscopeService :  IGyroscope { 
	
		void IGyroscope.Start(Xamarin.Essentials.SensorSpeed sensorSpeed)
			 => Xamarin.Essentials.Gyroscope.Start(sensorSpeed);
		
		void IGyroscope.Stop()
			 => Xamarin.Essentials.Gyroscope.Stop();
		
		bool IGyroscope.IsMonitoring
			 => Xamarin.Essentials.Gyroscope.IsMonitoring;
	}
	public class LauncherService :  ILauncher { 
	
		Task<bool> ILauncher.CanOpenAsync(string uri)
			 => Xamarin.Essentials.Launcher.CanOpenAsync(uri);
		
		Task<bool> ILauncher.CanOpenAsync(Uri uri)
			 => Xamarin.Essentials.Launcher.CanOpenAsync(uri);
		
		Task ILauncher.OpenAsync(string uri)
			 => Xamarin.Essentials.Launcher.OpenAsync(uri);
		
		Task ILauncher.OpenAsync(Uri uri)
			 => Xamarin.Essentials.Launcher.OpenAsync(uri);
	}
	public class MagnetometerService :  IMagnetometer { 
	
		void IMagnetometer.Start(Xamarin.Essentials.SensorSpeed sensorSpeed)
			 => Xamarin.Essentials.Magnetometer.Start(sensorSpeed);
		
		void IMagnetometer.Stop()
			 => Xamarin.Essentials.Magnetometer.Stop();
		
		bool IMagnetometer.IsMonitoring
			 => Xamarin.Essentials.Magnetometer.IsMonitoring;
	}
	public class MainThreadService :  IMainThread { 
	
		void IMainThread.BeginInvokeOnMainThread(Action action)
			 => Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(action);
		
		bool IMainThread.IsMainThread
			 => Xamarin.Essentials.MainThread.IsMainThread;
	}
	public class MapService :  IMap { 
	
		Task IMap.OpenAsync(Xamarin.Essentials.Location location)
			 => Xamarin.Essentials.Map.OpenAsync(location);
		
		Task IMap.OpenAsync(Xamarin.Essentials.Location location, Xamarin.Essentials.MapLaunchOptions options)
			 => Xamarin.Essentials.Map.OpenAsync(location, options);
		
		Task IMap.OpenAsync(double latitude, double longitude)
			 => Xamarin.Essentials.Map.OpenAsync(latitude, longitude);
		
		Task IMap.OpenAsync(double latitude, double longitude, Xamarin.Essentials.MapLaunchOptions options)
			 => Xamarin.Essentials.Map.OpenAsync(latitude, longitude, options);
		
		Task IMap.OpenAsync(Xamarin.Essentials.Placemark placemark)
			 => Xamarin.Essentials.Map.OpenAsync(placemark);
		
		Task IMap.OpenAsync(Xamarin.Essentials.Placemark placemark, Xamarin.Essentials.MapLaunchOptions options)
			 => Xamarin.Essentials.Map.OpenAsync(placemark, options);
	}
	public class OrientationSensorService :  IOrientationSensor { 
	
		void IOrientationSensor.Start(Xamarin.Essentials.SensorSpeed sensorSpeed)
			 => Xamarin.Essentials.OrientationSensor.Start(sensorSpeed);
		
		void IOrientationSensor.Stop()
			 => Xamarin.Essentials.OrientationSensor.Stop();
		
		bool IOrientationSensor.IsMonitoring
			 => Xamarin.Essentials.OrientationSensor.IsMonitoring;
	}
	public class PhoneDialerService :  IPhoneDialer { 
	
		void IPhoneDialer.Open(string number)
			 => Xamarin.Essentials.PhoneDialer.Open(number);
	}
	public class PreferencesService :  IPreferences { 
	
		bool IPreferences.ContainsKey(string key)
			 => Xamarin.Essentials.Preferences.ContainsKey(key);
		
		void IPreferences.Remove(string key)
			 => Xamarin.Essentials.Preferences.Remove(key);
		
		void IPreferences.Clear()
			 => Xamarin.Essentials.Preferences.Clear();
		
		string IPreferences.Get(string key, string defaultValue)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue);
		
		bool IPreferences.Get(string key, bool defaultValue)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue);
		
		int IPreferences.Get(string key, int defaultValue)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue);
		
		double IPreferences.Get(string key, double defaultValue)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue);
		
		float IPreferences.Get(string key, float defaultValue)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue);
		
		long IPreferences.Get(string key, long defaultValue)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue);
		
		void IPreferences.Set(string key, string value)
			 => Xamarin.Essentials.Preferences.Set(key, value);
		
		void IPreferences.Set(string key, bool value)
			 => Xamarin.Essentials.Preferences.Set(key, value);
		
		void IPreferences.Set(string key, int value)
			 => Xamarin.Essentials.Preferences.Set(key, value);
		
		void IPreferences.Set(string key, double value)
			 => Xamarin.Essentials.Preferences.Set(key, value);
		
		void IPreferences.Set(string key, float value)
			 => Xamarin.Essentials.Preferences.Set(key, value);
		
		void IPreferences.Set(string key, long value)
			 => Xamarin.Essentials.Preferences.Set(key, value);
		
		bool IPreferences.ContainsKey(string key, string sharedName)
			 => Xamarin.Essentials.Preferences.ContainsKey(key, sharedName);
		
		void IPreferences.Remove(string key, string sharedName)
			 => Xamarin.Essentials.Preferences.Remove(key, sharedName);
		
		void IPreferences.Clear(string sharedName)
			 => Xamarin.Essentials.Preferences.Clear(sharedName);
		
		string IPreferences.Get(string key, string defaultValue, string sharedName)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);
		
		bool IPreferences.Get(string key, bool defaultValue, string sharedName)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);
		
		int IPreferences.Get(string key, int defaultValue, string sharedName)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);
		
		double IPreferences.Get(string key, double defaultValue, string sharedName)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);
		
		float IPreferences.Get(string key, float defaultValue, string sharedName)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);
		
		long IPreferences.Get(string key, long defaultValue, string sharedName)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);
		
		void IPreferences.Set(string key, string value, string sharedName)
			 => Xamarin.Essentials.Preferences.Set(key, value, sharedName);
		
		void IPreferences.Set(string key, bool value, string sharedName)
			 => Xamarin.Essentials.Preferences.Set(key, value, sharedName);
		
		void IPreferences.Set(string key, int value, string sharedName)
			 => Xamarin.Essentials.Preferences.Set(key, value, sharedName);
		
		void IPreferences.Set(string key, double value, string sharedName)
			 => Xamarin.Essentials.Preferences.Set(key, value, sharedName);
		
		void IPreferences.Set(string key, float value, string sharedName)
			 => Xamarin.Essentials.Preferences.Set(key, value, sharedName);
		
		void IPreferences.Set(string key, long value, string sharedName)
			 => Xamarin.Essentials.Preferences.Set(key, value, sharedName);
		
		DateTime IPreferences.Get(string key, DateTime defaultValue)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue);
		
		void IPreferences.Set(string key, DateTime value)
			 => Xamarin.Essentials.Preferences.Set(key, value);
		
		DateTime IPreferences.Get(string key, DateTime defaultValue, string sharedName)
			 => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);
		
		void IPreferences.Set(string key, DateTime value, string sharedName)
			 => Xamarin.Essentials.Preferences.Set(key, value, sharedName);
	}
	public class SecureStorageService :  ISecureStorage { 
	
		Task<string> ISecureStorage.GetAsync(string key)
			 => Xamarin.Essentials.SecureStorage.GetAsync(key);
		
		Task ISecureStorage.SetAsync(string key, string value)
			 => Xamarin.Essentials.SecureStorage.SetAsync(key, value);
		
		bool ISecureStorage.Remove(string key)
			 => Xamarin.Essentials.SecureStorage.Remove(key);
		
		void ISecureStorage.RemoveAll()
			 => Xamarin.Essentials.SecureStorage.RemoveAll();
	}
	public class ShareService :  IShare { 
	
		Task IShare.RequestAsync(string text)
			 => Xamarin.Essentials.Share.RequestAsync(text);
		
		Task IShare.RequestAsync(string text, string title)
			 => Xamarin.Essentials.Share.RequestAsync(text, title);
		
		Task IShare.RequestAsync(Xamarin.Essentials.ShareTextRequest request)
			 => Xamarin.Essentials.Share.RequestAsync(request);
	}
	public class SmsService :  ISms { 
	
		Task ISms.ComposeAsync()
			 => Xamarin.Essentials.Sms.ComposeAsync();
		
		Task ISms.ComposeAsync(Xamarin.Essentials.SmsMessage message)
			 => Xamarin.Essentials.Sms.ComposeAsync(message);
	}
	public class TextToSpeechService :  ITextToSpeech { 
	
		Task<IEnumerable<Xamarin.Essentials.Locale>> ITextToSpeech.GetLocalesAsync()
			 => Xamarin.Essentials.TextToSpeech.GetLocalesAsync();
		
		Task ITextToSpeech.SpeakAsync(string text, CancellationToken cancelToken = default)
			 => Xamarin.Essentials.TextToSpeech.SpeakAsync(text, cancelToken);
		
		Task ITextToSpeech.SpeakAsync(string text, Xamarin.Essentials.SpeechOptions options, CancellationToken cancelToken = default)
			 => Xamarin.Essentials.TextToSpeech.SpeakAsync(text, options, cancelToken);
	}
	public class VersionTrackingService :  IVersionTracking { 
	
		void IVersionTracking.Track()
			 => Xamarin.Essentials.VersionTracking.Track();
		
		bool IVersionTracking.IsFirstLaunchForVersion(string version)
			 => Xamarin.Essentials.VersionTracking.IsFirstLaunchForVersion(version);
		
		bool IVersionTracking.IsFirstLaunchForBuild(string build)
			 => Xamarin.Essentials.VersionTracking.IsFirstLaunchForBuild(build);
		
		bool IVersionTracking.IsFirstLaunchEver
			 => Xamarin.Essentials.VersionTracking.IsFirstLaunchEver;
		
		bool IVersionTracking.IsFirstLaunchForCurrentVersion
			 => Xamarin.Essentials.VersionTracking.IsFirstLaunchForCurrentVersion;
		
		bool IVersionTracking.IsFirstLaunchForCurrentBuild
			 => Xamarin.Essentials.VersionTracking.IsFirstLaunchForCurrentBuild;
		
		string IVersionTracking.CurrentVersion
			 => Xamarin.Essentials.VersionTracking.CurrentVersion;
		
		string IVersionTracking.CurrentBuild
			 => Xamarin.Essentials.VersionTracking.CurrentBuild;
		
		string IVersionTracking.PreviousVersion
			 => Xamarin.Essentials.VersionTracking.PreviousVersion;
		
		string IVersionTracking.PreviousBuild
			 => Xamarin.Essentials.VersionTracking.PreviousBuild;
		
		string IVersionTracking.FirstInstalledVersion
			 => Xamarin.Essentials.VersionTracking.FirstInstalledVersion;
		
		string IVersionTracking.FirstInstalledBuild
			 => Xamarin.Essentials.VersionTracking.FirstInstalledBuild;
		
		IEnumerable<string> IVersionTracking.VersionHistory
			 => Xamarin.Essentials.VersionTracking.VersionHistory;
		
		IEnumerable<string> IVersionTracking.BuildHistory
			 => Xamarin.Essentials.VersionTracking.BuildHistory;
	}
	public class VibrationService :  IVibration { 
	
		void IVibration.Vibrate()
			 => Xamarin.Essentials.Vibration.Vibrate();
		
		void IVibration.Vibrate(double duration)
			 => Xamarin.Essentials.Vibration.Vibrate(duration);
		
		void IVibration.Vibrate(TimeSpan duration)
			 => Xamarin.Essentials.Vibration.Vibrate(duration);
		
		void IVibration.Cancel()
			 => Xamarin.Essentials.Vibration.Cancel();
	}

}
