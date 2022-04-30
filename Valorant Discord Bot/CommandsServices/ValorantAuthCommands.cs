using Discord;
using Discord.Commands;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Valorant_Discord_Bot.DataBase;
using Valorant_Discord_Bot.Store.Util;
using Valorant_Discord_Bot.Utilities;
using Valorant_Discord_Bot.Valorant.Auth;
using Valorant_Discord_Bot.Valorant.CurrentGame;
using Valorant_Discord_Bot.Valorant.Store;
using static Valorant_Discord_Bot.DataBase.DataEmums;
using static Valorant_Discord_Bot.Valorant.Store.WeaponsSkinUuuidModels;
using static Valorant_Discord_Bot.CommandsServices.ValorantAuthCommands;

namespace Valorant_Discord_Bot.CommandsServices
{
    public class ValorantAuthCommands : ModuleBase
    {
        public static string AccessToken { get; set; }
        public static string EntitlementToken { get; set; }
        public static string puuid { get; set; }
        public static string username { get; set; }
        public static string userdata { get; set; }

        public class PlayerGameInfo
        {
            public string ID = "";

            public string CharactorID = "";
            public string CharactorIcon = "";
            public string CharactorName = "";

            public Color TeamColor;
            public string TeamID;

            public string RankIMGURL = "";
            public string RankName = "";

            public int Elo = 0;

            public string Peak_RankIMGURL = "";
            public string Peak_RankName = "";
        }

        [Command("Store")]
        public async Task Store(params string[] args)
        {
            var username = args[0];

            DataBaseResponse CheckUser = Manager.CheckForUser(username);
            if (CheckUser == DataBaseResponse.NotFound)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("User Not Found!", $"We coundnt find your Profile in our database!\nPlease make sure you have create an account by using\n{Configuration.DiscordPrefix}AddUser <RiotUsername> <RiotPassword> <RiotRegion> in DMS!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }
            else if (CheckUser == DataBaseResponse.Found)
                userdata = Manager.UserInfo(username);

            DataModels userinfo = JsonConvert.DeserializeObject<DataModels>(userdata);

            bool successful = Authentication.Login(userinfo.valorantDetails.Username, Encryption.Decrypt(userinfo.valorantDetails.password, Configuration.EncryptionKey));
            if (!successful)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("Auth Error!", $"There was an error with Authentication. Make sure that,\n2FA is disabled on your account,\nCheck if your password is right").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }

            Authentication.GetUserInfo(AccessToken);

            StoreFrontModels currentUserShop = JsonConvert.DeserializeObject<StoreFrontModels>(StoreRequest.GetStoreFront(userinfo.valorantData.puuid, userinfo.valorantDetails.region));

            string[] Skins = currentUserShop.SkinsPanelLayout.SingleItemOffers;

            foreach (var skin in Skins)
            {
                var WeaponSkin = DiscordEmbedAdder.AdderSkin(StoreRequest.GetWeaponSkinUuid(skin), StoreRequest.GetOffers(userinfo.valorantDetails.region)).Build();
                await Context.Channel.SendMessageAsync(null, false, WeaponSkin);
            }

            TimeSpan t = TimeSpan.FromSeconds(currentUserShop.SkinsPanelLayout.SingleItemOffersRemainingDurationInSeconds);
            string timeLeft = string.Format("{0:D2} hours, {1:D2} minutes, and {2:D2} seconds remaining",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);

            var timeEmbed = DiscordEmbedAdder.AdderSmall("Offers ends in", timeLeft).Build();
            await Context.Channel.SendMessageAsync(null, false, timeEmbed);
        }

