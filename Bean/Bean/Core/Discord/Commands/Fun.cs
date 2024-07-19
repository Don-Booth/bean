using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bean.Core.Discord.Commands
{
    public class Fun : ModuleBase<SocketCommandContext>
    {
        [Command("test"), Alias("testing"), Summary("Test")]
        public async Task Test()
        {
            await Context.Channel.SendMessageAsync("**Test**");
        }

        [Command("binarysolo"), Alias("solo", "binary solo"), Summary("Binary solo!")]
        public async Task BinarySolo()
        {
            await Context.Channel.SendMessageAsync("**Binary Solo!**");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync("0000001");
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("0000011");
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("00000111");
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("**00001111**");
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("0000001");
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("0000011");
            await Task.Delay(500);
            await Context.Channel.SendMessageAsync("00000111");
            await Task.Delay(700);
            await Context.Channel.SendMessageAsync("**Come on sucker lick my battery**");
        }
    }
}
