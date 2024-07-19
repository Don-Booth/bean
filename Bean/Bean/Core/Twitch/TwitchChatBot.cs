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
using System.Threading.Tasks;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;
using TwitchLib.Client.Enums;
using System.Threading;

namespace Bean.Core.Twitch
{
    internal class TwitchChatBot
    {
        private TwitchAPI API = new TwitchAPI();
        private LiveStreamMonitorService Monitor;
        readonly ConnectionCredentials twitchcreds = new ConnectionCredentials(TwitchInfo.BotUsername, TwitchInfo.AccessToken);
        TwitchClient client;
        DateTime lastAutoHeartRateMessageSent = new DateTime();
        private Task task;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        
        internal async void Connect()
        {
            Console.WriteLine("Twitch] Twitch Bot Connecting");

            client = new TwitchClient();
            client.Initialize(twitchcreds, TwitchInfo.TestChannelName);

            API.Settings.ClientId = TwitchInfo.ClientID;
            API.Settings.AccessToken = TwitchInfo.AccessToken;
            TwitchLib.Api.Services.FollowerService followerService = new FollowerService(API);
            List<string> channelstocheckfollow = new List<string>();
            channelstocheckfollow.Add("PhilRossiMedia");
            followerService.SetChannelsByName(channelstocheckfollow);
            followerService.OnNewFollowersDetected += FollowerService_OnNewFollowersDetected;

            client.OnLog += Client_OnLog;
            client.OnConnected += Client_OnConnected;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnConnectionError += Client_OnConnectionError;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnMessageThrottled += Client_OnMessageThrottled;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnGiftedSubscription += Client_OnGiftedSubscription;
            client.OnDisconnected += Client_OnDisconnected;

            client.Connect();
            LiveMonitor();
            //MessageThrottler();
        }

        private void FollowerService_OnNewFollowersDetected(object sender, TwitchLib.Api.Services.Events.FollowerService.OnNewFollowersDetectedArgs e)
        {
            client.SendMessage(e.Channel, $"{e.NewFollowers} just followed!  ONE OF US ONE OF US ONE OF US!");
        }

        private void Client_OnGiftedSubscription(object sender, OnGiftedSubscriptionArgs e)
        {
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            client.Reconnect();
        }

        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
            {
                client.SendMessage(e.Channel, $"{e.Subscriber.DisplayName} just subscribed via Twitch Prime!  ONE OF US ONE OF US ONE OF US");
            }
            else
            {
                client.SendMessage(e.Channel, $"{e.Subscriber.DisplayName} just subscribed!  ONE OF US ONE OF US ONE OF US");
            }
        }

