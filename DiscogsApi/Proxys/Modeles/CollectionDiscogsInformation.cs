using System.Collections.Generic;

namespace DiscogsApi.Proxys.Modeles
{
    public class CollectionDiscogsInformation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public List<CollectionDicogsArtist> Artists { get; set; }
    }
}
