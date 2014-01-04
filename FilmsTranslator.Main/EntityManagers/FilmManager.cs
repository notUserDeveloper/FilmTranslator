using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityManagers
{
    public class FilmManager
    {
        private readonly EntityContext db;

        public FilmManager()
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

        public IEnumerable<Film> GetFilmsChecked(bool flag)
        {
            return (from film in db.Films
                where
                    film.Checked == flag
                select film);
        }

        public void SetChecked(int filmId, bool flag = true)
        {
            var film = db.Films.FirstOrDefault(f => f.Id == filmId);
            if (film != null)
            {
                film.Checked = flag; 
                db.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}