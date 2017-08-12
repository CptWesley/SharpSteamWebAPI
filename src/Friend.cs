using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Steam friend.
    public class Friend : SSWAObject
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int Relationship { get; set; }

        // Constructor of a friend.
        public Friend()
        {
            Id = -1;
            Date = DateTime.MinValue;
            Relationship = -1;
        }

        // Checks if Id info is known.
        public bool HasId()
        {
            return Id != -1;
        }

        // Checks if date info is known.
        public bool HasDate()
        {
            return Date != DateTime.MinValue;
        }

        // Checks if relationship info is known.
        public bool HasRelationship()
        {
            return Relationship != -1;
        }

        // Queries all the achieved achievement info of a player.
        public static Friend[] Query(string apikey, long playerId)
        {
            string url = String.Format("http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key={0}&steamid={1}&format=xml", apikey, playerId);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new Friend[0];

            XElement[] items = xml.Descendants("friend").ToArray();
            Friend[] result = new Friend[items.Length];

            for (int i = 0; i < items.Length; ++i)
                result[i] = Parse(items[i]);

            return result;
        }

        // Parses the xml formatted verbose owned game info to an object.
        private static Friend Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);

            if (!parser.GetAttributeBoolean("achieved"))
                return null;

            Friend result = new Friend
            {
                Id = parser.GetAttributeLong("steamid"),
                Date = parser.GetAttributeDate("friend_since"),
                Relationship = GetRelationshipInt(parser.GetAttributeString("relationship"))
            };

            return result;
        }

        // Get the right integer of the relationship type.
        private static int GetRelationshipInt(string code)
        {
            switch (code)
            {
                case "friend":
                    return SharpSteamWebApi.Relationship.Friend;
                default:
                    return SharpSteamWebApi.Relationship.Unknown;
            }
        }
    }
}
