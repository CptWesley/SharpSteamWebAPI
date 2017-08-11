
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
    }
}
