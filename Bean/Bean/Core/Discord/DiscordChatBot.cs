using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using Bean.Data;

namespace Bean.Core.Discord
{
    internal class DiscordChatBot
    {
        #region Class Variables
        private DiscordSocketClient DiscordClient;
        private CommandService DiscordCommands;
        #endregion

        internal async void Connect()
        {
            DiscordClient = new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Debug });
            DiscordCommands = new CommandService(new CommandServiceConfig { CaseSensitiveCommands = true, DefaultRunMode = RunMode.Async, LogLevel = LogSeverity.Debug });

            DiscordClient.MessageReceived += DiscordClient_MessageReceived; // Fires when someone sends a message in a channel that the bot can read.
            await DiscordCommands.AddModulesAsync(Assembly.GetEntryAssembly(), null); // Adds all commands in the project.

            DiscordClient.Ready += DiscordClient_Ready; // Fires when bot is started and logged in successfully.
            DiscordClient.Log += DiscordClient_Log; // Fires whenever log messages are received (based on log level set above).

            await DiscordClient.LoginAsync(TokenType.Bot, DiscordInfo.TestToken); // Logs the bot in.
            await DiscordClient.StartAsync(); // Start the bot.
        }

        private async Task DiscordClient_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task DiscordClient_Ready()
        {
            await DiscordClient.SetStatusAsync(UserStatus.Online); // Sets status to online (should already be so)

            string strTitle = await Core.Common.GetMostRecentPodcastEpisode.GetLatestPodcastEpisode();

            if (strTitle != null && strTitle != "")
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

            if ((Message.HasMentionPrefix(DiscordClient.CurrentUser, ref ArgPos)) && (Message.Content.ToLower().Contains("play a game")))
            {
                await Context.Channel.SendMessageAsync("A strange game. The only winning move is not to play. How about a nice game of chess?");
            }
            
            if (!(Message.HasStringPrefix("b!", ref ArgPos) || Message.HasMentionPrefix(DiscordClient.CurrentUser, ref ArgPos))) return;

            var Result = await DiscordCommands.ExecuteAsync(Context, ArgPos, null); // Execute the command

            if (!Result.IsSuccess)
            { // If the command failed, let's deal with that.
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with executing a command.  Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
            }
        }
    }
}