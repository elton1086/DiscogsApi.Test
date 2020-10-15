using System.Collections.Generic;

namespace DiscogsApi.Modeles
{
    public class Disque
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public List<Artiste> Artists { get; set; }
    }
}
