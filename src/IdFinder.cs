using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    // Finds Id's based on vanity url names and urls.
    public static class IdFinder
    {
        // Gets a steam id from a vanity profile name.
        public static long GetIdFromName(string apikey, string name)
        {
            string url = String.Format("https://api.steampowered.com/ISteamUser/ResolveVanityURL/v1?key={0}&vanityurl={1}&format=xml", apikey, name);
            XDocument xml = GetXML(url);

            if (xml == null)
                return -1;

            return Parse(xml.Element("response"));
        }

        // Gets a steam Id from a full profile url.
        public static long GetIdFromUrl(string apikey, string url)
        {
            string idPattern = @"\/profiles\/(\d+)\/?";
            Regex idRegex = new Regex(idPattern);
            Match idMatch = idRegex.Match(url);
            if (idMatch.Success)
                return long.Parse(idMatch.Groups[1].Value);

            string namePattern = @"\/id\/(.+)\/?";
            Regex nameRegex = new Regex(namePattern);
            Match nameMatch = nameRegex.Match(url);
            if (nameMatch.Success)
                return GetIdFromName(apikey, nameMatch.Groups[1].Value);

            return -1;
        }

        // Parses id getter results.
        private static long Parse(XElement xml)
        {
            if (xml == null)
                return -1;

            ElementParser parser = new ElementParser(xml);
            return parser.GetAttributeLong("steamid");
        }

        // Returns the retrieved XML content from a url.
        private static XDocument GetXML(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
            }
            catch
            {
                return null;
            }

            res.GetResponseStream();
            Stream stream = res.GetResponseStream();

            if (stream == null)
                return null;

            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            stream.Close();
            res.Close();

            return XDocument.Parse(result);
        }
    }
}
