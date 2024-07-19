using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bean.Data;
using System.Reflection;
using System.Runtime.Versioning;

using Discord;
using Discord.Commands;

namespace Bean.Core.Discord.Commands
{
    public class Info : ModuleBase<SocketCommandContext>
    {
        [Command("getsysinfo"), Alias("system", "sysinfo", "diag"), Summary("Gives system information")]
        public async Task SysInfo()
        {
            Dictionary<string, string> DictSysInfo = new Dictionary<string, string>();

            try
            {
                string CoreVer = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

                DictSysInfo.Add("Machine Name", Environment.MachineName.ToString());
                DictSysInfo.Add("OS Version", Environment.OSVersion.ToString());
                DictSysInfo.Add("Framework Version", CoreVer);
                DictSysInfo.Add("Thread ID", Environment.CurrentManagedThreadId.ToString());
                DictSysInfo.Add("64-bit OS", Environment.Is64BitOperatingSystem.ToString());
                DictSysInfo.Add("64-bit Process", Environment.Is64BitProcess.ToString());
                DictSysInfo.Add("Processor Count", Environment.ProcessorCount.ToString());
                DictSysInfo.Add("System Page Size", Environment.SystemPageSize.ToString());
                DictSysInfo.Add("Uptime (ticks)", Environment.TickCount.ToString());
                DictSysInfo.Add("Interactive Process", Environment.UserInteractive.ToString());
                DictSysInfo.Add("CLR Version", Environment.Version.ToString());
                DictSysInfo.Add("Working Set", Environment.WorkingSet.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SysInfo Bad Juju] {ex.Message.ToString()}");
            }

            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Bean System Diagnostics", Context.Guild.Owner.GetAvatarUrl());
            Embed.WithColor(Color.Red);
            Embed.WithFooter($"Current Time: {DateTime.Now}");
            Embed.WithDescription("**[Support Phil on Patreon](https://patreon.com/philrossi)**");

            foreach (var entry in DictSysInfo)
            {
                Embed.AddField($"{entry.Key}", $"{entry.Value}", false);
            }

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("captain"), Alias("capt"), Summary("Bean will tell you who his captain is")]
        public async Task Captain()
        {
            await Context.Channel.SendMessageAsync("My Captain's name is Gerald Evans.");
        }
    }
}
