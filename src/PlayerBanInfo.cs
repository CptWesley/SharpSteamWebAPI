using System;
using System.Linq;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Steam player ban info container.
    public class PlayerBanInfo : SSWAObject
    {
        public bool HasVACBan { get; set; }
        public bool HasCommunityBan { get; set; }
        public bool HasTradeBan { get; set; }
        public int VACBanCount { get; set; }
        public int GameBanCount { get; set; }
        public int DaysSinceLastBan { get; set; }

        // Constructor for player ban info.
        public PlayerBanInfo()
        {
            HasVACBan = false;
            HasCommunityBan = false;
            HasTradeBan = false;
            VACBanCount = -1;
            GameBanCount = -1;
            DaysSinceLastBan = -1;
        }

        // Checks if this player ban info has a VAC ban count.
        public bool HasVACBanCount()
        {
            return VACBanCount != -1;
        }

        // Checks if this player ban info has a game ban count.
        public bool HasGameBanCount()
        {
            return GameBanCount != -1;
        }

        // Checks if this player ban info has days since last ban info.
        public bool HasDaysSinceLastBan()
        {
            return DaysSinceLastBan != -1;
        }

        // Queries player bans.
        public static PlayerBanInfo Query(string apikey, long playerId)
        {
            string url = String.Format("http://api.steampowered.com/ISteamUser/GetPlayerBans/v1/?key={0}&steamids={1}&format=xml", apikey, playerId);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new PlayerBanInfo();

            return Parse(xml.Descendants("player").ToArray()[0]);
        }

        // Parses player bans
        private static PlayerBanInfo Parse(XElement xml)
        {
            if (xml == null)
                return new PlayerBanInfo();

            ElementParser parser = new ElementParser(xml);
            PlayerBanInfo result = new PlayerBanInfo
            {
                HasVACBan = parser.GetAttributeBoolean("VACBanned"),
                HasCommunityBan = parser.GetAttributeBoolean("CommunityBanned"),
                HasTradeBan = parser.GetAttributeBoolean("EconomyBan"),
                VACBanCount = parser.GetAttributeInteger("NumberOfVACBans"),
                GameBanCount = parser.GetAttributeInteger("NumberOfGameBans"),
                DaysSinceLastBan = parser.GetAttributeInteger("DaysSinceLastBan")
            };

            return result;
        }
    }
}
