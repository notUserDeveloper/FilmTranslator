using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityProviders
{
    public class NewTitleProvider
    {
        private EntityContext db;

        public NewTitleProvider()
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

        /// <remarks>
        /// Превращает в наблюдаемую коллекцию
        /// </remarks>
        /// <param name="newTitles"></param>
        public void AddNewTitles(List<NewTitle> newTitles)
        {
            for(var i=0; i<newTitles.Count; i++)
            {
                var ntLoc = newTitles[i];
                var newTitleDb = db.NewTitles.FirstOrDefault(t => t.FilmId == ntLoc.FilmId);
                if (newTitleDb == null)
                {
                    db.NewTitles.Add(ntLoc);
                }
                else
                {
                    newTitles[i] = newTitleDb;
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
