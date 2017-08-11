namespace SharpSteamWebApi
{
    // Ban info container
    public struct PlayerBanInfo
    {
        public bool HasVACBan { get; set; }
        public bool HasCommunityBan { get; set; }
        public bool HasTradeBan { get; set; }
        public int VACBanCount { get; set; }
        public int GameBanCount { get; set; }
        public int DaysSinceLastBan { get; set; }
    }
}
