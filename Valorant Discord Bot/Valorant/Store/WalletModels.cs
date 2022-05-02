using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.Store
{
    public class WalletModels
    {
        [JsonProperty("Balances")]
        public Balances balances { get; set; }
    }

    public class Balances
    {
        [JsonProperty("85ad13f7-3d1b-5128-9eb2-7cd8ee0b5741")]
        public int Valorant { get; set; }

        [JsonProperty("e59aa87c-4cbf-517a-5983-6e81511be9b7")]
        public int Radianite { get; set; }
    }

}
