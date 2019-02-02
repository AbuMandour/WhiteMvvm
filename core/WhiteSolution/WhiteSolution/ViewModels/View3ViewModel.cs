using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteSolution.ViewModels.Bases;

namespace WhiteSolution.ViewModels
{
    public class View3ViewModel : BaseViewModel
    {
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
        public override bool HandleBackButton()
        {
            var modalStack = App.Current.MainPage.Navigation.ModalStack;
            var navogationStack = App.Current.MainPage.Navigation.NavigationStack;
            return base.HandleBackButton();
        }
    }
}
