
namespace SharpSteamWebApi
{
    // Steam player.
    public class Player : SSWAObject
    {
        public PlayerSummary Summary { get; set; }
        public PlayerBanInfo BanInfo { get; set; }
        public OwnedGameInfo[] OwnedGames { get; set; }
        public OwnedGameInfo[] RecentGames { get; set; }

        // Constructor for a player.
        public Player()
        {
            Summary = null;
            BanInfo = null;
            OwnedGames = new OwnedGameInfo[0];
            RecentGames = new OwnedGameInfo[0];
        }

        // Checks if this player has a summary.
        public bool HasSummary()
        {
            return Summary != null;
        }

        // Checks if this player has ban info.
        public bool HasBanInfo()
        {
            return BanInfo != null;
        }

        // Checks if we have info on owned games.
        public bool HasOwnedGames()
        {
            return OwnedGames.Length > 0;
        }

        // Checks if we have info on recent games.
        public bool HasRecentGames()
        {
            return RecentGames.Length > 0;
        }

        // Queries all player info.
        public static Player Query(string apikey, long playerId)
        {
            Player player = new Player
            {
                Summary = PlayerSummary.Query(apikey, playerId),
                BanInfo = PlayerBanInfo.Query(apikey, playerId),
                OwnedGames = OwnedGameInfo.Query(apikey, playerId, true),
                RecentGames = OwnedGameInfo.QueryRecent(apikey, playerId, 3)
            };

            return player;
        }
    }
}
