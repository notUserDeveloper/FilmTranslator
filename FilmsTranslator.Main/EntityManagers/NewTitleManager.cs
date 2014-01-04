using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityManagers
{
    public class NewTitleManager
    {
        private EntityContext db;

        public NewTitleManager()
        {
            db = DB.Init;
        }

        public void AddNewTitle(NewTitle resaultTransliteration)
        {
            if (db.NewTitles.FirstOrDefault(t => t.FilmId == resaultTransliteration.FilmId) == null)
            {
                db.NewTitles.Add(resaultTransliteration);
            }
            db.SaveChanges();
        }

        public void AddNewTitles(List<NewTitle> newTitles)
        {
            foreach (var newTitle in newTitles)
            {
                var newTitleDb = db.NewTitles.FirstOrDefault(t => t.FilmId == newTitle.FilmId);
                if (newTitleDb == null)
                {
                    db.NewTitles.Add(newTitle);
                }
                else
                {
                    
                }
            }
            db.SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public List<NewTitle> GetNewTitlesById(IEnumerable<int> id)
        {
            return db.NewTitles.Where(t => id.Contains(t.Id)).ToList();
        } 
  
        public List<NewTitle> GetNewTitleById(int id)
        {
            return db.NewTitles.Where(t => t.Id == id).ToList();
        }
    }
}
