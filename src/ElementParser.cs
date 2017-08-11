using System;
using System.Globalization;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    internal class ElementParser
    {
        private XElement _el;

        // Constructor for the parser.
        internal ElementParser(XElement element)
        {
            _el = element;
        }

        // Gets an attribute.
        internal XElement GetAttribute(string name)
        {
            return _el.Element(name);
        }

        // Checks if the element contains a certain attribute.
        internal bool HasAttribute(string name)
        {
            return _el.Element(name) != null;
        }

        // Returns a certain attribute as a string.
        internal string GetAttributeString(string name)
        {
            XElement attr = GetAttribute(name);
            if (attr == null)
                return null;

            return attr.Value;
        }

        // Returns a certain attribute as a long.
        internal long GetAttributeLong(string name)
        {
            XElement attr = GetAttribute(name);
            if (attr == null)
                return -1;

            long val = -1;
            long.TryParse(attr.Value, out val);

            return val;
        }

        // Returns a certain attribute as an integer.
        internal int GetAttributeInteger(string name)
        {
            XElement attr = GetAttribute(name);
            if (attr == null)
                return -1;

            int val = -1;
            int.TryParse(attr.Value, out val);

            return val;
        }

        // Returns a certain attribute as a double.
        internal double GetAttributeDouble(string name)
        {
            XElement attr = GetAttribute(name);
            if (attr == null)
                return -1;

            double val = -1;
            double.TryParse(attr.Value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out val);

            return val;
        }

        // Returns a certain attribute as a boolean.
        internal bool GetAttributeBoolean(string name)
        {
            XElement attr = GetAttribute(name);
            if (attr == null)
                return false;

            if (attr.Value == "true" || attr.Value == "1")
                return true;
            return false;
        }

        // Returns a certain attribute as a Date Time.
        internal DateTime GetAttributeDate(string name)
        {
            long secs = GetAttributeLong(name);
            if (secs == -1)
                return DateTime.MinValue;

            DateTime res = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            res = res.AddSeconds(secs);

            return res;
        }
    }
}
