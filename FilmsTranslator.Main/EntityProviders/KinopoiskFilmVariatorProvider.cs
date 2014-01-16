using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityProviders
{
    public class KinopoiskFilmVariatorProvider
    {
        private EntityContext db;

        public KinopoiskFilmVariatorProvider()
        {
            db = DB.Init;
        }

        public void AddFilmVariators(List<KinopoiskFilmVariator> filmsVariation)
        {
            for (int i = 0; i < filmsVariation.Count; i++)
            {
                var filmLoc = filmsVariation[i];
                var filmVariantDb = db.KinopoiskFilmVariators.FirstOrDefault(
                    v => v.NewTitleId == filmLoc.NewTitleId && v.Title == filmLoc.Title);
                if (filmVariantDb == null)
                {
                    db.KinopoiskFilmVariators.Add(filmLoc);
                }
                else
                {
                    filmsVariation[i] = filmVariantDb;
                }
            }
            db.SaveChanges();
        }
    }
}
