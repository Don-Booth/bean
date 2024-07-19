// System
using System;
using System.Reflection;
using System.Threading.Tasks;
using Bean.Core.Twitch;
using Bean.Core.Discord;
using Bean.Core.Common;
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
        internal static int globalheartrate = new int();
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

            Task t = HeartRate.Connect();

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
    // TODO Heartrate could auto report on a timer - maybe directly or through nightbot - 10 minute interval.
    // TODO Get Heartbeat working properly - twitch ONLY
    //      - Heart rate task needs to be able to be started and stopped via command and check if task is already running.
    //      - Heart rate should be stored to the database with the value and a timestamp.  That way we can run metrics on it later.
    //      - Heart rate can be retrieved after the fact from the database.
    //      - Heart Rate should also be compared to last rate to determine if a spike happened so it can be announced.
    // TODO Bring commands from Discord into Twitch.
    // TODO Add build version as a command to show which build we are on.
    // TODO Check to see if StyleCop is working for VS2019
    // TODO Allow Phil to update the schedule/put schedule in database
    // TODO Keep Phil's list of currently played games/game queue
    // TODO Movie queue
    #endregion 
}