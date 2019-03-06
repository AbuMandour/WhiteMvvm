using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;

namespace WhiteMvvm.Services.Api
{
    public class ApiService : IApiService
    {
        public async Task<TBaseTransitional> Get<TBaseTransitional>(Dictionary<string, string> headers, string uri, Dictionary<string, string> param = null) where TBaseTransitional : BaseTransitional
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
            var fullUri = GetFullUrl(uri, param);
            var response = await client.GetAsync(fullUri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<TBaseTransitional>(jsonString);
            return jsonObject;
        }
        private string GetFullUrl(string uri, Dictionary<string, string> param)
        {
            var query = new StringBuilder().Append("?");
            if (param == null || param.Count <= 0)
                return !string.IsNullOrEmpty(uri) ? uri : "";
            var lastElement = param.ElementAt(param.Count - 1);
            foreach (var item in param)
            {
                var flag = item.Key == lastElement.Key;
                if (!flag)
                    query.Append(item.Key + "=" + item.Value).Append("&");
                else
                    query.Append(item.Key + "=" + item.Value);
            }
            return $"{uri}/{query}";
        }

    }
}
