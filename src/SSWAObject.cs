using System.IO;
using System.Net;
using System.Xml.Linq;

namespace SharpSteamWebApi
{
    public abstract class SSWAObject
    {
        // Returns the retrieved XML content from a url.
        public static XDocument GetXML(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            WebResponse res = req.GetResponse();
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
