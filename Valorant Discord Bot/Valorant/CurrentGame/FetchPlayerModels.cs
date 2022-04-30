using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.CurrentGame
{
    public class FetchPlayerModels
    {
        [JsonProperty("Subject")]
        public string Subject { get; set; }

        [JsonProperty("MatchID")]
        public string MatchID { get; set; }

        [JsonProperty("Version")]
        public long GameStartEpochms { get; set; }
    }
}
