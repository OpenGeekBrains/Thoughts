using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Thoughts.Interfaces.Base;
using Thoughts.WebAPI.Clients.Base;

namespace Thoughts.WebAPI.Clients.ShortUrl
{
    public class ShortUrlClient: BaseClient, IShortUrlManager
    {
        public ShortUrlClient(IConfiguration Configuration):base(Configuration,WebApiControllersPath.ShortUrlV1)
        {

        }

        public async Task<int> AddUrlAsync(string Url, CancellationToken Cancel = default)
        {
            var response = await PostAsync<string>($"{WebApiControllersPath.ShortUrlV1}", Url);
            return await response.Content.ReadAsAsync<int>();
        }

        public async Task<bool> DeleteUrlAsync(int Id, CancellationToken Cancel = default)
        {
            var response = await DeleteAsync($"{WebApiControllersPath.ShortUrlV1}/{Id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<string> GetAliasByIdAsync(int Id, CancellationToken Cancel = default)
        {
            var response = await GetAsync<string>($"{WebApiControllersPath.ShortUrlV1}/alias/{Id}");
            return response;
        }

        public async Task<Uri?> GetUrlAsync(string Alias, CancellationToken Cancel = default)
        {
            var response = await GetAsync<Uri?>($"{WebApiControllersPath.ShortUrlV1}?Alias={Alias}");
            return response;
        }

        public async Task<Uri?> GetUrlByIdAsync(int Id, CancellationToken Cancel = default)
        {
            var response = await GetAsync<Uri?>($"{WebApiControllersPath.ShortUrlV1}/{Id}");
            return response;
        }

        public async Task<bool> UpdateUrlAsync(int Id, string Url, CancellationToken Cancel = default)
        {
            var response = await PostAsync<string>($"{WebApiControllersPath.ShortUrlV1}/{Id}", Url);
            return await response.Content.ReadAsAsync<bool>();
        }

    }
}
