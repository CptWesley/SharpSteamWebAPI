using System;

namespace SharpSteamWebApi
{
    // Steam player.
    public class Player
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
        public string ServerIp { get; set; }

        // Constructor for a player.
        public Player()
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
        }
    }
}
