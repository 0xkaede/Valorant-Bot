using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.CurrentGame
{
    public class PlayerMatchModels
    {
        [JsonProperty("Matches")]
        public List<Matches> matches { get; set; }
    }

    public class Matches
    {
        [JsonProperty("TierAfterUpdate")]
        public int TearAfterUpdate { get; set; }

        [JsonProperty("RankedRatingAfterUpdate")]
        public int RankedRatingAfterUpdate { get; set; }
    }
}
