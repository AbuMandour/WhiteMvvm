﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteSolution.Services.Dialog;
using WhiteSolution.ViewModels.Bases;

namespace WhiteSolutionUnitTest.Services
{
    [TestClass]
    public class DialogServicesTest : BaseTest
    {
        [TestMethod]
        public void ShowAlertTest()
        {
            //Arange           
            var dialogServiceMock = new Mock<IDialogService>();
            var dialogService = dialogServiceMock.Object;
            //Act
            var result = dialogService.ShowAlertAsync("message", "title", "Ok");
            //Assert
            Assert.IsTrue(result.IsCompleted);
        }
        [TestMethod]
        public void ShowConfirmMessageTest()
        {
            //Arange           
            var dialogServiceMock = new Mock<IDialogService>();
            var dialogService = dialogServiceMock.Object;
            //Act
            var result =  dialogService.ShowConfirmMessageAsync("message");
            //Assert
            Assert.IsTrue(result.IsCompleted);
        }
        [TestCleanup]
        public override void CleanUpTest()
        {
            ViewModelLocator.UpdateDependencies(false);
        }
    }
}
