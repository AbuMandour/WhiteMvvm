using System;
using System.Globalization;
using System.Reflection;
using WhiteMvvm.Configuration;
using WhiteMvvm.Services.Resolve;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;


namespace WhiteMvvm.Bases

{
    public class BaseContentPage : ContentPage
    {
        private static IReflectionResolve _resolve;
        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            _resolve = BaseLocator.Instance.Resolve<IReflectionResolve>();
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
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(BaseContentPage), default(bool),
                propertyChanged: OnAutoWireViewModelChanged);
        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((!(bindable is BaseContentPage page)))
                return;
            page.BindingContext = _resolve.CreateViewModel(page.GetType());
        }
        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }
        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }
    }
}
