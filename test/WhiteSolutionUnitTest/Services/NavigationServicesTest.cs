using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WhiteSolution;
using WhiteSolution.Services.Navigation;
using WhiteSolution.ViewModels;
using WhiteSolution.ViewModels.Bases;
using WhiteSolution.Views;
using Xamarin.Forms;

namespace WhiteSolutionUnitTest.Services
{
    [TestClass]
    public class NavigationServicesTest : BaseTest
    {
        [TestMethod]
        public async Task InitializeAppNavigation()
        {
            //Arange                      
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            //Act
            await navigationService.InitializeAsync();
            //Assert
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.Where(x => x.BindingContext.GetType() == typeof(HomeViewModel));
            Assert.IsNotNull(homePage);
        }
        [TestMethod]
        public void NavigateTo_WithOutParameter()
        {
            //Arrange           
            var navigationService = ViewModelLocator.Resolve<INavigationService>();            
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
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            //Act
            navigationService.NavigateToAsync<HomeViewModel>("Hello Test");
            var homePage = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(x => x.GetType() == typeof(HomePage));
            var homeViewModel = ViewModelLocator.Resolve<HomeViewModel>();
            //Assert
            Assert.IsTrue((string)homeViewModel.NavigationData == "Hello Test");
            Assert.IsNotNull(homePage);
            
        }
        [TestMethod]
        public void NavigateToTabbedPage()
        {
            //Arrange
            ViewModelLocator.UpdateDependencies(true);
            var pageContainers = new List<PageContainer>
            {
                new PageContainer()
                {
                    IsNavigationPage = true,
                    Parameter = "Home Page",
                    ViewModel = ViewModelLocator.Resolve<HomeViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page One",
                    ViewModel = ViewModelLocator.Resolve<View1ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page two",
                    ViewModel = ViewModelLocator.Resolve<View2ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page three",
                    ViewModel = ViewModelLocator.Resolve<View3ViewModel>()
                }
            };
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
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
                    ViewModel = ViewModelLocator.Resolve<HomeViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page One",
                    ViewModel = ViewModelLocator.Resolve<View1ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page two",
                    ViewModel = ViewModelLocator.Resolve<View2ViewModel>()
                },
                new PageContainer()
                {
                    IsNavigationPage = false,
                    Parameter = "Page three",
                    ViewModel = ViewModelLocator.Resolve<View3ViewModel>()
                }
            };
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
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
            ViewModelLocator.UpdateDependencies(false);
        }
    }
}
