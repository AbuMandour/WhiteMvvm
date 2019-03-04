using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.Navigation;
using Xamarin.Forms;

namespace WhiteMvvm
{
    public class WhiteApp : Application
    {
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter"></param>
        /// <param name="isNavigationPage"></param>
        /// <returns></returns>

        public static Task InitializeApp<TViewModel>(object parameter = null, bool isNavigationPage = true) where TViewModel : BaseViewModel
        {
            var navigationService = BaseViewModelLocator.Container.Resolve<INavigationService>();
            return navigationService.NavigateModalToAsync<TViewModel>(parameter, isNavigationPage);
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <param name="pageContainers"></param>
        /// <param name="tabbedPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>

        public static Task InitializeApp(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null, bool hasNavBar = true)
        {
            var navigationService = BaseViewModelLocator.Container.Resolve<INavigationService>();
            return navigationService.NavigateToTabbedAsync(pageContainers, tabbedPage, hasNavBar);
        }
        /// <summary>
        /// method to Initialize Navigation service in app class where we can write how we will begin app navigation 
        /// </summary>
        /// <param name="master"></param>
        /// <param name="detail"></param>
        /// <param name="masterDetailPage"></param>
        /// <param name="hasNavBar"></param>
        /// <returns></returns>

        public static Task InitializeApp(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false)
        {
            var navigationService = BaseViewModelLocator.Container.Resolve<INavigationService>();
            return navigationService.NavigateToMasterDetailsAsync(master, detail, masterDetailPage, hasNavBar);
        }
    }
}
