using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhiteMvvm.Bases;
using WhiteMvvm.UnitTest.Mocks;
using Xamarin.Forms;

namespace WhiteMvvm.UnitTest
{
    [TestClass]
    public class BaseTest
    {
        public static IReadOnlyList<Page> MockModalStack
        {
            get
            {
                var modalStack = MockApp.Current.MainPage.Navigation.ModalStack ?? new List<Page>();
                return modalStack;
            }
        }
        public static IReadOnlyList<Page> MockNavigationStack
        {
            get
            {
                var navigationStack = MockApp.Current.MainPage.Navigation.NavigationStack ?? new List<Page>();
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
            BaseLocator.Instance.MocksUpdate(false);
        }
    }

}
