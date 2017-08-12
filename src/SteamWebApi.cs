
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

        public Player GetPlayer(Friend friend)
        {
            return GetPlayer(friend.Id);
        }

        // Retrieves player summary.
        public PlayerSummary GetPlayerSummary(long playerId)
        {
            return PlayerSummary.Query(Key, playerId);
        }

        public PlayerSummary GetPlayerSummary(Friend friend)
        {
            return GetPlayerSummary(friend.Id);
        }

        // Retrieves player ban info.
        public PlayerBanInfo GetPlayerBanInfo(long playerId)
        {
            return PlayerBanInfo.Query(Key, playerId);
        }

        public PlayerBanInfo GetPlayerBanInfo(Friend friend)
        {
            return GetPlayerBanInfo(friend.Id);
        }

        // Retrieves player owned games info.
        public OwnedGameInfo[] GetOwnedGames(long playerId, bool includeFreeGames, bool verbose)
        {
            if (verbose)
                return VerboseOwnedGameInfo.Query(Key, playerId, includeFreeGames);
            return OwnedGameInfo.Query(Key, playerId, includeFreeGames);
        }

        public OwnedGameInfo[] GetOwnedGames(Friend friend, bool includeFreeGames, bool verbose)
        {
            return GetOwnedGames(friend.Id, includeFreeGames, verbose);
        }

        // Retrieves recently played owned games info.
        public OwnedGameInfo[] GetRecentGames(long playerId, int count, bool verbose)
        {
            if (verbose)
                return VerboseOwnedGameInfo.QueryRecent(Key, playerId, count);
            return OwnedGameInfo.QueryRecent(Key, playerId, count);
        }

        public OwnedGameInfo[] GetRecentGames(Friend friend, int count, bool verbose)
        {
            return GetRecentGames(friend.Id, count, verbose);
        }

        // Retrieves a player's friends list.
        public Friend[] GetPlayerFriends(long playerId)
        {
            return Friend.Query(Key, playerId);
        }

        public Friend[] GetPlayerFriends(Friend friend)
        {
            return GetPlayerFriends(friend.Id);
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

        public PlayerAchievement[] GetPlayerAchievements(Friend friend, int appId)
        {
            return GetPlayerAchievements(friend.Id, appId);
        }

        // Retrieves player statistics of a particular game.
        public PlayerStatistic[] GetPlayerStatistics(long playerId, int appId)
        {
            return PlayerStatistic.Query(Key, playerId, appId);
        }

        public PlayerStatistic[] GetPlayerStatistics(Player player, int appId)
        {
            return GetPlayerStatistics(player.Summary.Id, appId);
        }

        public PlayerStatistic[] GetPlayerStatistics(Friend friend, int appId)
        {
            return GetPlayerStatistics(friend.Id, appId);
        }

        // Retrieve player Id.
        public long FindPlayerId(string name)
        {
            long id = IdFinder.GetIdFromName(Key, name);
            if (id != -1)
                return id;

            id = IdFinder.GetIdFromUrl(Key, name);
            return id;
        }
    }
}
