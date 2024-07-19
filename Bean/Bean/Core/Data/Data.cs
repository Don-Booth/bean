using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bean.Resources.Database;
using System.Linq;
using MoreLinq;

namespace Bean.Core.Data
{
    public static class Data
    {
        #region quotes
        public static string GetQuote()
        {
            using (var DbContext = new SQLiteDbContext())
            {
                int intQouteCount = DbContext.Quotes.Count();

                if (intQouteCount > 0)
                {
                    Random random = new Random();
                    var intRecord = random.Next(1, intQouteCount);

                    string strResult = "";
                    Quote SelectedQuote = DbContext.Quotes.Where(x => x.QuoteId == intRecord).FirstOrDefault();
                    strResult = $"```'{SelectedQuote.QuoteText}' - {SelectedQuote.QuoteAuthor}, {SelectedQuote.QuoteSource}, Date: {SelectedQuote.QuoteDate} | Contributed by: {SelectedQuote.QuoteContributor} on {SelectedQuote.DateAdded}```";

                    return strResult;
                }
                else
                {
                    return "";
                }
            }
        }

        public static async void SaveQuote(string QuoteText, string QuoteAuthor, string QuoteSource, string QuoteDate, string QuoteContributor)
        {
            using (var DbContext = new SQLiteDbContext())
            {
                DbContext.Quotes.Add(new Quote
                {
                    QuoteText = QuoteText,
                    QuoteAuthor = QuoteAuthor,
                    QuoteSource = QuoteSource,
                    QuoteDate = QuoteDate,
                    QuoteContributor = QuoteContributor,
                    DateAdded = DateTime.Now.ToString()
                });

                await DbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region heartbeat
        public static HeartBeat GetHeartRate()
        {
            HeartBeat hb = null;

            using (var DbContext = new SQLiteDbContext())
            {
                hb = DbContext.HeartBeats.MaxBy(x => x.HeartRate).FirstOrDefault();

                return hb;
            }
        }

        public static async void SaveHeartRate(int HeartRate, ulong HeartRateTimestamp, string HeartRateGamePlayed)
        {
            using (var DbContext = new SQLiteDbContext())
            {
                DbContext.HeartBeats.Add(new HeartBeat
                {
                    HeartRate = HeartRate,
                    HeartRateTimestamp = HeartRateTimestamp,
                    HeartRateGamePlayed = HeartRateGamePlayed
                });

                await DbContext.SaveChangesAsync();
            }
        }
        #endregion
    }
}
