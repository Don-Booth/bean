using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Bean.Resources.Database;
using System.Linq;

namespace Bean.Core.Discord.Commands
{
    public class Quotes : ModuleBase<SocketCommandContext>
    {
        [Group("quote"), Alias("quotes"), Summary("Group to manage quote commands")]
        public class QuotesGroup : ModuleBase<SocketCommandContext>
        {
            [Command(""), Alias("get", "getrandom", "random", "randomquote"), Summary("Gets a random quote")]
            public async Task GetRandomQuote()
            {
                try
                {
                    string strResult = Data.Data.GetQuote();

                    if (strResult != "")
                    {
                        await Context.Channel.SendMessageAsync($"{strResult}");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($":x: Error retrieving quote");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            [Command("save"), Alias("insert", "store"), Summary("Inserts a quote into the database")]
            public async Task InsertQuote(string QuoteText, string QuoteAuthor, string QuoteSource, string QuoteDate)
            {
                //quote save quotetext quoteauthor quotesource quotedate
                //QuoteId , QuoteText, QuoteAuthor, QuoteSource, QuoteDate, QuoteContributor, DateAdded

                // Validation
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (Context.User.IsBot || !User1.GuildPermissions.Administrator)
                {
                    await Context.Channel.SendMessageAsync(":x: Only a human admin may add a quote at this time");
                    return;
                }

                if ((QuoteText == null || QuoteText == "") || (QuoteAuthor == null || QuoteAuthor == "") || (QuoteSource == null || QuoteSource == "") || (QuoteDate == null || QuoteDate == ""))
                {
                    await Context.Channel.SendMessageAsync(":x: Check your parameters.  Please us the format: quote save quotetext quoteauthor quotesource quotedate");
                    return;
                }

                // Save data
                //await Data.Data.SaveQuote();
                await Context.Channel.SendMessageAsync($":tada: {User1.Mention} has inserted a quote");
            }
        }
    }
}