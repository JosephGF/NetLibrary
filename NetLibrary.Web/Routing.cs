using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace NetLibrary.Web
{
    public class Routing
    {
        public static Dictionary<string, object> UrlToDictionary(string url)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            int idx = url.IndexOf('?') + 1;
            string strArgs = url.Substring(idx);
            string[] args = url.Split('&');

            foreach (string arg in args)
            {
                idx = url.IndexOf('=') + 1;
                string key = arg.Substring(0, idx);
                string value = arg.Substring(idx + 1);
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public static RouteValueDictionary UrlToRouteValueDictionary(string url)
        {
            Dictionary<string, object> dictionary = UrlToDictionary(url);
            return new RouteValueDictionary(dictionary);
        }
    }
}
