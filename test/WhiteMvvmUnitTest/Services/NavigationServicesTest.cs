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
        public void InitializeAppNavigationWithContentPage()
        {
            //Arrange   
            var app = new MockApp();
            //Act
            app.SetHomePage<HomeViewModel>();            
            //Assert
            var homePage = app.MainPage.BindingContext.GetType() == typeof(HomeViewModel);
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void InitializeAppNavigationWithTabbedPage()
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
            var app = new MockApp();
            //Act
            app.SetHomePage(pageContainers);
            //Assert
            var tabbedIsExists = app.MainPage.GetType() == typeof(TabbedPage);
            Assert.IsTrue(tabbedIsExists);
        }
        [TestMethod]
        public void InitializeAppNavigationWithMasterDetialPage()
        {
            //Arrange                      
            var detialPage = new PageContainer() { IsNavigationPage = true, Parameter = "Detail Page", ViewModel = new HomeViewModel(), PageName = "Detail Page" };
            var masterPage = new PageContainer() { IsNavigationPage = false, Parameter = "Master Page", ViewModel = new View1ViewModel(), PageName = "Master Page" };
            Application.Current = new MockApp();
            var app = new MockApp();
            //Act
            app.SetHomePage(masterPage,detialPage);
            //Assert
            var masterIsExists = Application.Current.MainPage.GetType() == typeof(MasterDetailPage);
            Assert.IsTrue(masterIsExists);
        }
        [TestMethod]
        public void NavigateTo_WithOutParameter()
        {
            //Arrange   
            var app = new MockApp();
            var navigationService = BaseLocator.Instance.Resolve<INavigationService>();            
            //Act
            app.SetHomePage<View1ViewModel>(isNavigationPage: true);
            navigationService.NavigateToAsync<HomeViewModel>();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateTo_WithParameter()
        {
            //Arrange  
            var app = new MockApp();
            var navigationService = BaseLocator.Instance.Resolve<INavigationService>();
            //Act
            app.SetHomePage<View1ViewModel>(isNavigationPage:true);
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
            var app = new MockApp();
            var navigationService = BaseLocator.Instance.Resolve<INavigationService>();
            //Act
            app.SetHomePage<View1ViewModel>();
            navigationService.NavigateModalToAsync<HomeViewModel>();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.ModalStack.LastOrDefault(x => x.GetType() == typeof(HomePage));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateToModal_WithParameter()
        {
            //Arrange           
            var app = new MockApp();
            var navigationService = BaseLocator.Instance.Resolve<INavigationService>();
            //Act
            app.SetHomePage<View1ViewModel>();
            navigationService.NavigateModalToAsync<HomeViewModel>("Hello Test");
            var homePage = app.MainPage.Navigation.ModalStack.LastOrDefault(x => x.GetType() == typeof(HomePage));           
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
                    ViewModel = BaseLocator.Instance.Resolve<HomeViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page One",
                    ViewModel = BaseLocator.Instance.Resolve<View1ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page two",
                    ViewModel = BaseLocator.Instance.Resolve<View2ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page three",
                    ViewModel = BaseLocator.Instance.Resolve<View3ViewModel>()
                }
            };
            var navigationService = BaseLocator.Instance.Resolve<INavigationService>();
            var app = new MockApp();
            //Act
            app.SetHomePage<HomeViewModel>();
            var navigateToTabbedAsyncTask = navigationService.NavigateToTabbedAsync(pageContainers);
            var isTabbedExists = ModalStack.Any(x => x.GetType() == typeof(TabbedPage)) ||
                                 NavigationStack.Any(x => x.GetType() == typeof(TabbedPage));

            //Assert
            Assert.IsTrue(navigateToTabbedAsyncTask.IsCompleted);
            Assert.IsTrue(isTabbedExists);
        }
        [TestMethod]
        public void NavigateToMasterDetailPage()
        {
            //Arrange
            var app = new MockApp();
            var masterPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Master Page",
                ViewModel = BaseLocator.Instance.Resolve<HomeViewModel>()
            };
            var detailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Detail Page",
                ViewModel = BaseLocator.Instance.Resolve<HomeViewModel>()
            }; ;
            var navigationService = BaseLocator.Instance.Resolve<INavigationService>();
            //Act
            app.SetHomePage<HomeViewModel>();
            var pushMasterDetailsTask = navigationService.NavigateToMasterDetailsAsync(masterPage,detailPage);
            var isMasterDetailExists = ModalStack.Any(x => x.GetType() == typeof(MasterDetailPage)) ||
                                       NavigationStack.Any(x => x.GetType() == typeof(MasterDetailPage));
            //Assert
            Assert.IsTrue(pushMasterDetailsTask.IsCompleted);
            Assert.IsTrue(isMasterDetailExists);
        }
        [TestMethod]
        public  void ChangeDetailPageInMasterDetailTest()
        {
            //Arrange
            var app = new MockApp();
            var masterPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Master Page",
                ViewModel = BaseLocator.Instance.Resolve<HomeViewModel>()
            };
            var detailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "Detail Page",
                ViewModel = BaseLocator.Instance.Resolve<HomeViewModel>()
            };
            var newDetailPage = new PageContainer()
            {
                IsNavigationPage = true,
                Parameter = "New Detail Page",
                ViewModel = BaseLocator.Instance.Resolve<HomeViewModel>()
            };
            var navigationService = BaseLocator.Instance.Resolve<INavigationService>();
            //Act
            app.SetHomePage(masterPage, detailPage);
            var isDetailPageChanged =  navigationService.ChangeDetailPage(newDetailPage);
            //Assert
            Assert.IsTrue(isDetailPageChanged);
        }
        [TestMethod]
        public void AddTabbedPageOnRunTime()
        {
            //Arrange              
            var app = new MockApp();
            var navigationService = BaseLocator.Instance.Resolve<INavigationService>();
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
            app.SetHomePage(pageContainers);            
            var isPageAdded = navigationService.AddPageToTabbedPage(newPage);
            //Assert
            Assert.IsTrue(isPageAdded);
        }        
    }
}
