using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhiteSolution;
using WhiteSolution.ViewModels.Bases;
using Xamarin.Forms;

namespace WhiteSolutionUnitTest
{
    [TestClass]
    public abstract class BaseTest
    {
        static BaseTest()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new App();
            ViewModelLocator.UpdateDependencies(true);
        }

        public static IReadOnlyList<Page> ModalStack
        {
            get
            {
                var modalStack = Application.Current.MainPage.Navigation.ModalStack ?? new List<Page>();
                return modalStack;
            }
        }

        public static IReadOnlyList<Page> NavigationStack
        {
            get
            {
                var navigationStack = Application.Current.MainPage.Navigation.NavigationStack ?? new List<Page>();
                return navigationStack;
            }
        }

        public abstract void CleanUpTest();
    }
}
