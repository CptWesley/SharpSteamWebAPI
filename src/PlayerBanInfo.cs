namespace SharpSteamWebApi
{
    // Steam player ban info container.
    public class PlayerBanInfo
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
    }
}
