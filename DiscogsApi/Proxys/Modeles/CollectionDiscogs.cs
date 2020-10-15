using System.Collections.Generic;

namespace DiscogsApi.Modeles
{
    public class CollectionDiscogs
    {
        public CollectionDiscogsPagination Pagination { get; set; }
        public List<CollectionDiscogsRelease> Releases { get; set; }
    }
}
