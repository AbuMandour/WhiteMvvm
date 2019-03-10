using System;
using System.Collections.Generic;
using System.Linq;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Services.Resolve;
using Xamarin.Forms;

namespace WhiteMvvm
{
    public class WhiteApplication : Application
    {
        private IReflectionResolve _resolve;        
        public WhiteApplication()
        {
            
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="isNavigationPage"></param>
        /// <returns></returns>
        public void SetHomePage<TViewModel>(object parameter = null, bool isNavigationPage = false)
        {
            _resolve = BaseLocator.Instance.Resolve<IReflectionResolve>();
            var page = _resolve.CreatePage(typeof(TViewModel));
            Application.Current.MainPage = isNavigationPage ? new NavigationPage(page) : page;
            if (page == null)
                return;
            if (page.BindingContext is BaseViewModel viewModel)
            {
                viewModel.InternalInitialize(parameter);
            }
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <param name="pageContainers"></param>
        /// <param name="tabbedPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>

        public void SetHomePage(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null, bool hasNavBar = false)
        {
            _resolve = BaseLocator.Instance.Resolve<IReflectionResolve>();
            if (pageContainers.Count <= 1)
                return;
            if (tabbedPage == null)
            {
                tabbedPage = new TabbedPage();
            }
            tabbedPage.CurrentPageChanged += (sender, args) =>
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
                viewModel.InternalInitialize(parameter);
            };
            foreach (var pageContainer in pageContainers)
            {
                var viewModelType = pageContainer.ViewModel;
                var page = _resolve.CreatePage(viewModelType.GetType());
                tabbedPage.Children.Add(pageContainer.IsNavigationPage ? new NavigationPage(page) : page);
            }
            Application.Current.MainPage = tabbedPage;
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <param name="master"></param>
        /// <param name="detail"></param>
        /// <param name="masterDetailPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>

        public void SetHomePage(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false)
        {
            _resolve = BaseLocator.Instance.Resolve<IReflectionResolve>();
            if (master == null || detail == null)
                throw new ArgumentNullException();
            master.IsNavigationPage = true;
            var masterViewModel = master.ViewModel;
            var masterPage = _resolve.CreatePage(masterViewModel.GetType());
            if (string.IsNullOrEmpty(masterPage.Title))
            {
                masterPage.Title = "Master Title";
            }
            var detailViewModel = detail.ViewModel;
            var detailPage = _resolve.CreatePage(detailViewModel.GetType());

            if (masterDetailPage == null)
                masterDetailPage = new MasterDetailPage();
            masterDetailPage.Master = masterPage;
            masterDetailPage.Detail = detail.IsNavigationPage ? new NavigationPage(detailPage) : detailPage;

            Application.Current.MainPage = masterDetailPage;

            masterViewModel.Initialize(master.Parameter);
            detailViewModel.Initialize(detail.Parameter);
        }

    }
}
