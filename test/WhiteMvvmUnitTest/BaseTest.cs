using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhiteMvvm;
using WhiteMvvm.Bases;
using WhiteMvvmUnitTest.Mocks;
using Xamarin.Forms;

namespace WhiteMvvmUnitTest
{
    [TestClass]
    public class BaseTest
    {      
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
        [AssemblyInitialize]
        public static void StartUpTest(TestContext context)
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            Application.Current = new MockApp();
            BaseLocator.Instance.MocksUpdate(true);
        }
        [AssemblyCleanup]
        public static void CleanUpTest()
        {
            BaseLocator.Instance.MocksUpdate(true);
        }
    }
}
