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
        /// <summary>
        /// any object to pass to view model
        /// </summary>
        public object Parameter { get; set; }
        /// <summary>
        /// viewmodel to be wired with page
        /// </summary>
        public BaseViewModel ViewModel { get; set; }
        /// <summary>
        /// if we want to make this page start as navigation page
        /// </summary>
        public bool IsNavigationPage { get; set; }
        /// <summary>
        /// name of page we will navigate, note that name will use in tabbed page
        /// </summary>
        public string PageName { get; set; }
    }
}
