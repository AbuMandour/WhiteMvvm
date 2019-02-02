using System;
using System.Collections.Generic;
using System.Text;
using WhiteSolution.ViewModels.Bases;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;


namespace WhiteSolution.Views.Bases

{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        public BaseViewModel ViewModel { get => this.BindingContext as BaseViewModel; }
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
