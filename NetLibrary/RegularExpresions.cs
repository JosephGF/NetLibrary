using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetLibrary
{
    public class RegularExpresions
    {
        public static Dictionary<string, string> GetValues(string regexp, string text, RegexOptions rxOptions)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            Regex rx = new Regex(regexp, rxOptions);
            foreach (Match match in rx.Matches(text))
            {
                for (int i = 1; i < match.Groups.Count; i++)
                {
                    var group = match.Groups[i];
                    if (group.Success)
                    {
                        string value = group.Value.ToString();
                        string key = rx.GroupNameFromNumber(i);
                        if (!String.IsNullOrEmpty(key))
                            results.Add(key, value);
                    }
                }
            }

            return results;
        }
        public static Dictionary<string, string> GetValues(string regexp, string text)
        {
            return GetValues(regexp, text, RegexOptions.IgnoreCase);
        }
    }
}
