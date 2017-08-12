using System;
using System.Linq;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Steam player statistic.
    public class PlayerStatistic : SSWAObject
    {
        public string Name { get; set; }
        public int Value { get; set; }

        // Constructor for a player statistic.
        public PlayerStatistic()
        {
            Name = null;
            Value = -1;
        }

        // Checks if we have info on the name.
        public bool HasName()
        {
            return Name != null;
        }

        // Checks if we have info on the value.
        public bool HasValue()
        {
            return Value != -1;
        }

        // Queries all the achieved achievement info of a player.
        public static PlayerStatistic[] Query(string apikey, long playerId, int appId)
        {
            string url = String.Format("http://api.steampowered.com/ISteamUserStats/GetUserStatsForGame/v0002/?appid={2}&key={0}&steamid={1}&format=xml", apikey, playerId, appId);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new PlayerStatistic[0];

            XElement[] items = xml.Descendants("friend").ToArray();
            PlayerStatistic[] result = new PlayerStatistic[items.Length];

            for (int i = 0; i < items.Length; ++i)
                result[i] = Parse(items[i]);

            return result;
        }

        // Parse xml formatted statistic to an object.
        private static PlayerStatistic Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);
            PlayerStatistic result = new PlayerStatistic
            {
                Name = parser.GetAttributeString("name"),
                Value = parser.GetAttributeInteger("value")
            };

            return result;
        }
    }
}
