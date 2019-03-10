using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteSample.Services;

namespace WhiteSample.ViewModels
{
    public class ViewModelLocator : BaseLocator
    {
        public static void Init()
        {
            Instance.Register<HomeViewModel>();
            Instance.Register<IConnectivity, ConnectivityService>();
            Instance.Register<IHomeService, HomeService>();
        }

        public override void MocksUpdate(bool useMocks)
        {
            base.MocksUpdate(useMocks);
        }
    }
}
