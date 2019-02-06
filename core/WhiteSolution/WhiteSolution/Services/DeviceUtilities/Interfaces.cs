using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace WhiteSolution.Services.DeviceUtilities
{
    public interface IAccelerometer
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
    public interface IAppInfo
    {
        void ShowSettingsUI();
        string PackageName { get; }
        string Name { get; }
        string VersionString { get; }
        Version Version { get; }
        string BuildString { get; }
    }
    public interface IBarometer
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
    public interface IBattery
    {
        double ChargeLevel { get; }
        BatteryState State { get; }
        BatteryPowerSource PowerSource { get; }
        EnergySaverStatus EnergySaverStatus { get; }
        event EventHandler<BatteryInfoChangedEventArgs> BatteryInfoChanged;
        event EventHandler<EnergySaverStatusChangedEventArgs> EnergySaverStatusChanged;
    }
    public interface IBrowser
    {
        Task OpenAsync(string uri);
        Task OpenAsync(string uri, BrowserLaunchMode launchMode);
        Task OpenAsync(Uri uri);
        Task<bool> OpenAsync(Uri uri, BrowserLaunchMode launchMode);
    }
    public interface IClipboard
    {
        Task SetTextAsync(string text);
        Task<string> GetTextAsync();
        bool HasText { get; }
    }
    public interface ICompass
    {
        void Start(SensorSpeed sensorSpeed);
        void Start(SensorSpeed sensorSpeed, bool applyLowPassFilter);
        void Stop();
        bool IsMonitoring { get; }
    }
    public interface IConnectivity
    {
        NetworkAccess NetworkAccess { get; }
        IEnumerable<ConnectionProfile> ConnectionProfiles { get; }
        event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;
    }
    public interface IDeviceDisplay
    {
        bool KeepScreenOn { get; set; }
        DisplayInfo MainDisplayInfo { get; }
        event EventHandler<DisplayInfoChangedEventArgs> MainDisplayInfoChanged;
    }
    public interface IDeviceInfo
    {
        string Model { get; }
        string Manufacturer { get; }
        string Name { get; }
        string VersionString { get; }
        Version Version { get; }
        DevicePlatform Platform { get; }
        DeviceIdiom Idiom { get; }
        DeviceType DeviceType { get; }
    }
    public interface IEmail
    {
        Task ComposeAsync();
        Task ComposeAsync(string subject, string body, params string[] to);
        Task ComposeAsync(EmailMessage message);
    }
    public interface IFileSystem
    {
        Task<Stream> OpenAppPackageFileAsync(string filename);
        string CacheDirectory { get; }
        string AppDataDirectory { get; }
    }
    public interface IFlashlight
    {
        Task TurnOnAsync();
        Task TurnOffAsync();
    }
    public interface IGeocoding
    {
        Task<IEnumerable<Placemark>> GetPlacemarksAsync(Location location);
        Task<IEnumerable<Placemark>> GetPlacemarksAsync(double latitude, double longitude);
        Task<IEnumerable<Location>> GetLocationsAsync(string address);
    }
    public interface IGeolocation
    {
        Task<Location> GetLastKnownLocationAsync();
        Task<Location> GetLocationAsync();
        Task<Location> GetLocationAsync(GeolocationRequest request);
        Task<Location> GetLocationAsync(GeolocationRequest request, CancellationToken cancelToken);
    }
    public interface IGyroscope
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
    public interface ILauncher
    {
        Task<bool> CanOpenAsync(string uri);
        Task<bool> CanOpenAsync(Uri uri);
        Task OpenAsync(string uri);
        Task OpenAsync(Uri uri);
    }
    public interface IMagnetometer
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
    public interface IMainThread
    {
        void BeginInvokeOnMainThread(Action action);
        bool IsMainThread { get; }
    }
    public interface IMap
    {
        Task OpenAsync(Location location);
        Task OpenAsync(Location location, MapLaunchOptions options);
        Task OpenAsync(double latitude, double longitude);
        Task OpenAsync(double latitude, double longitude, MapLaunchOptions options);
        Task OpenAsync(Placemark placemark);
        Task OpenAsync(Placemark placemark, MapLaunchOptions options);
    }
    public interface IOrientationSensor
    {
        void Start(SensorSpeed sensorSpeed);
        void Stop();
        bool IsMonitoring { get; }
    }
    public interface IPhoneDialer
    {
        void Open(string number);
    }
    public interface IPreferences
    {
        bool ContainsKey(string key);
        void Remove(string key);
        void Clear();
        string Get(string key, string defaultValue);
        bool Get(string key, bool defaultValue);
        int Get(string key, int defaultValue);
        double Get(string key, double defaultValue);
        float Get(string key, float defaultValue);
        long Get(string key, long defaultValue);
        void Set(string key, string value);
        void Set(string key, bool value);
        void Set(string key, int value);
        void Set(string key, double value);
        void Set(string key, float value);
        void Set(string key, long value);
        bool ContainsKey(string key, string sharedName);
        void Remove(string key, string sharedName);
        void Clear(string sharedName);
        string Get(string key, string defaultValue, string sharedName);
        bool Get(string key, bool defaultValue, string sharedName);
        int Get(string key, int defaultValue, string sharedName);
        double Get(string key, double defaultValue, string sharedName);
        float Get(string key, float defaultValue, string sharedName);
        long Get(string key, long defaultValue, string sharedName);
        void Set(string key, string value, string sharedName);
        void Set(string key, bool value, string sharedName);
        void Set(string key, int value, string sharedName);
        void Set(string key, double value, string sharedName);
        void Set(string key, float value, string sharedName);
        void Set(string key, long value, string sharedName);
        DateTime Get(string key, DateTime defaultValue);
        void Set(string key, DateTime value);
        DateTime Get(string key, DateTime defaultValue, string sharedName);
        void Set(string key, DateTime value, string sharedName);
    }
    public interface ISecureStorage
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, string value);
        bool Remove(string key);
        void RemoveAll();
    }
    public interface IShare
    {
        Task RequestAsync(string text);
        Task RequestAsync(string text, string title);
        Task RequestAsync(ShareTextRequest request);
    }
    public interface ISms
    {
        Task ComposeAsync();
        Task ComposeAsync(SmsMessage message);
    }
    public interface ITextToSpeech
    {
        Task<IEnumerable<Locale>> GetLocalesAsync();
        Task SpeakAsync(string text, CancellationToken cancelToken = default);
        Task SpeakAsync(string text, SpeechOptions options, CancellationToken cancelToken = default);
    }
    public interface IVersionTracking
    {
        void Track();
        bool IsFirstLaunchForVersion(string version);
        bool IsFirstLaunchForBuild(string build);
        bool IsFirstLaunchEver { get; }
        bool IsFirstLaunchForCurrentVersion { get; }
        bool IsFirstLaunchForCurrentBuild { get; }
        string CurrentVersion { get; }
        string CurrentBuild { get; }
        string PreviousVersion { get; }
        string PreviousBuild { get; }
        string FirstInstalledVersion { get; }
        string FirstInstalledBuild { get; }
        IEnumerable<string> VersionHistory { get; }
        IEnumerable<string> BuildHistory { get; }
    }
    public interface IVibration
    {
        void Vibrate();
        void Vibrate(double duration);
        void Vibrate(TimeSpan duration);
        void Cancel();
    }
}
