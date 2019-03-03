using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;


namespace WhiteMvvm.Bases

{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        public BaseViewModel ViewModel => BindingContext as BaseViewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel?.OnDisappearing();
        }
        protected override bool OnBackButtonPressed()
        {
            var result = ViewModel.HandleBackButton();
            return result;
        }
    }
}
