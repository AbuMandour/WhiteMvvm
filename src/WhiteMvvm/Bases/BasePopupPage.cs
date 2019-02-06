using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Bases
{
    public class BasePopupPage : PopupPage
    {
        public BaseViewModel ViewModel => this.BindingContext as BaseViewModel;

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
