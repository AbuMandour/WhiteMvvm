using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Utilities;
using WhiteSample.Transitions;

namespace WhiteSample.Services
{
    public interface IHomeService
    {
        Task<TransitionalList<ApiProduct>> GetProducts();
    }
}
