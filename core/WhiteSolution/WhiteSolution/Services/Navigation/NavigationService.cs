using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WhiteSolution.ViewModels;
using WhiteSolution.ViewModels.Bases;
using Xamarin.Forms;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace WhiteSolution.Services.Navigation
{
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
        /// <returns></returns>
        public Task NavigateModalToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            return InternalNavigateModalToAsync(typeof(TViewModel), parameter);
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin or navigation 
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            #region TabbedPage
            //var PageContainers = new List<PageContainer>();
            //PageContainers.Add(new PageContainer() { IsNavigationPage = true, Parameter = "Home Page", ViewModel = ViewModelLocator.Resolve<HomeViewModel>() });
            //PageContainers.Add(new PageContainer() { IsNavigationPage = false, Parameter = "Page One", ViewModel = ViewModelLocator.Resolve<View1ViewModel>() });
            //PageContainers.Add(new PageContainer() { IsNavigationPage = false, Parameter = "Page two", ViewModel = ViewModelLocator.Resolve<View2ViewModel>() });
            //PageContainers.Add(new PageContainer() { IsNavigationPage = false, Parameter = "Page three", ViewModel = ViewModelLocator.Resolve<View3ViewModel>() });
            //var tabbedpage = new Xamarin.Forms.TabbedPage();
            //tabbedpage.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            //return NavigateToTabbedAsync(PageContainers, tabbedpage);

            #endregion

            #region MasterDetials
            //var homePage = new PageContainer() { IsNavigationPage = true, Parameter = "Home Page", ViewModel = ViewModelLocator.Resolve<HomeViewModel>(), PageName = "Home Page" };
            //var pageOne = new PageContainer() { IsNavigationPage = false, Parameter = "Page One", ViewModel = ViewModelLocator.Resolve<View1ViewModel>(), PageName = "Page One" };
            //var pageTwo = new PageContainer() { IsNavigationPage = false, Parameter = "Page Two", ViewModel = ViewModelLocator.Resolve<View2ViewModel>(), PageName = "Page Two" };
            //var pageThree = new PageContainer() { IsNavigationPage = false, Parameter = "Page three", ViewModel = ViewModelLocator.Resolve<View3ViewModel>(), PageName = "Page Three" };
            //var masterPage = new PageContainer() { IsNavigationPage = true, Parameter = new ObservableRangeCollection<PageContainer>() { homePage, pageOne, pageTwo, pageThree }, ViewModel = ViewModelLocator.Resolve<MasterViewModel>() };
            //return NavigateToMasterDetailsAsync(masterPage, homePage);
            #endregion
            await NavigateToAsync<HomeViewModel>();

        }
        /// <summary>
        /// async method to navigate to tabbed page which take list of page container class and tabbed page as optional parameter
        /// </summary>
        /// <param name="pageContainers"></param>
        /// <param name="tabbedPage"></param>
        /// <returns></returns>
        public async Task NavigateToTabbedAsync(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null)
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
                var navigation = Application.Current.MainPage;
                if (navigation == null || navigation.Navigation.ModalStack.Count <= 0)
                {
                    Application.Current.MainPage = tabbedPage;
                }
                else
                {
                    await navigation.Navigation.PushModalAsync(tabbedPage);
                }
            }
        }
        /// <summary>
        /// async method to navigate to master detail page with master and detail page as parameter also if you want to keep navigation bar or not as optional parameter also if you want to use custom master detail page
        /// </summary>
        /// <param name="master"></param>
        /// <param name="detail"></param>
        /// <param name="masterDetailPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>
        public async Task NavigateToMasterDetailsAsync(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false)
        {
            if (master == null || detail == null)
                throw new ArgumentNullException();
            master.IsNavigationPage = true;
            var masterViewModel = master.ViewModel;
            var masterPage = CreatePage(masterViewModel.GetType());

            var detailViewModel = detail.ViewModel;
            var detailPage = CreatePage(detailViewModel.GetType());

            if (masterDetailPage == null)
                masterDetailPage = new MasterDetailPage();
            masterDetailPage.Master = masterPage;
            masterDetailPage.Detail = detailPage;
            masterDetailPage.ShouldShowToolbarButton();
            masterDetailPage.Detail = detail.IsNavigationPage ? new NavigationPage(detailPage) : detailPage;

            var navigation = Application.Current.MainPage;
            if (navigation == null || navigation.Navigation.ModalStack.Count <= 0)
            {
                if (hasNavBar)
                {
                    Application.Current.MainPage = new NavigationPage(masterDetailPage);
                }
                else
                {
                    Application.Current.MainPage = masterDetailPage;
                }
            }
            else
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
            await masterViewModel.InitializeAsync(master.Parameter);
            await detailViewModel.InitializeAsync(detail.Parameter);
        }
        /// <summary>
        /// method to change details page with another on take one parameter page container
        /// </summary>
        /// <param name="pageContainer"></param>
        /// <returns></returns>
        public async Task ChangeDetailPage(PageContainer pageContainer)
        {
            var modalStack = Application.Current.MainPage.Navigation.ModalStack;
            var navigationStack = Application.Current.MainPage.Navigation.NavigationStack;
            var currentMasterDetailPage = Application.Current.MainPage;
            if (currentMasterDetailPage is MasterDetailPage baseMasterDetail)
            {
                var viewModelType = pageContainer.ViewModel;
                var page = CreatePage(viewModelType.GetType());
                baseMasterDetail.Detail = pageContainer.IsNavigationPage ? new NavigationPage(page) : page;
                await pageContainer.ViewModel.InitializeAsync(pageContainer.Parameter);
                baseMasterDetail.IsPresented = false;
                return;
            }
            currentMasterDetailPage = modalStack.FirstOrDefault(x => x.GetType() == typeof(MasterDetailPage));
            if (currentMasterDetailPage is MasterDetailPage masterDetailModal)
            {
                var viewModelType = pageContainer.ViewModel;
                var page = CreatePage(viewModelType.GetType());
                masterDetailModal.Detail = pageContainer.IsNavigationPage ? new NavigationPage(page) : page;
                await pageContainer.ViewModel.InitializeAsync(pageContainer.Parameter);
                masterDetailModal.IsPresented = false;
            }
            currentMasterDetailPage = navigationStack.FirstOrDefault(x => x.GetType() == typeof(MasterDetailPage));
            if (currentMasterDetailPage is MasterDetailPage masterDetailNavigation)
            {
                var viewModelType = pageContainer.ViewModel;
                var page = CreatePage(viewModelType.GetType());
                masterDetailNavigation.Detail = pageContainer.IsNavigationPage ? new NavigationPage(page) : page;
                await pageContainer.ViewModel.InitializeAsync(pageContainer.Parameter);
                masterDetailNavigation.IsPresented = false;
            }
        }
        /// <summary>
        /// method to add tabbed page in run time take one parameter page container
        /// </summary>
        /// <param name="pageContainer"></param>
        /// <returns></returns>
        public async Task AddPageToTabbedPage(PageContainer pageContainer)
        {
            var modalStack = Application.Current.MainPage.Navigation.ModalStack;
            var currentTabbedPage = modalStack.FirstOrDefault(x => x.GetType() == typeof(TabbedPage));
            if (currentTabbedPage is TabbedPage tabbedPage)
            {
                var viewModelType = pageContainer.ViewModel;
                var page = CreatePage(viewModelType.GetType());
                tabbedPage.Children.Add(pageContainer.IsNavigationPage ? new NavigationPage(page) : page);
                await pageContainer.ViewModel.InternalInitializeAsync(pageContainer.Parameter);
            }
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
        private async Task InternalNavigateModalToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType);

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                await navigationPage.Navigation.PushModalAsync(page);
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
        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.Name.Replace("ViewModel", "Page");
            if (viewModelType.Namespace == null)
                return null;
            var namespaceName = viewModelType.Namespace.Replace("ViewModels", "Views");
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", namespaceName, viewName);
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
