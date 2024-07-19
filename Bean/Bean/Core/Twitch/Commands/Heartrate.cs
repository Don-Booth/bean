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
        //[Command("kickstartmyheart"), Alias("kickheart"), Summary("Start the Heart Rate Websocket Service")]
        //public async Task KickstartMyHeart()
        //{
            //s suggested by Jon Skeet, the Task.IsCompleted is the better option.
            //if (task != null && (task.Status == TaskStatus.Running || task.Status == TaskStatus.WaitingToRun || task.Status == TaskStatus.WaitingForActivation))
            //{
            //    Logger.Log("Task has attempted to start while already running");
            //}
            //else
            //{
            //    Logger.Log("Task has began");

            //    task = Task.Factory.StartNew(() =>
            //    {
            //        // Stuff                
            //    });
        //    //}
        //}
    }
}
