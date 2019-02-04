using System;
using System.Collections.Generic;
using System.Text;
using WhiteSolution.Utilities;
using WhiteSolution.ViewModels.Bases;

namespace WhiteSolution.Services.Navigation
{
    public class PageContainer : NotifiedObject
    {
        public PageContainer()
        {
        }
        public PageContainer(BaseViewModel viewModel, object parameter = null,  bool isNavigationPage = false, string pageName = "")
        {
            Parameter = parameter;
            ViewModel = viewModel;
            IsNavigationPage = isNavigationPage;
            PageName = pageName;
        }
        public object Parameter { get; set; }
        public BaseViewModel ViewModel { get; set; }
        public bool IsNavigationPage { get; set; }
        public string PageName { get; set; }
    }
}
