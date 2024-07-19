using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;
using Discord.WebSocket;

namespace Bean.Core.Common
{
    public class Quotes : ModuleBase<SocketCommandContext>
    {
        //Quote
        // create
        // get

        [Group("quote"), Alias("quotes"), Summary("Group to manage quote commands")]
        public class QuotesGroup : ModuleBase<SocketCommandContext>
        {
            [Command(""), Alias("get", "getrandom", "random"), Summary("Gets a random quote")]
            public async Task GetRandomQuote()
            {

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


                await Context.Channel.SendMessageAsync($":tada: {User1.Mention} has inserted a quote");
            }
        }
    }
}