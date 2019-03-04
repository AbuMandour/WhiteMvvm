using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WhiteMvvm;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Bases;
using WhiteMvvmUnitTest.Mocks;
using WhiteMvvmUnitTest.Mocks.ViewModels;
using WhiteMvvmUnitTest.Mocks.Views;
using Xamarin.Forms;

namespace WhiteMvvmUnitTest.Services
{
    [TestClass]
    public class NavigationServicesTest : BaseTest
    {
        [TestMethod]
        public async Task InitializeAppNavigationWithContentPage()
        {
            //Arrange                      
            //Act
            await WhiteApp.InitializeApp<HomeViewModel>();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault(x => x.BindingContext.GetType() == typeof(HomeViewModel));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public async Task InitializeAppNavigationWithTabbedPage()
        {
            //Arrange                      
            var pageContainers = new List<PageContainer>
            {
                new PageContainer()
                {
                    IsNavigationPage = true, Parameter = "Home Page", ViewModel = new HomeViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page One", ViewModel = new View1ViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page two", ViewModel = new View2ViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page three", ViewModel = new View3ViewModel()
                }
            };
            //Act
            await WhiteApp.InitializeApp(pageContainers);
            //Assert
            var tabbedIsExists = Application.Current.MainPage.GetType() == typeof(TabbedPage);
            Assert.IsTrue(tabbedIsExists);
        }
        [TestMethod]
        public async Task InitializeAppNavigationWithMasterDetialPage()
        {
            //Arrange                      
            var detialPage = new PageContainer() { IsNavigationPage = true, Parameter = "Detail Page", ViewModel = new HomeViewModel(), PageName = "Detail Page" };
            var masterPage = new PageContainer() { IsNavigationPage = false, Parameter = "Master Page", ViewModel = new View1ViewModel(), PageName = "Master Page" };
            Application.Current = new MockApp();
            //Act
            await WhiteApp.InitializeApp(masterPage,detialPage);
            //Assert
            var masterIsExists = Application.Current.MainPage.GetType() == typeof(MasterDetailPage);
            Assert.IsTrue(masterIsExists);
        }
        [TestMethod]
        public void NavigateTo_WithOutParameter()
        {
            //Arrange           
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();            
            //Act
            navigationService.NavigateToAsync<HomeViewModel>();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateTo_WithParameter()
        {
            //Arrange           
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            navigationService.NavigateToAsync<HomeViewModel>("Hello Test");
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            var homeViewModel = (HomeViewModel)homePage?.BindingContext;
            //Assert
            Assert.IsTrue((string)homeViewModel?.NavigationData == "Hello Test");
            Assert.IsNotNull(homePage);            
        }
        [TestMethod]
        public void NavigateToModal_WithOutParameter()
        {
            //Arrange           
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            navigationService.NavigateModalToAsync<HomeViewModel>();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateToModal_WithParameter()
        {
            //Arrange           
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            navigationService.NavigateModalToAsync<HomeViewModel>("Hello Test");
            var homePage = Application.Current.MainPage.Navigation.ModalStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            if (homePage == null)
            {
                homePage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            }
            var homeViewModel = (HomeViewModel)homePage?.BindingContext;
            //Assert
            Assert.IsNotNull(homePage);
            Assert.IsTrue((string)homeViewModel?.NavigationData == "Hello Test");            
        }
        [TestMethod]
        public void NavigateToTabbedPage()
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
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page One",
                    ViewModel = BaseViewModelLocator.Resolve<View1ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page two",
                    ViewModel = BaseViewModelLocator.Resolve<View2ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page three",
                    ViewModel = BaseViewModelLocator.Resolve<View3ViewModel>()
                }
            };
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            var initializeAsyncTask = WhiteApp.InitializeApp<HomeViewModel>();
            var navigateToTabbedAsyncTask = navigationService.NavigateToTabbedAsync(pageContainers);
            var isTabbedExists = ModalStack.Any(x => x.GetType() == typeof(TabbedPage)) ||
                                 NavigationStack.Any(x => x.GetType() == typeof(TabbedPage));

            //Assert
            Assert.IsTrue(initializeAsyncTask.IsCompleted);
            Assert.IsTrue(navigateToTabbedAsyncTask.IsCompleted);
            Assert.IsTrue(isTabbedExists);
        }
        [TestMethod]
        public void NavigateToMasterDetailPage()
        {
            //Arrange
            var masterPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Master Page",
                ViewModel = BaseViewModelLocator.Resolve<HomeViewModel>()
            };
            var detailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Detail Page",
                ViewModel = BaseViewModelLocator.Resolve<HomeViewModel>()
            }; ;
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            var initializeAsyncTask = WhiteApp.InitializeApp<HomeViewModel>();
            var pushMasterDetailsTask = navigationService.NavigateToMasterDetailsAsync(masterPage,detailPage);
            var isMasterDetailExists = ModalStack.Any(x => x.GetType() == typeof(MasterDetailPage)) ||
                                       NavigationStack.Any(x => x.GetType() == typeof(MasterDetailPage));
            //Assert
            Assert.IsTrue(initializeAsyncTask.IsCompleted);
            Assert.IsTrue(pushMasterDetailsTask.IsCompleted);
            Assert.IsTrue(isMasterDetailExists);
        }
        [TestMethod]
        public  void ChangeDetailPageInMasterDetailTest()
        {
            //Arrange
            var masterPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Master Page",
                ViewModel = BaseViewModelLocator.Resolve<HomeViewModel>()
            };
            var detailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Detail Page",
                ViewModel = BaseViewModelLocator.Resolve<HomeViewModel>()
            };
            var newDetailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "New Detail Page",
                ViewModel = BaseViewModelLocator.Resolve<HomeViewModel>()
            };
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            //Act
            var task = WhiteApp.InitializeApp(masterPage, detailPage);

            var isDetailPageChanged =  navigationService.ChangeDetailPage(newDetailPage).Result;
            //Assert
            Assert.IsTrue(task.IsCompleted);
            Assert.IsTrue(isDetailPageChanged);
        }
        [TestMethod]
        public void AddTabbedPageOnRunTime()
        {
            //Arrange                      
            var navigationService = BaseViewModelLocator.Resolve<INavigationService>();
            var pageContainers = new List<PageContainer>
            {
                new PageContainer()
                {
                    IsNavigationPage = true, Parameter = "Home Page", ViewModel = new HomeViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page One", ViewModel = new View1ViewModel()
                },
                new PageContainer()
                {
                    IsNavigationPage = false, Parameter = "Page two", ViewModel = new View2ViewModel()
                }                
            };
            var newPage = new PageContainer()
            {
                IsNavigationPage = false,
                Parameter = "Page three",
                ViewModel = new View3ViewModel()
            };
            //Act
            var initializeAsyncTask = WhiteApp.InitializeApp(pageContainers);            
            var isPageAdded = navigationService.AddPageToTabbedPage(newPage).Result;
            //Assert
            Assert.IsTrue(isPageAdded);
            Assert.IsTrue(initializeAsyncTask.IsCompleted);
        }        
    }
}
