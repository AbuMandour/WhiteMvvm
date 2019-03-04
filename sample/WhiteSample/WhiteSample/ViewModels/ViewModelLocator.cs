using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;
using WhiteSample.Services;

namespace WhiteSample.ViewModels
{
    public class ViewModelLocator : BaseViewModelLocator
    {
        static ViewModelLocator()
        {
            Register<HomeViewModel>();
            Register<IHomeService,HomeService>();
        }
    }
}
