using System.Collections.Generic;
using System.Linq;

namespace FilmsTranslator.Main.Code.Helpers
{
    public static class StringHelper
    {
        private const string CharsSeparator = ". _";

        public static IEnumerable<string> Split(string text)
        {
            IEnumerable<string> words = text.Split(CharsSeparator.ToCharArray())
                .Select(w => w.Trim())
                .Where(w => w.Length > 0);
            return words;
        }
    }
}