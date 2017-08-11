
namespace SharpSteamWebApi
{
    // Steam player.
    public class Player : SSWAObject
    {
        public PlayerSummary Summary { get; set; }
        public PlayerBanInfo BanInfo { get; set; }

        // Constructor for a player.
        public Player()
        {
            Summary = null;
            BanInfo = null;
        }

        // Queries all player info.
        public static Player Query(string apikey, long playerId)
        {
            Player player = new Player
            {
                Summary = PlayerSummary.Query(apikey, playerId),
                BanInfo = PlayerBanInfo.Query(apikey, playerId)
            };

            return player;
        }
    }
}
