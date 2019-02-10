namespace WhiteMvvm.iOS
{
    public class PlatformiOS
    {
        internal static bool IsInitialized { get; private set; }
        public static void Init()
        {
            IsInitialized = true;
            Rg.Plugins.Popup.Popup.Init();
        }
    }
}
