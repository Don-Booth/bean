// System
using System;
using System.Reflection;
using System.Threading.Tasks;
using Bean.Core.Twitch;
using Bean.Core.Discord;
// Discord

// Other
// https://github.com/Sjustein/AllRoundBot/

namespace Bean
{
    /// <summary>
    /// This bot was coded with love and fueled by horror movies, Crescent and death metal!!
    /// </summary>
    class Program
    {
        //static void Main(string[] args)
        static void Main()
        {
            new Program().MainAsync().GetAwaiter().GetResult(); // Kicks off an instance of MainAsync.
        }

        private async Task MainAsync()
        {
            DiscordChatBot DBot = new DiscordChatBot();
            DBot.Connect();

            TwitchChatBot TBot = new TwitchChatBot();
            TBot.Connect();

            //Core.Common.HeartRate hr = new Core.Common.HeartRate();
            //hr.HeartRateConnect();

            await Task.Delay(-1); // Delay infinitely otherwise the program would close immediately, defeating the purpose of having a bot.
        }
    }

    #region TODO
    // TODO Add Chess into Bean
    // TODO Move Phil's schedule into the database
    // TODO Allow Quotes to be inserted into the database by using the quote emoji as a reaction on a comment.
    // TODO Add ASP.NET Site to allow configuration of Bean.
    // TODO Add some more Crescent related things
    // TODO Make Bean automatically poll the Podcast RSS feed on an interval instead of manually or on startup only.
    // TODO Get Heartbeat working properly.
    // TODO Bring commands from Discord into Twitch.
    // TODO Add build version as a command to show which build we are on.
    // TODO Check to see if StyleCop is working for VS2019
    #endregion
}