using System;
using Unity;
using Unity.Lifetime;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Services.Dialog;
using static WhiteMvvm.Services.DeviceUtilities.Mocks;

namespace WhiteMvvm.Bases
{
    public class BaseViewModelLocator
    {
        private static readonly Lazy<UnityContainer> Lazy = new Lazy<UnityContainer>(() => new UnityContainer());
        public static UnityContainer Container => Lazy.Value;
        static BaseViewModelLocator()
        {
            Container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IConnectivity, ConnectivityService>(new ContainerControlledLifetimeManager());
        }
        public static void UpdateDependenciesInternal(bool useMockServices)
        {
            // Change injected dependencies
            if (useMockServices)
            {
                Container.RegisterType<IConnectivity, ConnectivityMockService>(new ContainerControlledLifetimeManager());                
                Container.RegisterType<IDialogService,DialogMockService >(new ContainerControlledLifetimeManager());
            }
            else
            {
                Container.RegisterType<IConnectivity, ConnectivityService>(new ContainerControlledLifetimeManager());
                Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            }
        }
        public static T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }
        public static object Resolve(Type type)
        {
            return Container.Resolve(type);
        }
        public static void Register<TFrom, TTo>() where   TTo : TFrom
        {
             Container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
        public static void Register<T>()
        {
            Container.RegisterType<T>(new ContainerControlledLifetimeManager());
        }
    }
}
