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

        [Command("ride"), Alias("takearide"), Summary("Command revolving around Phil's short story Let's Go For A Ride")]
        public async Task Ride()
        {
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username}, let's go for a ride.");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} slips out of the house at 3AM and treks across the empty parking lot");
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} approaches the shopping carts and the entrance door slides open");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} approaches Harry the dragon and plugs the ride in");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} inserts their coins and the ride begins to move.");
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} hears the nightmare chorus of destroyed children's voices from Harry's speaker");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} climbs on top of Harry the dragon");
            await Task.Delay(2000);
            await Context.Channel.SendMessageAsync($"In a vertigyo inducing flash, {Context.Message.Author.Username} sees a dark silhouette against a steaming ocean of glowing magma");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"Flaring yellow eyes turn into twin infernoes, engulfing everything");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} hurtles towards the fires while unspeakable horrors stream past:");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"the faces of people contorted in perpetual agony");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"people being ripped assunder by invisible, malevolent forces");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"rivers of blood and rivers of fire");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync($"The vision is brief but burns itself into {Context.Message.Author.Username}'s brain");
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} screams and weeps");
            await Context.Channel.SendMessageAsync($"**{Context.Message.Author.Username} now understands on a visceral level why the children always cried**");
            await Task.Delay(2000);
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Username} melts off the ride and unplugs it");
        }
    }
}
