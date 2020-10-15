using DiscogsApi.Proxys.Modeles;
using Newtonsoft.Json;

namespace DiscogsApi.Modeles
{
    public class CollectionDiscogsRelease
    {
        [JsonProperty("basic_information")]
        public CollectionDiscogsInformation BasicInformation { get; set; }
    }
}
