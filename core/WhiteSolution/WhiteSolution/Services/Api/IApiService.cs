using Refit;
using WhiteSolution.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteSolution.Transitions;

namespace WhiteSolution.Services.Api
{
    [Headers("*put your headers here*")]
    public interface IApiService
    {
        [Get("*put your uri here*")]
        Task<ApiProduct> GetProducts();
    }
}

