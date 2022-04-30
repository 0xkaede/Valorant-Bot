using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Valorant_Discord_Bot.Valorant.CurrentGame
{
    public class CompetitiveUpdatesModels
    {
        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("Subject")]
        public string Subject { get; set; }

        [JsonProperty("Matches")]
        public List<PlayerMatches> matches { get; set; }
    }

    public class PlayerMatches
    {
        [JsonProperty("TierAfterUpdate")]
        public int TierAfterUpdate { get; set; }
    }
}
