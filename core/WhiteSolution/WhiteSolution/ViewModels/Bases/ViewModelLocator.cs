using System;
using Unity;
using Unity.Lifetime;
using WhiteSolution.Services.Home;
using WhiteSolution.Services.Navigation;
using WhiteSolution.Services.DeviceUtilities;
using Xamarin.Forms;
using System.Globalization;
using WhiteSolution.Services.Dialog;
using Microsoft.AppCenter.Crashes;
using static WhiteSolution.Services.DeviceUtilities.Mocks;

namespace WhiteSolution.ViewModels.Bases
{
    public class ViewModelLocator
    {
        private static UnityContainer _container;
        static ViewModelLocator()
        {
            _container = new UnityContainer();
            _container.RegisterType<HomeViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<MasterViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<View3ViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<View2ViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<View1ViewModel>(new ContainerControlledLifetimeManager());


            _container.RegisterType<IHomeService, HomeService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISecureStorage, SecureStorageService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IConnectivity, ConnectivityService>(new ContainerControlledLifetimeManager());

        }
        public static readonly BindableProperty AutoWireViewModelProperty =
                BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);
        public static void UpdateDependencies(bool useMockServices)
        {
            // Change injected dependencies
            if (useMockServices)
            {
                _container.RegisterType<IConnectivity, ConnectivityMockService>(new ContainerControlledLifetimeManager());                
                _container.RegisterType<IDialogService,DialogMockService >(new ContainerControlledLifetimeManager());
            }
            else
            {
                _container.RegisterType<IConnectivity, ConnectivityService>(new ContainerControlledLifetimeManager());
                _container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            }
        }

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
                var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, viewName);
                var viewModelType = Type.GetType(viewAssemblyName);

                if (viewModelType == null)
                {
                    return;
                }
                var viewModel = _container.Resolve(viewModelType);
                view.BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
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
