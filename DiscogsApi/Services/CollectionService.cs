using DiscogsApi.Contrats.Proxys;
using DiscogsApi.Contrats.Services;
using DiscogsApi.Modeles;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscogsApi.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly IDiscogsApiProxy _discogsProxy;
        private readonly Random _random = new Random(DateTime.Now.Millisecond);

        public CollectionService(IDiscogsApiProxy discogsProxy)
        {
            _discogsProxy = discogsProxy;
        }

        public async Task<List<Disque>> ObtenirDisquesAsync(int? quantiteMax)
        {            
            var quantite = quantiteMax.HasValue ? Math.Min(quantiteMax.Value, 5) : 1;
            var collection = await _discogsProxy.ObtenirCollectionDisquesAsync();
            var max = collection.Pagination.Items;
            var listePages = DefinirListePageAleatoires(quantite, max);
            return await ObtenirDisquesAsync(listePages);
        }

        private async Task<List<Disque>> ObtenirDisquesAsync(List<int> listePages)
        {
            var resultat = new List<Disque>();
            foreach (var page in listePages)
            {
                var discogs = await _discogsProxy.ObtenirCollectionDisquesAsync(page);
                resultat.Add(discogs.Releases.FirstOrDefault()?.BasicInformation.Adapt<Disque>());
            }
            return resultat;
        }

        private List<int> DefinirListePageAleatoires(int quantite, int max)
        {
            var listePages = new List<int>();
            int indice = 0;
            while (indice < Math.Min(quantite, max))
            {
                var page = _random.Next(1, max + 1);
                if (!listePages.Contains(page))
                {
                    listePages.Add(page);
                    ++indice;
                }
            }
            return listePages;
        }
    }
}
