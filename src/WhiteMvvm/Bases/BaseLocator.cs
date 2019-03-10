using System;
using Unity;
using Unity.Lifetime;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Services.Dialog;
using WhiteMvvm.Services.Resolve;
using static WhiteMvvm.Services.DeviceUtilities.Mocks;

namespace WhiteMvvm.Bases
{
    public class BaseLocator
    {
        private static readonly Lazy<BaseLocator> Lazy = new Lazy<BaseLocator>(() => new BaseLocator());
        private UnityContainer _container;

        public static BaseLocator Instance => Lazy.Value;

        //private static readonly Lazy<UnityContainer> LazyContainer = new Lazy<UnityContainer>(() => new UnityContainer());
        //public static UnityContainer Container => LazyContainer.Value;
        public UnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    return _container = new UnityContainer(); ;
                };
                return _container;
            }
        }

        public void RegisterBaseService()
        {
            Container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IConnectivity, ConnectivityService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IReflectionResolve, ReflectionResolve>(new ContainerControlledLifetimeManager());
        }
        protected BaseLocator()
        {
            RegisterBaseService();
        }
        private void MocksUpdateInternal(bool useMocks)
        {
            // Change injected dependencies
            if (useMocks)
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
        public T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }
        public object Resolve(Type type)
        {
            return Container.Resolve(type);
        }
        public void Register<TFrom, TTo>() where   TTo : TFrom
        {
             Container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
        public void Register<T>()
        {
            Container.RegisterType<T>(new ContainerControlledLifetimeManager());
        }

        public virtual void MocksUpdate(bool useMocks)
        {
            MocksUpdateInternal(useMocks);
        }
    }
}
