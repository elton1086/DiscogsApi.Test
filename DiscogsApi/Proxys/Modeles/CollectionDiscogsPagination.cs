using Newtonsoft.Json;

namespace DiscogsApi.Modeles
{
    public class CollectionDiscogsPagination
    {
        public int Page { get; set; }
        [JsonProperty("per_page")]
        public int PerPage { get; set; }
        public int Items { get; set; }
    }
}