        [Command("CurrentGame")]
        public async Task CurrentGame(params string[] args)
        {
            var username = args[0];

            DataBaseResponse CheckUser = Manager.CheckForUser(username);
            if (CheckUser == DataBaseResponse.NotFound)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("User Not Found!", $"We coundnt find your Profile in our database!\nPlease make sure you have create an account by using\n{Configuration.DiscordPrefix}AddUser <RiotUsername> <RiotPassword> <RiotRegion> in DMS!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }
            else if (CheckUser == DataBaseResponse.Found)
                userdata = Manager.UserInfo(username);

            DataModels userinfo = JsonConvert.DeserializeObject<DataModels>(userdata);

            bool successful = Authentication.Login(userinfo.valorantDetails.Username, Encryption.Decrypt(userinfo.valorantDetails.password, Configuration.EncryptionKey));
            if (!successful)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("Auth Error!", $"There was an error with Authentication. Make sure that,\n2FA is disabled on your account,\nCheck if your password is right").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }

            Authentication.GetUserInfo(AccessToken);

            List<PlayerGameInfo> PlayerInGame = new List<PlayerGameInfo>();

            try
            {
                CurrentGameRequest.GetFetchPlayer(userinfo.valorantData.puuid, userinfo.valorantDetails.region);
            }
            catch (Exception ex)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("Player Not In game!", $"Current {username} isnt in a game, This command only works when user is in a game!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }

            FetchPlayerModels CurrentGameUser = JsonConvert.DeserializeObject<FetchPlayerModels>(CurrentGameRequest.GetFetchPlayer(userinfo.valorantData.puuid, userinfo.valorantDetails.region));

            FetchMatchModels CurrentGame = JsonConvert.DeserializeObject<FetchMatchModels>(CurrentGameRequest.GetFetchMacth(CurrentGameUser.MatchID, userinfo.valorantDetails.region));

            foreach (var player in CurrentGame.Players)
            {
                bool a = false;

                AgentModels agentinfo = JsonConvert.DeserializeObject<AgentModels>(CurrentGameRequest.GetAgentInfo(player.CharacterID));

                CompetitiveUpdatesModels competitiveupdates = JsonConvert.DeserializeObject<CompetitiveUpdatesModels>(CurrentGameRequest.GetFetchCompetitiveUpdates(player.Subject, userinfo.valorantDetails.region));

                CompetitiveTiersModels competitivetiers = JsonConvert.DeserializeObject<CompetitiveTiersModels>(CurrentGameRequest.GetCompetitiveTiers());

                try
                {
                    var hi = competitiveupdates.matches.FirstOrDefault().TierAfterUpdate;
                }
                catch (Exception ex)
                {
                    a = true;
                }

                Tiers tier;

                if (a)
                {
                    tier = new Tiers()
                    {
                        tier = 0,
                        tierName = "UNRANKED",
                        smallIcon = "https://media.valorant-api.com/competitivetiers/e4e9a692-288f-63ca-7835-16fbf6234fda/0/smallicon.png"
                    };
                }
                else tier = competitivetiers.data.tiers.FirstOrDefault(x => x.tier == competitiveupdates.matches.FirstOrDefault().TierAfterUpdate);

                Console.WriteLine(player.Subject + ":" + tier.tierName);

                var Info = new PlayerGameInfo
                {
                    ID = player.Subject,

                    CharactorID = agentinfo.data.uuid,
                    CharactorIcon = agentinfo.data.killfeedPortrait,
                    CharactorName = agentinfo.data.displayName,

                    TeamID = player.TeamID,

                    RankName = tier.tierName,
                    RankIMGURL = tier.smallIcon
                };

                if (player.TeamID.ToLower() == "blue")
                    Info.TeamColor = Color.Blue;
                else if (player.TeamID.ToLower() == "red")
                    Info.TeamColor = Color.Red;

                PlayerInGame.Add(Info);
            }

            var playersTeamColor = PlayerInGame.FirstOrDefault(x => x.ID == userinfo.valorantData.puuid).TeamID;
            var EnemyTeamColor = "";

            if (playersTeamColor.ToLower() == "blue")
                EnemyTeamColor = "red";
            else EnemyTeamColor = "blue";

            //Players Team

            foreach (var player in PlayerInGame)
            {
                if (player.TeamID == playersTeamColor)
                {
                    var Authbuilder = DiscordEmbedAdder.AdderInGame(player, "This Player is on **Your** Team.").Build();

                    await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                }
            }

            foreach (var player in PlayerInGame)
            {
                if (player.TeamID.ToLower() == EnemyTeamColor)
                {
                    var Authbuilder = DiscordEmbedAdder.AdderInGame(player, "This Player is on the **Enemy** Team.").Build();

                    await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                }
            }
        }

        [Command("AddUser")]
        public async Task AddUser(params string[] args)
        {
            var username = args[0];
            var password = args[1];
            var region = args[2];

            if (Context.Client.GetDMChannelAsync(Context.Channel.Id).Result == null)
            {
                Context.Message.DeleteAsync();
                var Authbuilder = DiscordEmbedAdder.AdderSmall("DMS ONLY!", $"This command only works in DMS!\nFor your safty please change your password **ASAP**!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }

            bool successful = Authentication.Login(username, password);
            if (!successful)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("Auth Error!", $"There was an error with Authentication. Make sure that,\n2FA is disabled on your account,\nCheck if your password is right").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }

            Authentication.GetUserInfo(AccessToken);

            var discordData = new DiscordData { Username = Context.User.Username + "#" + Context.User.Discriminator, ID = Context.User.Id };

            var userDetail =
                new ValorantDetails { Username = username, password = Encryption.Encrypt(password, Configuration.EncryptionKey), region = region };

            var userData =
                new ValorantData { AccessToken = AccessToken, EntitlementToken = EntitlementToken, puuid = puuid, username = ValorantAuthCommands.username };

            var user =
                new DataModels { valorantDetails = userDetail, valorantData = userData, discordData = discordData };

            DataBaseResponse CheckUser = Manager.CheckForUser(username);
            if (CheckUser == DataBaseResponse.Found)
            {
                var AEbuilder = DiscordEmbedAdder.AdderSmall("User Already Exists!", $"{username} was already found in our database.\nIf you wanna update your password do <+UpdateUser>!").Build();
                await Context.Channel.SendMessageAsync(null, false, AEbuilder);
                return;
            }
            else Manager.CreateUser(user);

            var builder = DiscordEmbedAdder.AdderSmall("User Added!", $"{username} has been successfully added to our database!").Build();

            await Context.Channel.SendMessageAsync(null, false, builder);
        }

        [Command("DelUser")]
        public async Task DelUser(params string[] args)
        {
            var username = args[0].ToLower();
            var password = args[1].ToLower();
            var region = args[2].ToLower();

            DataBaseResponse CheckUser = Manager.CheckForUser(username);
            if (CheckUser == DataBaseResponse.NotFound)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("User Not Found!", $"We coundnt find your Profile in our database!\nPlease make sure you have create an account by using\n{Configuration.DiscordPrefix}AddUser <RiotUsername> <RiotPassword> <RiotRegion> in DMS!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }
            else if (CheckUser == DataBaseResponse.Found)
                userdata = Manager.UserInfo(username);

            DataModels userinfo = JsonConvert.DeserializeObject<DataModels>(userdata);

            bool Correct = Checks.CheckIfCorrectDetails(userinfo, args);
            if (!Correct)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("Wrong Info!", $"We only delete users if they get there info correct!\nThere are some work arounds if your own this account!\n" +
                    $"{Configuration.DiscordPrefix}DiscordDeleUser <RiotUsername> - If you made the user on this Discord Account this method will work!)\n" +
                    $"{Configuration.DiscordPrefix}UpdatePass <RiotUsername> <RiotPassword> <RiotRegion> - If you forgot your password and want to reset it you can by this command but only works if you made this user on this Discord Account!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }

            bool CheckIfCanDel = Manager.DeleteUser(userinfo);
            if (!CheckIfCanDel)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("Something went worng!", $"Please contact support in our discord server by using the command\n{Configuration.DiscordPrefix}SupportServer").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }
            else
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("User Deleted!", $"{username} was deleted from our database!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }
        }

        [Command("UpdatePass")]
        public async Task UpdatePass(params string[] args)
        {
            var username = args[0].ToLower();
            var password = args[1].ToLower();
            var region = args[2].ToLower();

            var SenderID = Context.User.Id;

            DataBaseResponse CheckUser = Manager.CheckForUser(username);
            if (CheckUser == DataBaseResponse.NotFound)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("User Not Found!", $"We coundnt find your Profile in our database!\nPlease make sure you have create an account by using\n{Configuration.DiscordPrefix}AddUser <RiotUsername> <RiotPassword> <RiotRegion> in DMS!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }
            else if (CheckUser == DataBaseResponse.Found)
                userdata = Manager.UserInfo(username);

            DataModels userinfo = JsonConvert.DeserializeObject<DataModels>(userdata);

            if (SenderID != userinfo.discordData.ID)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("Wrong Discord!", $"As we dont want other users accounts to be change we lock your account to your Discord Account\nIf you have lost that account please contact Support in our Discord\n{Configuration.DiscordPrefix}SupportServer!").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }

            bool successful = Authentication.Login(userinfo.valorantDetails.Username, password);
            if (!successful)
            {
                var Authbuilder = DiscordEmbedAdder.AdderSmall("Auth Error!", $"There was an error with Authentication. Make sure that,\n2FA is disabled on your account,\nCheck if your **NEW** password is right").Build();

                await Context.Channel.SendMessageAsync(null, false, Authbuilder);
                return;
            }

            Manager.UpdatePassword(userinfo, Encryption.Encrypt(password, Configuration.EncryptionKey));

            var Rbuilder = DiscordEmbedAdder.AdderSmall("Password Updated!", $"Your password was update to {Encryption.Encrypt(password, Configuration.EncryptionKey)}!").Build();

            await Context.Channel.SendMessageAsync(null, false, Rbuilder);
            return;
        }
    }
}

namespace Valorant_Discord_Bot.Store.Util
{
    internal class DiscordEmbedAdder
    {
        public static EmbedBuilder AdderSmall(string Title, string Desc)
        {
            return new EmbedBuilder()
            {
                Title = Title,
                Description = Desc,
                Color = Color.Red
            };
        }

        public static EmbedBuilder AdderInGame(PlayerGameInfo userinfo, string desc)
        {
            List<EmbedFieldBuilder> FieldList = new List<EmbedFieldBuilder>();

            FieldList.Add(new EmbedFieldBuilder()
            {
                Name = "Ranked:",
                Value = userinfo.RankName,
                IsInline = true,
            });

            FieldList.Add(new EmbedFieldBuilder()
            {
                Name = "Team:",
                Value = userinfo.TeamID,
                IsInline = true,
            });

            return new EmbedBuilder()
            {
                Title = userinfo.CharactorName,
                Description = desc,
                Color = userinfo.TeamColor,
                Fields = FieldList,
                ThumbnailUrl = userinfo.CharactorIcon
            };
        }

        public static EmbedBuilder AdderSkin(string uuid, string pricestring)
        {
            data item = JsonConvert.DeserializeObject<data>(uuid);

            GetOffersModels prices = JsonConvert.DeserializeObject<GetOffersModels>(pricestring);

            Offers offer = prices.offers.FirstOrDefault(x => x.OfferID == item.information.uuid);

            return new EmbedBuilder()
            {
                Title = item.information.displayName,
                Description = $"Valorant Points: {offer.cost.CostID} !",
                ThumbnailUrl = item.information.displayIcon,
                Color = Color.Red
            };
        }
    }

    public class Checks
    {
        public static bool CheckIfCorrectDetails(DataModels userinfo, string[] commandinfo)
        {
            var username = commandinfo[0].ToLower();
            var password = commandinfo[1].ToLower();
            var region = commandinfo[2].ToLower();

            if (username == userinfo.valorantDetails.Username.ToLower())
            {
                if (password == Encryption.Decrypt(userinfo.valorantDetails.password, Configuration.EncryptionKey).ToLower())
                {
                    if (region == userinfo.valorantDetails.region.ToLower())
                        return true;
                }
            }
            return false;
        }
    }
}
