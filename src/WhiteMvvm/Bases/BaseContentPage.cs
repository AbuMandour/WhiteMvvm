using System;
using System.Globalization;
using System.Reflection;
using Unity;
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
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(BaseViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);
        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                if (!(bindable is Element view))
                {
                    return;
                }
                var viewType = view.GetType();
                var viewName = viewType.Name.Replace("Page", "ViewModel");
                if (viewType.Namespace == null)
                    return;
                var namespaceName = viewType.Namespace.Replace("Views", "ViewModels");
                var viewFullName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, viewName);
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewFullName, viewAssemblyName);
                var viewModelType = Type.GetType(viewModelName);
                if (viewModelType == null)
                {
                    return;
                }
                var viewModel = BaseViewModelLocator.Resolve(viewModelType);
                view.BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw new Exception("Couldn't wire view model to page");
            }
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
