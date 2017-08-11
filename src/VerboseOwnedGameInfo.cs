
using System;
using System.Linq;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Steam owned game info verbose.
    public class VerboseOwnedGameInfo : OwnedGameInfo
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Logo { get; set; }
        public bool HasGameStats { get; set; }

        // Constructor for the verbose owned game info.
        public VerboseOwnedGameInfo()
        {
            AppId = -1;
            TotalPlayTime = -1;
            RecentPlayTime = -1;

            Title = null;
            Icon = null;
            Logo = null;
            HasGameStats = false;
        }
        
        // Checks if this game has a title.
        public bool HasTitle()
        {
            return Title != null;
        }

        // Checks if this game has an icon.
        public bool HasIcon()
        {
            return Icon != null;
        }

        // Checks if this game has a logo.
        public bool HasLogo()
        {
            return Logo != null;
        }

        // Queries all the verbose owned game information of a player.
        public static VerboseOwnedGameInfo[] Query(string apikey, long playerId, bool includeFreeGames)
        {
            int freeGamesInt = 0;
            if (includeFreeGames)
                freeGamesInt = 1;

            string url = String.Format("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&include_played_free_games={2}&format=xml", apikey, playerId, freeGamesInt);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new VerboseOwnedGameInfo[0];

            XElement[] items = xml.Descendants("message").ToArray();
            VerboseOwnedGameInfo[] result = new VerboseOwnedGameInfo[items.Length];

            for (int i = 0; i < items.Length; ++i)
                result[i] = Parse(items[i]);

            return result;
        }

        // Parses the xml formatted verbose owned game info to an object.
        private static VerboseOwnedGameInfo Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);
            VerboseOwnedGameInfo result = new VerboseOwnedGameInfo
            {
                AppId = parser.GetAttributeInteger("appid"),
                TotalPlayTime = parser.GetAttributeInteger("playtime_forever"),
                RecentPlayTime = parser.GetAttributeInteger("playtime_2weeks"),
                Title = parser.GetAttributeString("name"),
                Icon = parser.GetAttributeString("img_icon_url"),
                Logo = parser.GetAttributeString("img_logo_url"),
                HasGameStats = parser.GetAttributeBoolean("has_community_visible_stats")
            };

            if (result.RecentPlayTime == -1)
                result.RecentPlayTime = 0;

            return result;
        }
    }
}
