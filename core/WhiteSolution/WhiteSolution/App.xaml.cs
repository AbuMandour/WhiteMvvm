using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using WhiteSolution.ViewModels.Bases;
using WhiteSolution.Services.Navigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WhiteSolution
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            // Add your app center keys here
            AppCenter.Start("ios=8f0414c4-0cd9-4728-be93-62f364100c37;" +
                            "android=android=a2d9ea6e-cae7-4299-81ff-b1a185524dfb;", typeof(Analytics), typeof(Crashes));
            await InitNavigation();
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
