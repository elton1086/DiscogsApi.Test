using DiscogsApi.Contrats.Proxys;
using DiscogsApi.Modeles;
using DiscogsApi.Proxys.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DiscogsApi.Proxys
{
    public class DiscogsApiProxy : IDiscogsApiProxy
    {
        private const string GET_COLLECTION = "users/ausamerika/collection/folders/0/releases?per_page=1&page={0}";

        private readonly HttpClient _clientHttp;

        public DiscogsApiProxy(IOptions<ConfigurationProxy> config)
        {
            _clientHttp = new HttpClient { BaseAddress = new Uri(config.Value.Discogs.AdresseBase) };
            _clientHttp.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("DiscogsApi.Test", "0.1"));
        }

        private async Task<T> GetAsync<T>(string uriRequete) where T : class
        {
            var reponse = await _clientHttp.GetAsync(uriRequete);
            if (!reponse.IsSuccessStatusCode)
                throw new Exception($"La requête http : {reponse.RequestMessage.RequestUri} a retournée le status: {reponse.StatusCode}");
            return JsonConvert.DeserializeObject<T>(await reponse.Content.ReadAsStringAsync());
        }

        public async Task<CollectionDiscogs> ObtenirCollectionDisquesAsync(int page = 1)
        {
            return await GetAsync<CollectionDiscogs>(string.Format(GET_COLLECTION, page));
        }
    }
}
