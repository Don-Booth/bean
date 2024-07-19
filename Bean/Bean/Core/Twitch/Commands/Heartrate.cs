using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client;

namespace Bean.Core.Twitch.Commands
{
    class Heartrate
    {
        internal static string GetHearRate()
        {
            string strHeartRate = "";

            strHeartRate = $"Current heartrate is: {Bean.Program.globalheartrate}";

            return strHeartRate;
        }
    }
}
