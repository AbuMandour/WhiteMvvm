using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using Unity.Lifetime;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Services.DeviceUtilities;
using Xamarin.Forms;
using System.Globalization;
using System.Reflection;
using WhiteMvvm.Services.Dialog;
using static WhiteMvvm.Services.DeviceUtilities.Mocks;
using System.Threading.Tasks;

namespace WhiteMvvm.Bases
{
    public class BaseViewModelLocator
    {    
        private static UnityContainer _container;
        static BaseViewModelLocator()
        {
            _container = new UnityContainer();
            _container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISecureStorage, SecureStorageService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IConnectivity, ConnectivityService>(new ContainerControlledLifetimeManager());

        }
        public static readonly BindableProperty AutoWireViewModelProperty =
                BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(BaseViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);
        public static void UpdateDependenciesinternal(bool useMockServices)
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
        public static Task InitializeNavigation<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            var navigationService = Resolve<INavigationService>();
            return navigationService.InitializeAsync<TViewModel>(parameter);
        }
        public static Task InitializeNavigation(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null) 
        {
            var navigationService = Resolve<INavigationService>();
            return navigationService.InitializeAsync(pageContainers,tabbedPage);
        }
        public static Task InitializeNavigation(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false)
        {
            var navigationService = Resolve<INavigationService>();
            return navigationService.InitializeAsync( master,detail,masterDetailPage,hasNavBar);
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
                var viewFullName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, viewName);
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName =  string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewFullName, viewAssemblyName);
                var viewModelType = Type.GetType(viewModelName);

                if (viewModelType == null)
                {
                    return;
                }
                var viewModel = _container.Resolve(viewModelType);
                view.BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
        public static  T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
        public static void Register<TFrom, TTo>() where   TTo : TFrom
        {
             _container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
        public static void Register<T>()
        {
            _container.RegisterType<T>(new ContainerControlledLifetimeManager());
        }
        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }
        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }
        public virtual void UpdateDependancies(bool useMockServices)
        {
            UpdateDependenciesinternal(useMockServices);
        }
    }
}
