using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityManagers
{
    public class RightWordManager
    {
        public List<string> GetAllWords()
        {
            var db = DB.Init;
            return db.RightWords.Select(w => w.Word).ToList();
        }
    }
}
