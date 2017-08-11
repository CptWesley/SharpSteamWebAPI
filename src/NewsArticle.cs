using System;
using System.Linq;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Steam news article.
    public class NewsArticle : SSWAObject
    {
        public long Gid { get; set; }
        public int AppId { get; set; }
        public bool IsExternal { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string FeedLabel { get; set; }
        public string FeedName { get; set; }
        public string FeedType { get; set; }
        public DateTime Date { get; set; }

        // Constructor for news articles.
        public NewsArticle()
        {
            Gid = -1;
            AppId = -1;
            Title = null;
            Url = null;
            Author = null;
            Content = null;
            FeedLabel = null;
            FeedName = null;
            FeedType = null;
            Date = DateTime.MinValue;
        }

        // Checks if this article has a gid value.
        public bool HasGid()
        {
            return Gid != -1;
        }

        // Checks if this article has an app id.
        public bool HasAppId()
        {
            return AppId != -1;
        }

        // Checks if this article has a title.
        public bool HasTitle()
        {
            return Title != null;
        }

        // Checks if this article has an url.
        public bool HasUrl()
        {
            return Url != null;
        }

        // Checks if this article has an author.
        public bool HasAuthor()
        {
            return Author != null;
        }

        // Checks if this article has content.
        public bool HasContent()
        {
            return Content != null;
        }

        // Checks if this article has a feed label.
        public bool HasFeedLabel()
        {
            return FeedLabel != null;
        }

        // Checks if this article has a feed name.
        public bool HasFeedName()
        {
            return FeedName != null;
        }

        // Checks if this article has a feed type.
        public bool HasFeedType()
        {
            return FeedType != null;
        }

        // Checks if this article has a date.
        public bool HasDate()
        {
            return Date != DateTime.MinValue;
        }

        // Parse xml formatted news article to an object.
        private static NewsArticle Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);
            NewsArticle result = new NewsArticle
            {
                Gid = parser.GetAttributeLong("gid"),
                AppId = parser.GetAttributeInteger("appid"),
                Title = parser.GetAttributeString("title"),
                Url = parser.GetAttributeString("url"),
                Content = parser.GetAttributeString("contents"),
                Author = parser.GetAttributeString("author"),
                FeedLabel = parser.GetAttributeString("feedlabel"),
                FeedType = parser.GetAttributeString("feed_type"),
                FeedName = parser.GetAttributeString("feedname"),
                Date = parser.GetAttributeDate("date"),
                IsExternal = parser.GetAttributeBoolean("is_external_url")
            };

            return result;
        }

        // Queries news articles of a particular game.
        public static NewsArticle[] Query(int appId, int count, int length)
        {
            NewsArticle[] articles = new NewsArticle[count];

            string url = String.Format("http://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid={0}&count={1}&maxlength={2}&format=xml", appId, count, length);
            XDocument xml = GetXML(url);

            XElement[] items = xml.Descendants("newsitem").ToArray();

            for (int i = 0; i < items.Length; ++i)
                articles[i] = NewsArticle.Parse(items[i]);

            return articles;
        }
    }
}
