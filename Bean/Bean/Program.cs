// System
using System;
using System.Reflection;
using System.Threading.Tasks;
using Bean.Core.Twitch;
// Discord
using Discord;
using Discord.Commands;
using Discord.WebSocket;
// Other
// https://github.com/Sjustein/AllRoundBot/

namespace Bean
{
    /// <summary>
    /// This bot was coded with love and fueled by horror movies, Crescent and death metal!!
    /// </summary>
    class Program
    {
        #region Class Variables
        private DiscordSocketClient DiscordClient;
        private CommandService DiscordCommands;
        #endregion

        //static void Main(string[] args)
        static void Main()
        {
            new Program().MainAsync().GetAwaiter().GetResult(); // Kicks off an instance of MainAsync.
        }

        private async Task MainAsync()
        {
            #region Discord Configuration
            DiscordClient = new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Debug });
            DiscordCommands = new CommandService(new CommandServiceConfig { CaseSensitiveCommands = true, DefaultRunMode = RunMode.Async, LogLevel = LogSeverity.Debug });

            DiscordClient.MessageReceived += DiscordClient_MessageReceived; // Fires when someone sends a message in a channel that the bot can read.
            await DiscordCommands.AddModulesAsync(Assembly.GetEntryAssembly(), null); // Adds all commands in the project.

            DiscordClient.Ready += DiscordClient_Ready; // Fires when bot is started and logged in successfully.
            DiscordClient.Log += DiscordClient_Log; // Fires whenever log messages are received (based on log level set above).

            await DiscordClient.LoginAsync(TokenType.Bot, Data.DiscordInfo.TestToken); // Logs the bot in.
            await DiscordClient.StartAsync(); // Start the bot.
            #endregion

            #region Twitch Configuration
            TwitchChatBot TBot = new TwitchChatBot();
            TBot.Connect();
            #endregion

            await Task.Delay(-1); // Delay infinitely otherwise the program would close immediately, defeating the purpose of having a bot.
        }

        private async Task DiscordClient_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task DiscordClient_Ready()
        {
            await DiscordClient.SetStatusAsync(UserStatus.Online); // Sets status to online (should already be so)

            string strTitle = await Core.Common.GetMostRecentPodcastEpisode.GetLatestPodcastEpisode();

            if (strTitle != null)
            {
                await DiscordClient.SetGameAsync($"{strTitle} - Phil Rossi Patreon Exclusive Podcast", "", ActivityType.Listening);
            }
        }
 
        private async Task DiscordClient_MessageReceived(SocketMessage MessageParam)
        {
            //var Message = MessageParam as SocketUserMessage; // Effectively casts SocketMessage as SocketUserMessage so that we can get information about the user and the message.
            var Message = (SocketUserMessage)MessageParam; // Effectively casts SocketMessage as SocketUserMessage so that we can get information about the user and the message.
            var Context = new SocketCommandContext(DiscordClient, Message);

            if (Context.Message == null || Context.Message.Content == "") return; // We don't want to do anything with the message if it's null/empty
            if (Context.User.IsBot) return; // We don't want to pay attention to any bot messages

            int ArgPos = 0; // if the message came from this bot or the message doesn't have the prefix we like, we want to ignore it.
            //if (!(Message.HasStringPrefix("b!", ref ArgPos) || Message.HasMentionPrefix(DiscordClient.CurrentUser, ref ArgPos))) return;
            if (!(Message.HasStringPrefix("t!", ref ArgPos) || Message.HasMentionPrefix(DiscordClient.CurrentUser, ref ArgPos))) return;

            var Result = await DiscordCommands.ExecuteAsync(Context, ArgPos, null); // Execute the command

            if (!Result.IsSuccess)
            { // If the command failed, let's deal with that.
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command.  Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
            }
        }
    }
}
