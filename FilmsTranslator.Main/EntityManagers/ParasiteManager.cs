using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityManagers
{
    public class ParasiteManager
    {
        public List<string> GetAllWords()
        {
            var db = DB.Init;
            return db.Parasites.Select(w => w.Word).ToList();
        }
    }
}
