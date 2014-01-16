using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityProviders
{
    public class RightWordProvider
    {
        public List<string> GetAllWords()
        {
            var db = DB.Init;
            return db.RightWords.Select(w => w.Word).ToList();
        }
    }
}
