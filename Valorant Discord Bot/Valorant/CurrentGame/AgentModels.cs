using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.CurrentGame
{
    public class AgentModels
    {
        [JsonProperty("data")]
        public Data data { get; set; }
    }

    public class Data
    {
        [JsonProperty("uuid")]
        public string uuid { get; set; }

        [JsonProperty("displayName")]
        public string displayName { get; set; }

        [JsonProperty("killfeedPortrait")]
        public string killfeedPortrait { get; set; }
    }
}
