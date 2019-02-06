using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteSolution.Models;
using WhiteSolution.Transitions;
using WhiteSolution.Utilities;

namespace WhiteSolution.Services.Home
{
    public interface IHomeService
    {
        Task<TransitionalList<ApiProduct>> GetProducts();
    }
}
