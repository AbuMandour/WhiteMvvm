using Rg.Plugins.Popup.Pages;

namespace WhiteMvvm.Bases
{
    public class BasePopupPage : PopupPage
    {
        public BaseViewModel ViewModel => BindingContext as BaseViewModel;

        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
            ViewModel?.OnPopupAppearing();
        }
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();                
            ViewModel?.OnPopupDisappearing();
        }
    }
}
