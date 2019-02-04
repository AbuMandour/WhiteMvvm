using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteSolution.Services.Home;
using WhiteSolution.ViewModels.Bases;
using WhiteSolution.Models;
using WhiteSolution.Utilities;
using System.Collections.Generic;
using WhiteSolution.Services.Navigation;
using WhiteSolution.Services.DeviceUtilities;

namespace WhiteSolution.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IHomeService _homeService;
        private readonly IConnectivity _connectivity;
        public ObservableRangeCollection<Product> Products { get; set; }
        public ICommand SelectProductCommand { get; set; }
        public HomeViewModel(IHomeService homeService, IConnectivity connectivity)
        {
            _homeService = homeService;
            _connectivity = connectivity;
            SelectProductCommand = new TaskCommand(OnSelectProductTask);            
        }
        public override async Task InitializeAsync(object navigationData)
        {
            NavigationData = navigationData;
            IsBusy = true;
            Products = await LoadData();            
            IsBusy = false;
        }                
        private async Task OnSelectProductTask(object obj)
        {
            var product = (Product)obj;
            if (product != null)
            {
                //await DialogService.ShowAlertAsync(product.Name, "Product Name", "Ok");

                //var PageContainers = new List<PageContainer>();
                //PageContainers.Add(new PageContainer() { IsNavigationPage = false, Parameter = "Page One", ViewModel = ViewModelLocator.Resolve<View1ViewModel>() });
                //PageContainers.Add(new PageContainer() { IsNavigationPage = false, Parameter = "Page two", ViewModel = ViewModelLocator.Resolve<View2ViewModel>() });
                //PageContainers.Add(new PageContainer() { IsNavigationPage = false, Parameter = "Page three", ViewModel = ViewModelLocator.Resolve<View3ViewModel>() });

                //await NavigationService.NavigateToTabbedAsync(PageContainers);
                await NavigationService.NavigateToAsync<View3ViewModel>();
            }
        }
        internal override async Task OnAppearing()
        {
            Products = await LoadData();
        }
        public async Task<ObservableRangeCollection<Product>> LoadData()
        {
            try
            {
                if (_connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsBusy = true;
                    var apiProducts = await _homeService.GetProducts();
                    IsBusy = false;
                    return apiProducts.ToModel<Product>();
                }
                else
                {
                    await DialogService.ShowAlertAsync("No Internet Connection", "Internet", "Ok");
                    IsBusy = false;
                    return new ObservableRangeCollection<Product>();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return new ObservableRangeCollection<Product>();
            }
        }
    }
}
