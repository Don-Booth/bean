using System;
using System.Collections.Generic;
using System.Text;

namespace Bean.Core.Twitch.Commands
{
    class Quotes
    {
        internal static string GetQuote()
        {
            string strRandomQuote = "";

            try
            {
                strRandomQuote = Data.Data.GetQuote();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Twitch] Database Error: {ex.Message}");
            }

            return strRandomQuote;
        }
    }
}
