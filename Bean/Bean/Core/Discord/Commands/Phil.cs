using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Bean.Core.Discord.Commands
{
    public class Phil : ModuleBase<SocketCommandContext>
    {
        [Command("hitme"), Alias("info", "phil", "philrossi"), Summary("Basic information about Phil Rossi")]
        public async Task Hitme()
        {
            await Context.Channel.SendMessageAsync("Welcome to **Phil Rossi**'s Discord server");
            await Context.Channel.SendMessageAsync("**Phil** is an *author, musician, podcaster, voice actor and Twitch streamer* (and a nice guy, but we're biased)");
            await Context.Channel.SendMessageAsync("For more information on the above topics, try the commands: *support, books, music, podcasts, voice, twitch, social*");
        }

        [Command("support"), Alias("supportphil", "donate"), Summary("Ways to support Phil Rossi")]
        public async Task Support()
        {
            await Context.Channel.SendMessageAsync("**Phil needs your support to continue making the magic happen!**");
            await Context.Channel.SendMessageAsync("Support Phil on Patreon: https://patreon.com/philrossi");
            await Context.Channel.SendMessageAsync("Visit Phil on Twitch: https://twitch.tv/philrossimedia");
            await Context.Channel.SendMessageAsync("Visit Phil's website: http://www.philrossimedia.com/");
            await Context.Channel.SendMessageAsync("Donate to Phil: https://streamlabs.com/philrossi");
        }

        [Command("philbooks"), Alias("books"), Summary("Phil's books")]
        public async Task Philbooks()
        {
            await Context.Channel.SendMessageAsync("Info coming soon");
        }

        [Command("philmusic"), Alias("music"), Summary("Phil's music")]
        public async Task Philmusic()
        {
            await Context.Channel.SendMessageAsync("Info coming soon");
        }

        [Command("philpodcasts"), Alias("podcasts", "podcast"), Summary("Phil's podcasts")]
        public async Task Philpodcast()
        {
            await Context.Channel.SendMessageAsync("Info coming soon");
        }

        [Command("philvoice"), Alias("voice"), Summary("Phil's vocal work")]
        public async Task Philvoice()
        {
            await Context.Channel.SendMessageAsync("Info coming soon");
        }

        [Command("philtwitch"), Alias("twitch"), Summary("Phil's Twitch")]
        public async Task Philtwitch()
        {
            await Context.Channel.SendMessageAsync("Info coming soon");
        }

        [Command("philsocial"), Alias("social", "socialmedia"), Summary("Phil's Twitch")]
        public async Task Social()
        {
            await Context.Channel.SendMessageAsync("Visit Phil on Twitter: http://www.twitter.com/philrossi");
            await Context.Channel.SendMessageAsync("Visit Phil on Instagram: http://www.instagram.com/philrossimedia");
        }

        [Command("philstreamschedule"), Alias("schedule", "streamschedule"), Summary("Phil's Streaming Schedule")]
        public async Task Schedule()
        {
            await Context.Channel.SendMessageAsync("Monday: Lunch Hour Live Stream 12 EST");
            await Context.Channel.SendMessageAsync("Tuesday: Scream Stream Tuesdays 8:30 EST");
            await Context.Channel.SendMessageAsync("Thursday: Scream Stream VR Thursdays 9:15 EST");
            await Context.Channel.SendMessageAsync("Friday: Lunch Hour Live Stream 12:30 EST");
            await Context.Channel.SendMessageAsync("~Times can vary depending on the week, keep an eye on social media.");
        }

        [Command("philquote"), Alias("quote"), Summary("Quotes from Phil Rossi")]
        public async Task Quote()
        {
            List<string> lstPhilQuotes = new List<string>();

            lstPhilQuotes.Add("'The cat is trying to get in here - at least I hope that's what that sound is.If this never airs, then it wasn't the cat.' - Phil Rossi, Behind the Podcast: Jan 2019");
            lstPhilQuotes.Add("'On + banana is good.Sometimes I like to halve a banana longways, spread some pb on it and call it a day or at least call it a snack' - Phil Rossi, PhilRossiMedia Discord Server - Jan 20, 2019");
            lstPhilQuotes.Add("'Old man Rossi is gonna play some video games!' - Phil Rossi, PhilRossiMedia Scream Stream - Jan 30, 2019");

            Random rnd = new Random();
            int r = rnd.Next(lstPhilQuotes.Count);

            await Context.Channel.SendMessageAsync($"```{lstPhilQuotes[r]}```");
        }

        [Command("aboutphil"), Alias("tell me about phil", "about"), Summary("Phil Rossi Information")]
        public async Task AboutPhil()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Phil Rossi", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithColor(Color.Red);
            Embed.WithImageUrl("http://www.thephilrossiexperience.com/philrossinet/wp-content/uploads/2011/09/banner.jpg");
            //Embed.WithThumbnailUrl("http://specficmedia.com/files/2011/11/PhilRossi.jpg");
            Embed.WithFooter("Last updated: 2019-02-03 11:25 AM");
            Embed.WithDescription("```Phil Rossi is an author, musician, producer, podcaster, and voice actor.  His writing, podcasts, and music have been nominated for numerous awards over the years. In 2006, Phil released his debut novel Crescent as a podcast, and in in 2007,  Crescent was released in book form, quickly becoming an Amazon best-seller. Phil has a passion for story-telling matched only by the pleasure he derives from keeping his fans awake at night. Since it’s initial release, Crescent has lured thousands of readers and listeners into a dark, twisted world of nightmares and things that go bump in the night. Phil’s writing has been paralleled to Stephen King, Philip K. Dick, Arthur C. Clarke, and HP Lovecraft. He has a flair for vivid and often chilling imagery that lends itself to engrossing narratives and an undertone of inescapable, creeping dread. Phil lives in outside of Washington, DC in Virginia where he raises his two daughters. He performs as a solo artist and in several bands throughout the DC metropolitan area.```\n" +
             "**[Support Phil on Patreon](https://patreon.com/philrossi)**");

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }

}
