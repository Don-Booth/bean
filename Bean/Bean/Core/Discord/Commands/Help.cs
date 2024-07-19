using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Bean.Core.Discord.Commands
{
    class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help"), Summary("Help for this bot")]
        public async Task HelpMe()
        {
            await Context.Channel.SendMessageAsync("Help is not yet implemented");
        }
    }
}
