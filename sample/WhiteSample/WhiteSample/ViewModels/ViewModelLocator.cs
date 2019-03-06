using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteSample.Services;

namespace WhiteSample.ViewModels
{
    public class ViewModelLocator : BaseViewModelLocator
    {
        public static void Init()
        {
            Register<HomeViewModel>();
            Register<IConnectivity, ConnectivityService>();
            Register<IHomeService, HomeService>();
        }
    }
}
