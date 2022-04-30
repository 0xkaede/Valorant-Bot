using Newtonsoft.Json;
using System.Collections.Generic;

namespace Valorant_Discord_Bot.Valorant.CurrentGame
{
    public class CompetitiveTiersModels
    {
        [JsonProperty("data")]
        public CData data { get; set; }
    }

    public class CData
    {
        [JsonProperty("uuid")]
        public string uuid { get; set; }

        [JsonProperty("assetObjectName")]
        public string assetObjectName { get; set; }

        [JsonProperty("tiers")]
        public List<Tiers> tiers { get; set; }
    }

    public class Tiers
    {
        [JsonProperty("tier")]
        public int tier { get; set; }

        [JsonProperty("tierName")]
        public string tierName { get; set; }

        [JsonProperty("smallIcon")]
        public string smallIcon { get; set; }
    }
}
