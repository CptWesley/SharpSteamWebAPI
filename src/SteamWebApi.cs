
namespace SharpSteamWebApi
{
    // Base API class where most of the magic happens.
    public class SteamWebApi
    {
        public string Key { get; }

        // Constructor of the API.
        public SteamWebApi(string apikey)
        {
            Key = apikey;
        }

        // Retrieves all news of a specific game.
        public NewsArticle[] GetNews(int appId, int count, int length)
        {
            return NewsArticle.Query(appId, count, length);
        }

        // Retrieves game info.
        public Game GetGame(int appId)
        {
            return Game.Query(Key, appId);
        }

        // Retrieves full player info.
        public Player GetPlayer(long playerId)
        {
            return Player.Query(Key, playerId);
        }

        // Retrieves player summary.
        public PlayerSummary GetPlayerSummary(long playerId)
        {
            return PlayerSummary.Query(Key, playerId);
        }

        // Retrieves player ban info.
        public PlayerBanInfo GetPlayerBanInfo(long playerId)
        {
            return PlayerBanInfo.Query(Key, playerId);
        }

        // Retrieves player owned games info.
        public OwnedGameInfo[] GetOwnedGames(long playerId, bool includeFreeGames, bool verbose)
        {
            if (verbose)
                return VerboseOwnedGameInfo.Query(Key, playerId, includeFreeGames);
            return OwnedGameInfo.Query(Key, playerId, includeFreeGames);
        }

        // Retrieves recently played owned games info.
        public OwnedGameInfo[] GetRecentGames(long playerId, int count, bool verbose)
        {
            if (verbose)
                return VerboseOwnedGameInfo.QueryRecent(Key, playerId, count);
            return OwnedGameInfo.QueryRecent(Key, playerId, count);
        }

        // Retrieves achieved player achievements.
        public PlayerAchievement[] GetPlayerAchievements(long playerId, int appId)
        {
            return PlayerAchievement.Query(Key, playerId, appId);
        }

        public PlayerAchievement[] GetPlayerAchievements(Player player, int appId)
        {
            return GetPlayerAchievements(player.Summary.Id, appId);
        }
    }
}
