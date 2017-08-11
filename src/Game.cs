using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Steam game.
    public class Game : SSWAObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Version { get; set; }
        public Achievement[] Achievements { get; set; }
        public Statistic[] Statistics { get; set; }

        // Constructor for a game.
        public Game()
        {
            Id = -1;
            Title = null;
            Version = -1;
            Achievements = new Achievement[0];
            Statistics = new Statistic[0];
        }

        // Checks if this game has an appId.
        public bool HasId()
        {
            return Id != -1;
        }

        // Checks if this game has a title.
        public bool HasTitle()
        {
            return Title != null;
        }

        // Checks if this game has a version.
        public bool HasVersion()
        {
            return Version != -1;
        }

        // Checks if a game has achievements.
        public bool HasAchievements()
        {
            return Achievements.Length != 0;
        }

        // Checks if a game has statistics.
        public bool HasStatistics()
        {
            return Statistics.Length != 0;
        }

        // Parse xml formatted game to an object.
        private static Game Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);
            Game result = new Game
            {
                Title = parser.GetAttributeString("gameName"),
                Version = parser.GetAttributeInteger("gameVersion"),
                Achievements = ParseAchievements(xml),
                Statistics = ParseStatistics(xml)
            };

            return result;
        }

        // Parses all achievements.
        private static Achievement[] ParseAchievements(XElement xml)
        {
            XElement[] achievements = xml.Descendants("achievement").ToArray();
            Achievement[] result = new Achievement[achievements.Length];

            for (int i = 0; i < achievements.Length; ++i)
                result[i] = Achievement.Parse(achievements[i]);

            return result;
        }

        // Parses all Statistics.
        private static Statistic[] ParseStatistics(XElement xml)
        {
            XElement[] statistics = xml.Descendants("stat").ToArray();
            Statistic[] result = new Statistic[statistics.Length];

            for (int i = 0; i < statistics.Length; ++i)
                result[i] = Statistic.Parse(statistics[i]);

            return result;
        }

        // Queries all AchievementPercentages
        private static Dictionary<string, double> QueryAchievementPercentages(int appId)
        {
            string url = String.Format("http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid={0}&format=xml", appId);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new Dictionary<string, double>();

            XElement[] items = xml.Descendants("achievement").ToArray();

            Dictionary<string, double> result = new Dictionary<string, double>();

            for (int i = 0; i < items.Length; ++i)
            {
                Tuple<string, double> percentageTuple = Achievement.ParseAchievementPercentage(items[i]);
                result.Add(percentageTuple.Item1, percentageTuple.Item2);
            }

            return result;
        }

        // Queries the game info.
        private static Game QueryGameInfo(string apikey, int appId)
        {
            string url = String.Format("http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2/?key={0}&appid={1}&format=xml", apikey, appId);
            XDocument xml = GetXML(url);

            if (xml == null)
                return new Game();

            Game result = Parse(xml.Element("game"));
            result.Id = appId;

            return result;
        }

        // Queries all game info/achievement/statistics.
        public static Game Query(string apikey, int appId)
        {
            Game game = QueryGameInfo(apikey, appId);
            Dictionary<string, double> percentages = QueryAchievementPercentages(appId);

            foreach (Achievement achievement in game.Achievements)
                achievement.Percentage = percentages[achievement.Name];

            return game;
        }

    }
}
