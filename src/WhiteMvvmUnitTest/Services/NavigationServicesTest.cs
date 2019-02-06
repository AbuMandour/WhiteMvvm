using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WhiteMvvm;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Bases;
using WhiteMvvmUnitTest.Mocks;
using Xamarin.Forms;

namespace WhiteMvvmUnitTest.Services
{
    [TestClass]
    public class NavigationServicesTest : BaseTest
    {
        [TestMethod]
        public async Task InitializeAppNavigation()
        {
            //Arange                      
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            //await navigationService.InitializeAsync();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.Where(x => x.BindingContext.GetType() == typeof(HomeViewModel));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateTo_WithOutParameter()
        {
            //Arrange           
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();            
            //Act
            navigationService.NavigateToAsync<HomeViewModel>(null);
            //Assert
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(x => x.GetType() == typeof(HomePage));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateTo_WithParameter()
        {
            //Arrange           
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            navigationService.NavigateToAsync<HomeViewModel>("Hello Test");
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(x => x.GetType() == typeof(HomePage));
            var homeViewModel = BaseViewModelLocator.Resolve<HomeViewModel>();
            //Assert
            Assert.IsTrue((string)homeViewModel.NavigationData == "Hello Test");
            Assert.IsNotNull(homePage);
            
        }
        [TestMethod]
        public void NavigateToTabbedPage()
        {
            //Arrange
            BaseViewModelLocator.UpdateDependencies(true);
            var pageContainers = new List<PageContainer>
            {
                new PageContainer()
                {
                    IsNavigationPage = true,
                    Parameter = "Home Page",
                    ViewModel = BaseViewModelLocator.Resolve<HomeViewModel>()
                },
                //new PageContainer()
                //{
                //    IsNavigationPage = false,
                //    Parameter = "Page One",
                //    ViewModel = BaseViewModelLocator.Resolve<View1ViewModel>()
                //},
                //new PageContainer()
                //{
                //    IsNavigationPage = false,
                //    Parameter = "Page two",
                //    ViewModel = BaseViewModelLocator.Resolve<View2ViewModel>()
                //},
                //new PageContainer()
                //{
                //    IsNavigationPage = false,
                //    Parameter = "Page three",
                //    ViewModel = BaseViewModelLocator.Resolve<View3ViewModel>()
                //}
            };
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            var task = navigationService.NavigateToTabbedAsync(pageContainers);
            //Assert
            Assert.IsTrue(task.IsCompleted);
            var tabbedIsExists = ModalStack.Any(x => x.GetType() == typeof(TabbedPage)) ||
                                 NavigationStack.Any(x => x.GetType() == typeof(TabbedPage)) || 
                                 Application.Current.MainPage.GetType() == typeof(TabbedPage);
            Assert.IsTrue(tabbedIsExists);
        }
        [TestMethod]
        public void NavigateToMasterDetailPage()
        {
            //Arrange
            var pageContainers = new List<PageContainer>
            {
                new PageContainer()
                {
                    IsNavigationPage = true,
                    Parameter = "Home Page",
                    ViewModel = BaseViewModelLocator.Resolve<HomeViewModel>()
                },
                //new PageContainer()
                //{
                //    IsNavigationPage = false,
                //    Parameter = "Page One",
                //    ViewModel = BaseViewModelLocator.Resolve<View1ViewModel>()
                //},
                //new PageContainer()
                //{
                //    IsNavigationPage = false,
                //    Parameter = "Page two",
                //    ViewModel = BaseViewModelLocator.Resolve<View2ViewModel>()
                //},
                //new PageContainer()
                //{
                //    IsNavigationPage = false,
                //    Parameter = "Page three",
                //    ViewModel = BaseViewModelLocator.Resolve<View3ViewModel>()
                //}
            };
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            var task = navigationService.NavigateToTabbedAsync(pageContainers);
            //Assert
            Assert.IsTrue(task.IsCompleted);
        }
        [TestMethod]
        public void ChangeNavigationPageDetailsTest()
        {

        }
        [TestMethod]
        public void AddTabbedPageOnRunTime()
        {

        }
        [TestCleanup]
        public override void CleanUpTest()
        {
            BaseViewModelLocator.UpdateDependencies(false);
        }
    }
}
