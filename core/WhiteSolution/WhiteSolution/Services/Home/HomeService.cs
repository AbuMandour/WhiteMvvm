using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using WhiteSolution.Transitions;
using WhiteSolution.Services.Utilities;
using WhiteSolution.Utils;

namespace WhiteSolution.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly IConnectivity _connectivity;

        public HomeService(IConnectivity connectivity)
        {
            _connectivity = connectivity;
        }
        public async Task<TransitionList<ApiProduct>> GetProducts()
        {
            try
            {
                if (_connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    return new TransitionList<ApiProduct>();
                }                    
                var products = new TransitionList<ApiProduct>();
                for (var i = 1; i < 20; i++)
                {
                    var product = new ApiProduct()
                    {
                        FirstName = $"Product{i} ",
                        SecondName = $"-{i}",
                        Price = 10 * i + "",
                        Discount = "0.15"
                    };
                    products.Add(product);
                }
                await Task.Delay(2000);
                return products;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                return new TransitionList<ApiProduct>();
            }
        }
    }
}
