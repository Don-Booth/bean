using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bean.Data;

using Discord;
using Discord.Commands;

using System.ServiceModel.Syndication;
using System.Xml;

namespace Bean.Core.Discord.Commands
{
    public class ListeningTo : ModuleBase<SocketCommandContext>
    {
        [Command("listen"), Alias("listento"), Summary("Tell user more info on what Bean is listening to")]
        public async Task Listen()
        {
            //await Context.Channel.SendMessageAsync("Welcome to **Phil Rossi**'s Discord server");
        }

        [Command("getlisten"), Alias("getlisteningto"), Summary("Checks podcast feed's latest episode")]
        public async Task GetListen()
        {
            string strTitle = await Core.Common.GetMostRecentPodcastEpisode.GetLatestPodcastEpisode();

            if (strTitle != null)
            {
                await Context.Client.SetGameAsync($"{strTitle} - Phil Rossi Patreon Exclusive Podcast", "", ActivityType.Listening);
                await Context.Channel.SendMessageAsync($"I'm listening to **{strTitle}**, the latest episode of **Phil Rossi's Patreon Exclusive Podcast**.  **https://patreon.com/philrossi** to join in the scares!");
            }
        }
    }
}
