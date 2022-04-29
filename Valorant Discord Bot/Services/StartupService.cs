using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Valorant_Discord_Bot.Utilities;

namespace Valorant_Discord_Bot.Services
{
    class StartupService
    {
        private static IServiceProvider _prividor;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        public StartupService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfigurationRoot config)
        {
            _prividor = provider;
            _config = config;
            _discord = discord;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            string token = Configuration.DiscordToken;
            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("Please enter in your token");
                return;
            }

            await _discord.LoginAsync(TokenType.Bot, token);
            await _discord.StartAsync();

            await _discord.SetStatusAsync(UserStatus.DoNotDisturb);
            await _discord.SetActivityAsync(new Game("Created by 0xkaede", ActivityType.Playing, ActivityProperties.None)).ConfigureAwait(false);
            await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), _prividor);
        }
    }
}
