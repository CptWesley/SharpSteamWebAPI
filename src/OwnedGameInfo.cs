using System;
using System.Linq;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // OwnedGameInfo
    public class OwnedGameInfo : SSWAObject
    {
        public int AppId { get; set; }
        public int TotalPlayTime { get; set; }
        public int RecentPlayTime { get; set; }

        // Constructor for info holder of owned games.
        public OwnedGameInfo()
        {
            AppId = -1;
            TotalPlayTime = -1;
            RecentPlayTime = -1;
        }

        // Checks if this OwnedGameInfo has information on an application ID.
        public bool HasAppId()
        {
            return AppId != -1;
        }

        // Checks if this OwnedGameInfo has information on total play time.
        public bool HasTotalPlayTime()
        {
            return TotalPlayTime != -1;
        }

        // Checks if this OwnedGameInfo has information on recent play time.
        public bool HasRecentPlayTime()
        {
            return RecentPlayTime != -1;
        }

        // Queries all the owned game information of a player.
        public static OwnedGameInfo[] Query(string apikey, long playerId, bool includeFreeGames)
        {
            int freeGamesInt = 0;
            if (includeFreeGames)
                freeGamesInt = 1;

            string url = String.Format("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&include_played_free_games={2}&format=xml", apikey, playerId, freeGamesInt);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new OwnedGameInfo[0];

            XElement[] items = xml.Descendants("message").ToArray();
            OwnedGameInfo[] result = new OwnedGameInfo[items.Length];

            for (int i = 0; i < items.Length; ++i)
                result[i] = Parse(items[i]);

            return result;
        }

        // Queries all the verbose owned game information of recently played games of a player.
        public static OwnedGameInfo[] QueryRecent(string apikey, long playerId, int count)
        {
            string url = String.Format("http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key={0}&steamid={1}&count={2}&format=xml", apikey, playerId, count);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new OwnedGameInfo[0];

            XElement[] items = xml.Descendants("message").ToArray();
            OwnedGameInfo[] result = new OwnedGameInfo[items.Length];

            for (int i = 0; i < items.Length; ++i)
                result[i] = Parse(items[i]);

            return result;
        }

        // Parses the xml formatted owned game info to an object.
        private static OwnedGameInfo Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);
            OwnedGameInfo result = new OwnedGameInfo
            {
                AppId = parser.GetAttributeInteger("appid"),
                TotalPlayTime = parser.GetAttributeInteger("playtime_forever"),
                RecentPlayTime = parser.GetAttributeInteger("playtime_2weeks")
            };

            if (result.RecentPlayTime == -1)
                result.RecentPlayTime = 0;

            return result;
        }
    }
}
