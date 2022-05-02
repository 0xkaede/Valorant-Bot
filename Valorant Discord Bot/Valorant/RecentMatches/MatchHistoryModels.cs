using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.RecentMatches
{
    public class MatchHistoryModels
    {
        [JsonProperty("History")]
        public List<History> history { get; set; }
    }

    public class History
    {
        [JsonProperty("MatchID")]
        public string matchID { get; set; }

        [JsonProperty("QueueID")]
        public string queueID { get; set; }
    }
}
