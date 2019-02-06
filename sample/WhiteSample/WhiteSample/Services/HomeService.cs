using System;
using System.Threading.Tasks;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Utilities;
using WhiteSample.Transitions;

namespace WhiteSample.Services
{
    public class HomeService : IHomeService
    {
        private readonly IConnectivity _connectivity;

        public HomeService(IConnectivity connectivity)
        {
            _connectivity = connectivity;
        }
        public async Task<TransitionalList<ApiProduct>> GetProducts()
        {
            try
            {         
                var products = new TransitionalList<ApiProduct>();
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
                Console.Write(ex);
                return new TransitionalList<ApiProduct>();
            }
        }
    }
}
