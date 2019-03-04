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
        public static Task InitializeApp<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            var navigationService = BaseViewModelLocator.Container.Resolve<INavigationService>();
            return navigationService.InitializeAsync<TViewModel>(parameter);
        }
        public static Task InitializeApp(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null)
        {
            var navigationService = BaseViewModelLocator.Container.Resolve<INavigationService>();
            return navigationService.InitializeAsync(pageContainers, tabbedPage);
        }
        public static Task InitializeApp(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false)
        {
            var navigationService = BaseViewModelLocator.Container.Resolve<INavigationService>();
            return navigationService.InitializeAsync(master, detail, masterDetailPage, hasNavBar);
        }
    }
}
