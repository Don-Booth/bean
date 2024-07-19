using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Bean.Core.Common
{
    class GetMostRecentPodcastEpisode
    {
        /// <summary>
        /// Gets the latest episode of the Phil Rossi Patreon Podcast and returns it.
        /// </summary>
        /// <returns>string</returns>
        public static async Task<string> GetLatestPodcastEpisode()
        {
            string strTitle = "";

            try
            {
                SyndicationFeed patreonFeed = SyndicationFeed.Load(XmlReader.Create(Data.General.PhilRossiPatreonPodcast));

                foreach (SyndicationItem item in patreonFeed.Items)
                {
                    System.Console.WriteLine($"RSS Feed: {item.Title.Text} - {item.Summary.Text}");
                    strTitle = item.Title.Text;
                    break; // we only want the first one for now.
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"GetLatestPodcastEpisode] Error: {ex.Message}");
            }

            return strTitle;
        }
    }
}