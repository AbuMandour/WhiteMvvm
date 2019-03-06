using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace WhiteMvvm.Services.Navigation
{
    public interface INavigationService
    {
        INavigation Navigation { get; }
        BaseViewModel PreviousPageViewModel { get; }
        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
        Task NavigateModalToAsync<TViewModel>(object parameter = null, bool isNavigationPage = false) where TViewModel : BaseViewModel;
        Task NavigateToTabbedAsync(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null, bool hasNavBar = false);
        Task<bool> AddPageToTabbedPage(PageContainer pageContainer);
        Task NavigateToMasterDetailsAsync(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false);
        Task<bool> ChangeDetailPage(PageContainer pageContainer);
    }
}
