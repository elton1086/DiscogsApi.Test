using DiscogsApi.Modeles;
using System.Threading.Tasks;

namespace DiscogsApi.Contrats.Proxys
{
    public interface IDiscogsApiProxy
    {
        Task<CollectionDiscogs> ObtenirCollectionDisquesAsync(int page = 1);
    }
}
