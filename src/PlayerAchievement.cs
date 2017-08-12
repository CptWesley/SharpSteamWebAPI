using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Unlocked Steam Achievement
    public class PlayerAchievement : SSWAObject
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        // Constructor for a player achievement.
        public PlayerAchievement()
        {
            Name = null;
            Date = DateTime.MinValue;
        }

        // Checks if we have info on the name of the achievement.
        public bool HasName()
        {
            return Name != null;
        }

        // Checks if we have info on the achieved date.
        public bool HasDate()
        {
            return Date != DateTime.MinValue;
        }

        // Queries all the achieved achievement info of a player.
        public static PlayerAchievement[] Query(string apikey, long playerId, int appId)
        {
            string url = String.Format("http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid={2}&key={0}&steamid={1}&format=xml", apikey, playerId, appId);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new PlayerAchievement[0];

            XElement[] items = xml.Descendants("achievement").ToArray();
            List<PlayerAchievement> result = new List<PlayerAchievement>();

            for (int i = 0; i < items.Length; ++i)
            {
                PlayerAchievement achievement = Parse(items[i]);
                if (achievement != null)
                    result.Add(achievement);
            }

            return result.ToArray();
        }

        // Parses the xml formatted verbose owned game info to an object.
        private static PlayerAchievement Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);

            if (!parser.GetAttributeBoolean("achieved"))
                return null;

            PlayerAchievement result = new PlayerAchievement
            {
                Name = parser.GetAttributeString("apiname"),
                Date = parser.GetAttributeDate("unlocktime")
            };

            return result;
        }
    }
}
