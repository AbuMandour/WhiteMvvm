using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSolution.Services.Home;
using WhiteSolution.Services.Utilities;
using WhiteSolution.ViewModels.Bases;

namespace WhiteSolutionUnitTest.Services
{
    [TestClass]
    public class HomeServicesTest : BaseTest
    {
        [TestCleanup]
        public override void CleanUpTest()
        {
            
        }

        [TestMethod]
        public async Task GetProductsTest()
        {
            //Arange
            var connectionServicesMock = new Mock<IConnectivity>();
            var connectionServices = connectionServicesMock.Object;
            connectionServicesMock.Setup(x => x.NetworkAccess).Returns(Xamarin.Essentials.NetworkAccess.Internet);
            var homeServices = new HomeService(connectionServices);

            //Act
            var products = await homeServices.GetProducts();
            //Assert
            Assert.IsTrue(products.Count > 0);
        }
        [TestMethod]
        public async Task GetProductsTestWithOutInternet()
        {
            //Arange
            var connectionServicesMock = new Mock<IConnectivity>();
            var connectionServices = connectionServicesMock.Object;
            connectionServicesMock.Setup(x => x.NetworkAccess).Returns(Xamarin.Essentials.NetworkAccess.Local);
            var homeServices = new HomeService(connectionServices);

            //Act
            var products = await homeServices.GetProducts();
            //Assert
            Assert.IsFalse(products.Count > 0);
        }
    }
}
