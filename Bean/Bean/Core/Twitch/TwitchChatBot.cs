using System;
using System.Collections.Generic;
using System.Text;

using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client;

using Bean.Data;

namespace Bean.Core.Twitch
{
    internal class TwitchChatBot
    {
        readonly ConnectionCredentials twitchcreds = new ConnectionCredentials(TwitchInfo.BotUsername, TwitchInfo.AccessToken);
        TwitchClient client;

        internal async void Connect()
        {
            Console.WriteLine("Twitch Bot Connecting");

            client = new TwitchClient();
            client.Initialize(twitchcreds, TwitchInfo.TestChannelName);

            client.OnLog += Client_OnLog;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;

            client.Connect();
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.StartsWith("hi Bean", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(e.ChatMessage.Channel, $"Hi {e.ChatMessage.DisplayName} !");
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
            //Console.WriteLine(e.Data);
        }

        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine($"Error: {e.Error}");
        }

        internal void Disconnect()
        {
            Console.WriteLine("Twitch Bot Disconnecting");
        }
    }
}
