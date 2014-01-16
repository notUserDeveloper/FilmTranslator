using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityProviders
{
    public class ParasiteProvider
    {
        public List<string> GetAllWords()
        {
            var db = DB.Init;
            return db.Parasites.Select(w => w.Word).ToList();
        }
    }
}
