using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bean.Resources.Database;
using System.Linq;

namespace Bean.Core.Data
{
    //public int QuoteId { get; set; }
    //public string QuoteText { get; set; }
    //public string QuoteAuthor { get; set; }
    //public string QuoteSource { get; set; }
    //public string QuoteDate { get; set; }
    //public string QuoteContributor { get; set; }
    //public string DateAdded { get; set; }

    public static class Data
    {
        public static Quote GetQuote()
        {
            Quote selectedQuote = new Quote();

            using (var DbContext = new SQLiteDbContext())
            {
                int count = DbContext.Quotes.Count();

                if (count > 0)
                {
                    Random random = new Random();
                    int selection = random.Next(0, count - 1);

                    selectedQuote = DbContext.Quotes.Where(x => x.QuoteId == selection).First();
                }
            }

            return selectedQuote;
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
    }
}
