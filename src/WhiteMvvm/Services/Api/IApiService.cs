using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;

namespace WhiteMvvm.Services.Api
{
    public interface IApiService
    {
        Task<TBaseTransitional> Get<TBaseTransitional>(Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null)
            where TBaseTransitional : BaseTransitional;
    }
}
