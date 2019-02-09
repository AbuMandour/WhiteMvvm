using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace WhiteMvvm.Services.Navigation
{
    public interface INavigationService
    {
        INavigation NavigationStack { get; }
        BaseViewModel PreviousPageViewModel { get; }
        Task InitializeAsync<TViewModel>(object parameter = null, bool isNavigationPage = true)
            where TViewModel : BaseViewModel;
        Task InitializeAsync(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null);
        Task InitializeAsync(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null,
            bool hasNavBar = false);
        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
        Task NavigateModalToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
        Task NavigateToTabbedAsync(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null);
        Task AddPageToTabbedPage(PageContainer pageContainer);
        Task NavigateToMasterDetailsAsync(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false);
        Task ChangeDetailPage(PageContainer pageContainer);
    }
}
