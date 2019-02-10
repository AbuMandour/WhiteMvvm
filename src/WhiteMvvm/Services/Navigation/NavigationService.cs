using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using Xamarin.Forms;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace WhiteMvvm.Services.Navigation
{
    /// <summary>
    /// Navigation service to navigate between pages based on view model 
    /// </summary>
    public class NavigationService : INavigationService
    {
        public INavigation NavigationStack => Application.Current.MainPage?.Navigation;
        /// <summary>
        /// 
        /// </summary>
        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                if (!(Application.Current.MainPage is NavigationPage mainPage))
                    return new BaseViewModel();
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="isNavigationPage"></param>
        /// <returns></returns>
        public Task InitializeAsync<TViewModel>(object parameter = null, bool isNavigationPage = true) where TViewModel : BaseViewModel
        {
            if (!isNavigationPage)
            {
                return InternalNavigateToAsync(typeof(TViewModel), parameter);
            }
            else
            {
                return InternalNavigateModalToAsync(typeof(TViewModel), parameter,true);
            }
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <param name="pageContainers"></param>
        /// <param name="tabbedPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>
        public Task InitializeAsync(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null, bool hasNavBar = true)
        {
            return InternalNavigateToTabbedAsync(pageContainers, true, tabbedPage, hasNavBar);
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <param name="master"></param>
        /// <param name="detail"></param>
        /// <param name="masterDetailPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>
        public Task InitializeAsync(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false)
        {
            return InternalNavigateToMasterDetailsAsync(master, detail,true, masterDetailPage, hasNavBar);
        }
        /// <summary>
        /// generic async method to push page in navigation stack or start app with navigation stack take one optional parameter which will send to view model type you inserted
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// 
        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }
        /// <summary>
        /// generic async method to push page as modal or start app with this page take one optional parameter which will send to view model type you inserted
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="isNavigationPage"></param>
        /// <returns></returns>
        public Task NavigateModalToAsync<TViewModel>(object parameter = null, bool isNavigationPage = false) where TViewModel : BaseViewModel
        {
            return InternalNavigateModalToAsync(typeof(TViewModel), parameter, isNavigationPage);
        }
        /// <summary>
        /// async method to navigate to tabbed page which take list of page container class and tabbed page as optional parameter
        /// </summary>
        /// <param name="pageContainers"></param>
        /// <param name="tabbedPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>
        public Task NavigateToTabbedAsync(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null, bool hasNavBar = false)
        {
            return InternalNavigateToTabbedAsync(pageContainers, false, tabbedPage,hasNavBar);
        }
        /// <summary>
        /// async method to navigate to master detail page with master and detail page as parameter also if you want to keep navigation bar or not as optional parameter also if you want to use custom master detail page
        /// </summary>
        /// <param name="master"></param>
        /// <param name="detail"></param>
        /// <param name="masterDetailPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>
        public Task NavigateToMasterDetailsAsync(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null,
            bool hasNavBar = false)
        {
           return InternalNavigateToMasterDetailsAsync(master, detail, false, masterDetailPage, hasNavBar);
        }
        /// <summary>
        /// method to change details page with another on take one parameter page container
        /// </summary>
        /// <param name="pageContainer"></param>
        /// <returns></returns>
        public async Task<bool> ChangeDetailPage(PageContainer pageContainer)
        {
            try
            {
                if (Application.Current.MainPage == null)
                    return false;

                var modalStack = Application.Current.MainPage.Navigation.ModalStack;
                var navigationStack = Application.Current.MainPage.Navigation.NavigationStack;

                var currentMasterDetailPage = Application.Current.MainPage;
                if (currentMasterDetailPage is MasterDetailPage baseMasterDetail)
                {
                    await InternalChangeDetailPage(pageContainer, baseMasterDetail);
                    return true;
                }

                currentMasterDetailPage = modalStack.FirstOrDefault(x => x.GetType() == typeof(MasterDetailPage));
                if (currentMasterDetailPage is MasterDetailPage modalMasterDetail)
                {
                    await InternalChangeDetailPage(pageContainer, modalMasterDetail);
                    return true;
                }

                currentMasterDetailPage = navigationStack.FirstOrDefault(x => x.GetType() == typeof(MasterDetailPage));
                if (currentMasterDetailPage is MasterDetailPage masterDetailInNavigation)
                {
                    await InternalChangeDetailPage(pageContainer, masterDetailInNavigation);
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }
        /// <summary>
        /// method to add tabbed page in run time take one parameter page container
        /// </summary>
        /// <param name="pageContainer"></param>
        /// <returns>boolean whatever the page added successfully or not</returns>
        public async Task<bool> AddPageToTabbedPage(PageContainer pageContainer)
        {
            try
            {
                if (Application.Current.MainPage == null)
                    return false;

                var modalStack = Application.Current.MainPage.Navigation.ModalStack;
                var navigationStack = Application.Current.MainPage.Navigation.NavigationStack;

                var currentTabbedPage = Application.Current.MainPage;
                if (currentTabbedPage is TabbedPage baseTabbedPage)
                {
                    await InternalAddPageToTabbedPage(pageContainer, baseTabbedPage);
                    return true;
                }

                currentTabbedPage = modalStack.FirstOrDefault(x => x.GetType() == typeof(TabbedPage));
                if (currentTabbedPage is TabbedPage modalTabbedPage)
                {
                    await InternalAddPageToTabbedPage(pageContainer, modalTabbedPage);
                    return true;
                }

                currentTabbedPage = navigationStack.FirstOrDefault(x => x.GetType() == typeof(TabbedPage));
                if (currentTabbedPage is TabbedPage tabbedPageInNavigation)
                {
                    await InternalAddPageToTabbedPage(pageContainer, tabbedPageInNavigation);
                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }

            Console.WriteLine("there is not any tabbed page in navigation stack");
            return false;

        }
        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {

            var page = CreatePage(viewModelType);

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }

            if (page != null)
            {
                if (page.BindingContext is BaseViewModel viewModel)
                {
                    await viewModel.InternalInitializeAsync(parameter);
                }

            }
        }
        private async Task InternalNavigateModalToAsync(Type viewModelType, object parameter,bool isNavigationPage)
        {
            var page = CreatePage(viewModelType);

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.Navigation.PushModalAsync(isNavigationPage ? new NavigationPage(page) : page);
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(page);
            }

            if (page != null)
            {
                if (page.BindingContext is BaseViewModel viewModel)
                {
                    await viewModel.InternalInitializeAsync(parameter);
                }

            }
        }
        private async Task InternalNavigateToMasterDetailsAsync(PageContainer master, PageContainer detail, bool isRoot, MasterDetailPage masterDetailPage = null, bool hasNavBar = false)
        {
            if (master == null || detail == null)
                throw new ArgumentNullException();
            master.IsNavigationPage = true;
            var masterViewModel = master.ViewModel;
            var masterPage = CreatePage(masterViewModel.GetType());
            if (string.IsNullOrEmpty(masterPage.Title))
            {
                masterPage.Title = "Master Title";
            }
            var detailViewModel = detail.ViewModel;
            var detailPage = CreatePage(detailViewModel.GetType());

            if (masterDetailPage == null)
                masterDetailPage = new MasterDetailPage();
            masterDetailPage.Master = masterPage;
            masterDetailPage.Detail = detail.IsNavigationPage ? new NavigationPage(detailPage) : detailPage;

            if (!isRoot)
            {
                var navigation = Application.Current.MainPage;
                if (navigation != null)
                {
                    if (hasNavBar)
                    {
                        await navigation.Navigation.PushModalAsync(new NavigationPage(masterDetailPage));
                    }
                    else
                    {
                        await navigation.Navigation.PushModalAsync(masterDetailPage);
                    }
                }
            }
            else
            {
                Application.Current.MainPage = masterDetailPage;
            }
            await masterViewModel.InitializeAsync(master.Parameter);
            await detailViewModel.InitializeAsync(detail.Parameter);
        }
        private async Task InternalChangeDetailPage(PageContainer pageContainer, MasterDetailPage masterDetail)
        {
            var viewModelType = pageContainer.ViewModel;
            var page = CreatePage(viewModelType.GetType());
            masterDetail.Detail = pageContainer.IsNavigationPage ? new NavigationPage(page) : page;
            await pageContainer.ViewModel.InitializeAsync(pageContainer.Parameter);
            masterDetail.IsPresented = false;
        }
        private async Task InternalAddPageToTabbedPage(PageContainer pageContainer,TabbedPage tabbedPage)
        {
            var viewModelType = pageContainer.ViewModel;
            var page = CreatePage(viewModelType.GetType());
            tabbedPage.Children.Add(pageContainer.IsNavigationPage ? new NavigationPage(page) : page);
            await pageContainer.ViewModel.InternalInitializeAsync(pageContainer.Parameter);
        }
        private async Task InternalNavigateToTabbedAsync(IList<PageContainer> pageContainers, bool isRoot, TabbedPage tabbedPage = null, bool hasNavBar = true)
        {
            if (pageContainers.Count > 1)
            {
                if (tabbedPage == null)
                {
                    tabbedPage = new TabbedPage();
                }
                tabbedPage.CurrentPageChanged += async (sender, args) =>
                {
                    var currentPage = ((TabbedPage)sender).CurrentPage;
                    if (currentPage is NavigationPage navigationPage)
                    {
                        var firstPage = navigationPage.Navigation.NavigationStack.FirstOrDefault();
                        if (firstPage == null)
                            return;
                        currentPage = firstPage;
                    }
                    if (!(currentPage.BindingContext is BaseViewModel viewModel))
                        return;
                    var pageContainer = pageContainers.FirstOrDefault(x => x.ViewModel.GetType() == viewModel.GetType());
                    var parameter = pageContainer?.Parameter;
                    await viewModel.InternalInitializeAsync(parameter);
                };
                foreach (var pageContainer in pageContainers)
                {
                    var viewModelType = pageContainer.ViewModel;
                    var page = CreatePage(viewModelType.GetType());
                    tabbedPage.Children.Add(pageContainer.IsNavigationPage ? new NavigationPage(page) : page);
                }
                if (!isRoot)
                {
                    var navigation = Application.Current.MainPage;
                    if (navigation != null )
                    {
                        await navigation.Navigation.PushModalAsync(hasNavBar? (Page) new NavigationPage(tabbedPage) : tabbedPage);
                    }
                }
                else
                {
                    Application.Current.MainPage = tabbedPage;
                }
            }
        }
        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.Name.Replace("ViewModel", "Page");
            var namespaceName = viewModelType.Namespace.Replace("ViewModels", "Views");
            var viewFullName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, viewName);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewFullName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
        private Page CreatePage(Type viewModelType)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}
