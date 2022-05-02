using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valorant_Discord_Bot.DataBase;
using Valorant_Discord_Bot.Store.Util;
using Valorant_Discord_Bot.Utilities;
using Valorant_Discord_Bot.Valorant.Auth;
using Valorant_Discord_Bot.Valorant.CurrentGame;
using static Valorant_Discord_Bot.DataBase.DataEmums;

namespace Valorant_Discord_Bot.CommandsServices
{
    public class AdminCommands : ModuleBase
    {
        [Command("Encrypt")]
        public async Task Encrypt(params string[] args)
        {
            var cleartext = args[0];
            var EncryptionKey = args[1];

            if (Context.User.Id == 653269276274458635)
                await Context.Channel.SendMessageAsync(Encryption.Encrypt(cleartext, EncryptionKey));
        }

        [Command("ListUsers")]
        public async Task ListUsers(params string[] args)
        {
            var whatname = args[0];

            if (Context.User.Id != 653269276274458635)
                return;

            await Context.Channel.SendMessageAsync(Manager.ListEveryone());
        }

        [Command("Help")]
        public async Task help()
        {
            List<EmbedFieldBuilder> FieldList = new List<EmbedFieldBuilder>();

            FieldList.Add(new EmbedFieldBuilder()
            {
                Name = "User Commands",
                Value = $"**{Configuration.DiscordPrefix}adduser <RiotUsername> <RiotPassword> <RiotRegion>** - This adds your user to our database to allow you to use Valorant Commands.\n" +
                $"**{Configuration.DiscordPrefix}deluser <RiotUsername>** - This will only work if you added user with your discord account.\n" +
                $"**{Configuration.DiscordPrefix}updatepass <RiotUsername> <RiotNewPassword> **- Only works if you added user with this account.\nIf you need anymore help please join our [Support Server]({Configuration.DiscordServer})."
            });

            FieldList.Add(new EmbedFieldBuilder()
            {
                Name = "Valorant Commands",
                Value = $"**Note:** Make sure you Added your user before using these commands, More info under UserCommands.\n" +
                $"**{Configuration.DiscordPrefix}store <RiotUsername>** - Sends your daily store.\n**" +
                $"{Configuration.DiscordPrefix}currentgame <RiotUsername>** - Sends your current game information, This includes players ranks, level and Character."
            });

            var footer = new EmbedFooterBuilder()
            {
                Text = "Created by 0xkaede",
            };

            var embed = new EmbedBuilder()
            {
                Title = "Help - Commands",
                Description = "Here is a life of every commands for our bot!",
                Color = Color.Red,
                Fields = FieldList,
                Timestamp = DateTime.Now,
                Footer = footer
            };

            await Context.Channel.SendMessageAsync(null, false, embed.Build());
        }

        [Command("Test")]
        public async Task Test(params string[] args)
        {
            var userdata = "";
            var puuid = args[0];

            var username = "whey1k";

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

            Authentication.GetUserInfo(ValorantAuthCommands.AccessToken);

            CompetitiveUpdatesModels competitiveupdates = JsonConvert.DeserializeObject<CompetitiveUpdatesModels>(CurrentGameRequest.GetFetchCompetitiveUpdates(puuid, "eu"));

            CompetitiveTiersModels competitivetiers = JsonConvert.DeserializeObject<CompetitiveTiersModels>(CurrentGameRequest.GetCompetitiveTiers());

            Tiers tier = competitivetiers.data.tiers.FirstOrDefault(x => x.tier == competitiveupdates.matches.FirstOrDefault().TierAfterUpdate);

            await Context.Channel.SendMessageAsync(tier.tierName + " : " + tier.tier);

        }
    }
}
