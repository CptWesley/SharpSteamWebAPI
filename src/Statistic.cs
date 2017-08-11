using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Steam statistic.
    public class Statistic
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int DefaultValue { get; set; }

        // Constructor for a statistic.
        public Statistic()
        {
            Name = null;
            DisplayName = null;
            DefaultValue = 0;
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

        // Parse xml formatted statistic to an object.
        public static Statistic Parse(XElement xml)
        {
            if (xml == null)
                return null;

            ElementParser parser = new ElementParser(xml);
            Statistic result = new Statistic
            {
                Name = parser.GetAttributeString("name"),
                DisplayName = parser.GetAttributeString("displayName"),
                DefaultValue = parser.GetAttributeInteger("defaultvalue")
            };

            return result;
        }
    }
}
