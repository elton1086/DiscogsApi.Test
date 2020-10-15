using DiscogsApi.Modeles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscogsApi.Contrats.Services
{
    public interface ICollectionService
    {
        Task<List<Disque>> ObtenirDisquesAsync(int? quantiteMax = null);
    }
}