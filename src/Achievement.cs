using System;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Steam achievement.
    public class Achievement
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int DefaultValue { get; set; }
        public bool Hidden { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string LockedIcon { get; set; }
        public double Percentage { get; set; }

        // Constructor for an achievement.
        public Achievement()
        {
            Name = null;
            DisplayName = null;
            DefaultValue = 0;
            Hidden = false;
            Description = null;
            Icon = null;
            LockedIcon = null;
            Percentage = -1;
        }

        // Checks if the achievement has a name.
        public bool HasName()
        {
            return Name != null;
        }

        // Checks if the achievement has a display name.
        public bool HasDisplayName()
        {
            return DisplayName != null;
        }

        // Checks if the achievement has a description.
        public bool HasDescription()
        {
            return Description != null;
        }

        // Checks if the achievement has an icon.
        public bool HasIcon()
        {
            return Icon != null;
        }

        // Checks if the achievement has a locked icon.
        public bool HasLockedIcon()
        {
            return LockedIcon != null;
        }

        // Checks if the achievement has a percentage.
        public bool HasPercentage()
        {
            return Percentage != -1;
        }

        // Parse xml formatted achievement to an object.
        public static Achievement Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);
            Achievement result = new Achievement
            {
                Name = parser.GetAttributeString("name"),
                DisplayName = parser.GetAttributeString("displayName"),
                DefaultValue = parser.GetAttributeInteger("defaultvalue"),
                Hidden = parser.GetAttributeBoolean("hidden"),
                Description = parser.GetAttributeString("description"),
                Icon = parser.GetAttributeString("icon"),
                LockedIcon = parser.GetAttributeString("icongray")
            };

            return result;
        }

        // Parses xml formatted achievement percentages to an object.
        public static Tuple<string, double> ParseAchievementPercentage(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);

            Tuple<string, double> result = new Tuple<string, double>(parser.GetAttributeString("name"), parser.GetAttributeDouble("percent"));

            return result;
        }
    }
}
