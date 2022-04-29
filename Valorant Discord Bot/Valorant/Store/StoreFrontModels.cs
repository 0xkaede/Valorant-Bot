using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valorant_Discord_Bot.Valorant.Store
{
    public class StoreFrontModels
    {
        [JsonProperty("SkinsPanelLayout")]
        public SkinsPanelLayout SkinsPanelLayout { get; set; }

        [JsonProperty("BonusStore")]
        public BonusStore BonusStore { get; set; }
    }

    public class SkinsPanelLayout
    {
        public string[] SingleItemOffers { get; set; }

        [JsonProperty("SingleItemOffersRemainingDurationInSeconds")]
        public int SingleItemOffersRemainingDurationInSeconds { get; set; }
    }

    public class BonusStore
    {
        [JsonProperty("BonusStoreOffers")]
        public List<BonusStoreOffer> BonusStoreOffers { get; set; }

        [JsonProperty("BonusStoreRemainingDurationInSeconds")]
        public int BonusStoreRemainingDurationInSeconds { get; set; }
    }

    public class BonusStoreOffer
    {
        [JsonProperty("BonusOfferID")]
        public string BonusOfferID { get; set; }

        [JsonProperty("Offer")]
        public Offer Offer { get; set; }

        [JsonProperty("DiscountPercent")]
        public int DiscountPercent { get; set; }

    }

    public class Offer
    {
        [JsonProperty("OfferID")]
        public string OfferID { get; set; }

        [JsonProperty("StartDate")]
        public string StartDate { get; set; }

        [JsonProperty("Cost")]
        public Cost Cost { get; set; }
    }
}
