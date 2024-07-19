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

            await Task.Delay(-1); // Delay infinitely otherwise the program would close immediately, defeating the purpose of having a bot.
        }
    }
}
