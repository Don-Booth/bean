using System;
using System.Collections.Generic;
using System.Text;

using TwitchLib.Api.Core.Models.Undocumented.Chatters;
using TwitchLib.Api.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;
using TwitchLib.Api;
using TwitchLib.Api.Services;
using TwitchLib.Api.Services.Events;

using Bean.Data;

namespace Bean.Core.Twitch
{
    internal class TwitchChatBot
    {
        readonly ConnectionCredentials twitchcreds = new ConnectionCredentials(TwitchInfo.BotUsername, TwitchInfo.AccessToken);
        TwitchClient client;

        internal async void Connect()
        {
            Console.WriteLine("Twitch] Twitch Bot Connecting");

            client = new TwitchClient();
            client.Initialize(twitchcreds, TwitchInfo.TestChannelName);

            client.OnLog += Client_OnLog;
            client.OnConnected += Client_OnConnected;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;

            client.Connect();
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"Twitch] Twitch Bot joined channel: ${e.Channel}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine("Twitch] Twitch Bot Connecting");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            TwitchAPI api = new TwitchAPI();
            var temp = api.V5.Streams.GetStreamByUserAsync("PhilRossiMedia");
            //TwitchLib.Communication.Services.Throttlers


            if (e.ChatMessage.Message.StartsWith("hi Bean", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(e.ChatMessage.Channel, $"Hi {e.ChatMessage.DisplayName}!");
            }

            if (e.ChatMessage.Message.Equals("b!quote", StringComparison.InvariantCulture))
            {
                string strResult = Twitch.Commands.Quotes.GetQuote();

                if (strResult != "")
                {
                    strResult = strResult.Replace("`", "");

                    client.SendMessage(e.ChatMessage.Channel, $"{strResult}");
                }
                else
                {
                    client.SendMessage(e.ChatMessage.Channel, $"Error retrieving quote");
                }
            }

            //if (e.ChatMessage.Message.StartsWith("!heartrate", StringComparison.InvariantCultureIgnoreCase))
            //{
            //HeartBeat hb = new HeartBeat();
            //int hbtest = hb.GetHeartbeat();
            //client.SendMessage(e.ChatMessage.Channel, $"Phil's current heartrate is : {hbtest.ToString()}");
            //}
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"Twitch] {e.Data}");
        }

        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine($"Twitch] Error: {e.Error}");
        }

        internal void Disconnect()
        {
            Console.WriteLine("Twitch] Twitch Bot Disconnecting");
        }
    }
}