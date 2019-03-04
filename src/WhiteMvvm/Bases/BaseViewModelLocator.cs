using System;
using System.Collections.Generic;
using Unity;
using Unity.Lifetime;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Services.DeviceUtilities;
using Xamarin.Forms;
using WhiteMvvm.Services.Dialog;
using static WhiteMvvm.Services.DeviceUtilities.Mocks;
using System.Threading.Tasks;

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
