using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valorant_Discord_Bot.Utilities;

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
    }
}
