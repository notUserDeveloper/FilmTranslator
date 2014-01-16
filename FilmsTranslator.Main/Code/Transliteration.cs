using System.Collections.Generic;
using System.Text.RegularExpressions;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code
{
    public class Transliteration
    {
        private static readonly Dictionary<string, string> Iso = new Dictionary<string, string>
            {
				{"shh", "щ"},
				{"yo", "ё"},
                {"zh", "ж"},
				{"cz", "ц"},
                {"ch", "ч"},  
                {"sh", "ш"},
				{"yu", "ю"},
                {"ya", "я"},
				{"e`", "э"},
			    {"a", "а"},
                {"b", "б"},
                {"v", "в"},
                {"g", "г"},
                {"d", "д"},
                {"e", "е"},
                {"i", "и"},
                {"j", "й"},
                {"k", "к"},
                {"l", "л"},
                {"h", "х"},
                {"m", "м"},
                {"n", "н"},
                {"o", "о"},
                {"p", "п"},
                {"r", "р"},
                {"s", "с"},
                {"t", "т"},
                {"u", "у"},
                {"f", "ф"},
                {"x", "х"},
                {"y", "ы"},
				{"z", "з"},
				{"`", "ь"},
                {@"\.", " "}
            };

        public string DoTransliteration(string clearTitle)
        {
            var title = clearTitle;
            foreach (var latter in Iso)
            {
                title = Regex.Replace(title, latter.Key, latter.Value, RegexOptions.IgnoreCase);
            }
            return title;
        }
    }
}