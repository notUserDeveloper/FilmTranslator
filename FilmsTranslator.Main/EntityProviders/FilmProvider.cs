using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityProviders
{
    public class FilmProvider
    {
        private readonly EntityContext db;

        public FilmProvider()
        {
            db = DB.Init;
        }

        public void AddFilms(List<Film> films)
        {
            films.ForEach(f =>
            {
                if (db.Films.FirstOrDefault(dbFilm => dbFilm.StartTitle == f.StartTitle) == null)
                {
                    db.Films.Add(f);
                }
            });
            db.SaveChanges();
        }

        public IEnumerable<Film> GetFilmsChecked(bool isChecked)
        {
            return (from film in db.Films
                where
                    film.Checked == isChecked
                select film);
        }

        public void SetChecked(int filmId, bool isChecked = true)
        {
            var film = db.Films.FirstOrDefault(f => f.Id == filmId);
            if (film != null)
            {
                film.Checked = isChecked; 
                db.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}