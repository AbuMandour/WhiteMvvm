using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Utilities;
using WhiteSample.Models;
using WhiteSample.Services;

namespace WhiteSample.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IHomeService _homeService;
        private readonly IConnectivity _connectivity;
        private ObservableRangeCollection<Product> _products;

        public ObservableRangeCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
            }
        }

        public ICommand SelectProductCommand { get; set; }
        public HomeViewModel(IHomeService homeService, IConnectivity connectivity)
        {
            _homeService = homeService;
            _connectivity = connectivity;
            SelectProductCommand = new TaskCommand(OnSelectProductTask);
        }
        protected override async Task InitializeAsync(object navigationData)
        {
            NavigationData = navigationData;
            Products = await LoadData();
        }
        private async Task OnSelectProductTask(object obj)
        {
            var product = (Product)obj;
            if (product != null)
            {
                //await NavigationService.NavigateToAsync<View3ViewModel>();
            }
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
                await DialogService.ShowAlertAsync(ex.Message, "Error", "Ok");
                return new ObservableRangeCollection<Product>();
            }
        }
    }
}