        private void Client_OnMessageThrottled(object sender, OnMessageThrottledEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"Twitch] Twitch Bot joined channel: {e.Channel}");
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine("Twitch] Twitch Bot Connecting");
        }

        private async void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            CancellationToken token = tokenSource.Token;

            if (e.ChatMessage.Message.StartsWith("hi Bean", StringComparison.InvariantCultureIgnoreCase))
            {
                client.SendMessage(e.ChatMessage.Channel, $"Hi {e.ChatMessage.DisplayName}!");
            }
            else if (e.ChatMessage.Message.Equals("b!quote", StringComparison.InvariantCulture))
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
            else if (e.ChatMessage.Message.Equals("b!starthr", StringComparison.InvariantCulture))
            {
                if (e.ChatMessage.IsBroadcaster || e.ChatMessage.IsModerator)
                {
                    var user = await API.V5.Users.GetUserByNameAsync("PhilRossiMedia");

                    if (user != null)
                    {
                        var stream = await API.V5.Streams.GetStreamByUserAsync(user.Matches[0].Id);
                        if (stream.Stream != null)
                        {
                            client.SendMessage(e.ChatMessage.Channel, $"Phil is currently playing: {stream.Stream.Game}");
                            StartTask(ref token, e);
                        }
                        else
                        {
                            bool isStreaming = await API.V5.Streams.BroadcasterOnlineAsync(user.Matches[0].Id);

                            if (isStreaming)
                            {
                                client.SendMessage(e.ChatMessage.Channel, $"Error retrieving Stream Game");
                                Console.WriteLine($"Twitch] Error retrieving Stream Game");
                            }
                            else
                            {
                                client.SendMessage(e.ChatMessage.Channel, $"{user.Matches[0].Name} is currently not live");
                                Console.WriteLine($"Twitch] {user.Matches[0].Name} is currently not live");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Twitch] ERROR - PhilRossiMedia user not found");
                    }
                }
                //string msg = $"{channel} has been streaming for {(DateTime.Now - stream.Stream.CreatedAt).ToString()} and they've been playing {stream.Stream.Game}.";
            }
            else if (e.ChatMessage.Message.Equals("b!stophr", StringComparison.InvariantCulture))
            {
                if (e.ChatMessage.IsBroadcaster || e.ChatMessage.IsModerator)
                {
                    StopTask();
                }
            }
            else if (e.ChatMessage.Message.Equals("b!gethr", StringComparison.InvariantCulture))
            {
                if (Program.hrtaskrun)
                {
                    if (Program.globalheartrate > 0)
                    {
                        string strResult = Twitch.Commands.Heartrate.GetHearRate();

                        if (strResult != "")
                        {
                            strResult = strResult.Replace("`", "");

                            client.SendMessage(e.ChatMessage.Channel, $"{strResult}");
                        }
                        else
                        {
                            client.SendMessage(e.ChatMessage.Channel, $"Error retrieving heartrate");
                        }
                    }
                    else
                    {
                        if (Bean.Program.hrtaskerror)
                        {
                            client.SendMessage(e.ChatMessage.Channel, $"Heartrate connection to server dropped due to error");
                        }
                        else
                        {
                            client.SendMessage(e.ChatMessage.Channel, $"Heartrate monitor is not currently running");
                        }
                    }
                }
                else
                {
                    if (Bean.Program.hrtaskerror)
                    {
                        client.SendMessage(e.ChatMessage.Channel, $"Heartrate connection to server dropped due to error");
                    }
                    else
                    {
                        client.SendMessage(e.ChatMessage.Channel, $"Heartrate monitor is not currently running");
                    }
                }
            }

            TimeSpan timeSpan = DateTime.Now - lastAutoHeartRateMessageSent;

            if (lastAutoHeartRateMessageSent == DateTime.MinValue || timeSpan.TotalMinutes >= 5)
            {
                if (Bean.Program.globalheartrate > 0)
                {
                    string strResult = Twitch.Commands.Heartrate.GetHearRate();

                    if (strResult != "")
                    {
                        strResult = strResult.Replace("`", "");

                        client.SendMessage(e.ChatMessage.Channel, $"{strResult}");
                        lastAutoHeartRateMessageSent = DateTime.Now;
                    }
                    else
                    {
                        client.SendMessage(e.ChatMessage.Channel, $"Error retrieving heartrate");
                        lastAutoHeartRateMessageSent = DateTime.Now;
                    }
                }
                else
                {
                    lastAutoHeartRateMessageSent = DateTime.Now;
                }
            }
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

        public void LiveMonitor()
        {
            Task.Run(() => ConfigLiveMonitorAsync());
        }

        private async Task ConfigLiveMonitorAsync()
        {
            Monitor = new LiveStreamMonitorService(API, 60);

            List<string> lst = new List<string> { "ID1", "ID2" };
            Monitor.SetChannelsById(lst);

            Monitor.OnStreamOnline += Monitor_OnStreamOnline;
            Monitor.OnStreamOffline += Monitor_OnStreamOffline;
            Monitor.OnStreamUpdate += Monitor_OnStreamUpdate;

            Monitor.OnServiceStarted += Monitor_OnServiceStarted;
            Monitor.OnChannelsSet += Monitor_OnChannelsSet;

            Monitor.Start(); //Keep at the end!
        }

        private void Monitor_OnStreamOnline(object sender, OnStreamOnlineArgs e)
        {
            Console.WriteLine($"Twitch] Stream has started on channel: ${e.Channel} at {e.Stream.StartedAt} - {e.Stream.Title}");
        }

        private void Monitor_OnStreamUpdate(object sender, OnStreamUpdateArgs e)
        {
            Console.WriteLine($"Twitch] Stream has been updated on channel: ${e.Channel} at {e.Stream.StartedAt} - {e.Stream.Title}");
        }

        private void Monitor_OnStreamOffline(object sender, OnStreamOfflineArgs e)
        {
            Console.WriteLine($"Twitch] Stream has ended on channel: ${e.Channel} for {(DateTime.Now - e.Stream.StartedAt).ToString()} - {e.Stream.Title}");
        }

        private void Monitor_OnChannelsSet(object sender, OnChannelsSetArgs e)
        {
            Console.WriteLine($"Twitch] Channels set: {e.Channels.ToString()}");
        }

        private void Monitor_OnServiceStarted(object sender, OnServiceStartedArgs e)
        {
            Console.WriteLine($"Twitch] Monitor Service has started");
        }

        private void StartTask(ref CancellationToken token, OnMessageReceivedArgs e)
        {
            task = Task.Factory.StartNew(() =>
            {
                Bean.Program.hrtaskrun = true;
                _ = Bean.Core.Common.HeartRate.Connect();
            }, token);

            client.SendMessage(e.ChatMessage.Channel, $"Heartrate Task has been started");
            Console.WriteLine($"Twitch] Heartrate Task has been started");
        }

        private void StopTask()
        {
            Bean.Program.hrtaskrun = false;
        }
    }
}