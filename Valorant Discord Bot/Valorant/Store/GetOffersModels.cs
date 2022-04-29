using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.Store
{
    public class GetOffersModels
    {
        [JsonProperty("Offers")]
        public List<Offers> offers { get; set; }
    }

    public class Offers
    {
        [JsonProperty("OfferID")]
        public string OfferID { get; set; }

        [JsonProperty("Cost")]
        public Cost cost { get; set; }
    }

    public class Cost
    {
        [JsonProperty("85ad13f7-3d1b-5128-9eb2-7cd8ee0b5741")]
        public int CostID { get; set; }
    }
}
