using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.Auth
{
    public class UserModel
    {
        [JsonProperty("sub")]
        public string puuid { get; set; }

        [JsonProperty("acct")]
        public Acct acct { get; set; }
    }

    public class Acct
    {
        [JsonProperty("game_name")]
        public string name { get; set; }

        [JsonProperty("tag_line")]
        public string tag { get; set; }
    }
}
