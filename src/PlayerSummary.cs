using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Player summary info.
    public class PlayerSummary : SSWAObject
    {
        // Public data.
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public int Status { get; set; }
        public int Visibility { get; set; }
        public bool Configured { get; set; }
        public DateTime LastSeen { get; set; }
        public bool AllowsComments { get; set; }
        public string Url { get; set; }
        public string AvatarSmall { get; set; }
        public string AvatarMedium { get; set; }
        public string AvatarLarge { get; set; }

        // Private data.
        public string Name { get; set; }
        public long PrimaryClan { get; set; }
        public DateTime CreationDate { get; set; }
        public long CityId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public int AppId { get; set; }
        public string AppInfo { get; set; }
        public IPAddress ServerIp { get; set; }
        public int ServerPort { get; set; }

        // Other
        public long SharedGameOwner { get; set; }

        // Constructor for player summaries.
        public PlayerSummary()
        {
            Id = -1;
            DisplayName = null;
            Status = -1;
            Visibility = -1;
            Configured = false;
            LastSeen = DateTime.MinValue;
            AllowsComments = false;
            Url = null;
            AvatarSmall = null;
            AvatarMedium = null;
            AvatarLarge = null;
            Name = null;
            PrimaryClan = -1;
            CreationDate = DateTime.MinValue;
            CityId = -1;
            Country = null;
            State = null;
            AppId = -1;
            AppInfo = null;
            ServerIp = null;
            ServerPort = -1;
            SharedGameOwner = -1;
        }

        // Checks if this player has an ID.
        public bool HasId()
        {
            return Id != -1;
        }

        // Checks if this player has a display name.
        public bool HasDisplayName()
        {
            return DisplayName != null;
        }

        // Checks if this player has a status.
        public bool HasStatus()
        {
            return Status != -1;
        }

        // Checks if this player has an visibility.
        public bool HasVisibility()
        {
            return Visibility != -1;
        }

        // Checks if this player has a last known date.
        public bool HasLastSeen()
        {
            return LastSeen != DateTime.MinValue;
        }

        // Checks if this player has an URL.
        public bool HasUrl()
        {
            return Url != null;
        }

        // Checks if this player has a small avatar.
        public bool HasAvatarSmall()
        {
            return AvatarSmall != null;
        }

        // Checks if this player has a medium avatar.
        public bool HasAvatarMedium()
        {
            return AvatarMedium != null;
        }

        // Checks if this player has a large avatar.
        public bool HasAvatarLarge()
        {
            return AvatarLarge != null;
        }

        // Checks if this player has a name.
        public bool HasName()
        {
            return Name != null;
        }

        // Checks if this player has a primary clan.
        public bool HasPrimaryClan()
        {
            return PrimaryClan != -1;
        }

        // Checks if this player has a creation date.
        public bool HasCreationDate()
        {
            return CreationDate != DateTime.MinValue;
        }

        // Checks if this player has a city ID.
        public bool HasCityId()
        {
            return CityId != -1;
        }

        // Checks if this player has a country code.
        public bool HasCountry()
        {
            return Country != null;
        }

        // Checks if this player has a state code.
        public bool HasState()
        {
            return State != null;
        }

        // Checks if this player has an application id.
        public bool HasAppId()
        {
            return AppId != -1;
        }

        // Checks if this player has application info.
        public bool HasAppInfo()
        {
            return AppInfo != null;
        }

        // Checks if this player has a server ip.
        public bool HasServerIp()
        {
            return ServerIp != null;
        }

        // Checks if this player has a server port.
        public bool HasServerPort()
        {
            return ServerPort != -1;
        }

        // Checks if this player has info on the owner of the shared game he is playing.
        public bool HasSharedGameOwner()
        {
            return SharedGameOwner != -1;
        }

        // Queries player summaries.
        public static PlayerSummary Query(string apikey, long playerId)
        {
            string url = String.Format("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={0}&steamids={1}&format=xml", apikey, playerId);
            XDocument xml = GetXML(url);

            if (xml == null)
                return null;

            XElement[] players = xml.Descendants("player").ToArray();
            if (players.Length <= 0)
                return null;

            PlayerSummary result = Parse(xml.Descendants("player").ToArray()[0]);
            result.SharedGameOwner = GetSharedGameOwner(apikey, playerId, result.AppId);
            return result;
        }

        // Parses player summaries.
        private static PlayerSummary Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);
            PlayerSummary result = new PlayerSummary
            {
                Id = parser.GetAttributeLong("steamid"),
                DisplayName = parser.GetAttributeString("personaname"),
                Url = parser.GetAttributeString("profileurl"),
                AvatarSmall = parser.GetAttributeString("avatar"),
                AvatarMedium = parser.GetAttributeString("avatarmedium"),
                AvatarLarge = parser.GetAttributeString("avatarfull"),
                Status = parser.GetAttributeInteger("personastate"),
                Visibility = parser.GetAttributeInteger("communityvisibilitystate"),
                Configured = parser.GetAttributeBoolean("profilestate"),
                LastSeen = parser.GetAttributeDate("lastlogoff"),
                AllowsComments = parser.GetAttributeBoolean("commentpermission"),
                Name = parser.GetAttributeString("realname"),
                PrimaryClan = parser.GetAttributeLong("primaryclanid"),
                CreationDate = parser.GetAttributeDate("timecreated"),
                AppId = parser.GetAttributeInteger("gameid"),
                AppInfo = parser.GetAttributeString("gameextrainfo"),
                ServerIp = parser.GetAttributeIpAdress("gameserverip"),
                ServerPort = parser.GetAttributePort("gameserverip"),
                CityId = parser.GetAttributeInteger("loccityid"),
                Country = parser.GetAttributeString("loccountrycode"),
                State = parser.GetAttributeString("locstatecode")
            };

            return result;
        }

        // Parse player shared game owner.
        private static long GetSharedGameOwner(string apikey, long playerId, int appId)
        {
            string url = String.Format("http://api.steampowered.com/IPlayerService/IsPlayingSharedGame/v0001/?key={0}&steamid={1}&appid_playing={2}&format=xml", apikey, playerId, appId);
            XDocument xml = GetXML(url);
            ElementParser parser = new ElementParser(xml.Element("response"));
            return parser.GetAttributeInteger("lender_steamid");
        }
    }
}
