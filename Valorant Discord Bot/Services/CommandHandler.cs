using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valorant_Discord_Bot.Utilities;

namespace Valorant_Discord_Bot.Services
{
    public class CommandHandler
    {
        private static IServiceProvider _prividor;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        public CommandHandler(DiscordSocketClient discord, CommandService commands, IConfigurationRoot config, IServiceProvider provider)
        {
            _prividor = provider;
            _config = config;
            _discord = discord;
            _commands = commands;

            _discord.Ready += OnReady;
            _discord.MessageReceived += OnMessageRecived;
            _discord.JoinedGuild += OnJoinedGuild;
            _discord.LeftGuild += OnJLeftGuild;
        }

        private async Task OnJLeftGuild(SocketGuild Guild)
            => Logs.Guilds($"We Left {Guild.Name}");

        private async Task OnJoinedGuild(SocketGuild Guild)
            => Logs.Guilds($"We Joined {Guild.Name}");

        private async Task OnMessageRecived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;

            var context = new SocketCommandContext(_discord, msg);

            int pos = 0;
            if (msg.HasStringPrefix(Configuration.DiscordPrefix, ref pos) || msg.HasMentionPrefix(_discord.CurrentUser, ref pos))
            {
                var usercommand = context.User.Username + "#" + context.User.Discriminator;
                var result = await _commands.ExecuteAsync(context, pos, _prividor);
                if (!result.IsSuccess)
                {
                    var reason = result.Error;

                    await context.Channel.SendMessageAsync($"The following error occured: \n {reason}");

                    Logs.Error($"The following error occured from an executed command from {usercommand}: {reason}");
                }
            }
        }

        private Task OnReady()
        {
            var TOKEN = Configuration.DiscordToken.GetLast(18);
            Logs.Info($"Discord bot created in Discord.NET, by 0xkaede");
            Logs.Info($"Connected as username: {_discord.CurrentUser.Username}#{_discord.CurrentUser.Discriminator}");
            Logs.Info($"Connected as ID: {_discord.CurrentUser.Id}");
            Logs.Info($"Bot using Token that ends with: {TOKEN}");
            Logs.Guilds($"Bot is currently in {_discord.Guilds.Count} Guild");
            foreach (var guild in _discord.Guilds)
            {
                string[] guildinfo = { guild.Name };

                Logs.Guilds($"{guildinfo[0]}");
            }
            return Task.CompletedTask;
        }
    }
}
