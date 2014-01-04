using System;
using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Code;
using FilmsTranslator.Main.Code.Helpers;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.EntityManagers
{
    public class StatisticManager
    {

        public void AddStatistic(List<string> words)
        {
            var db = DB.Init;
            var groupWords = words.GroupBy(w => w).Select(g => new
            {
                Word = g.Key,
                Count = g.Count()
            });

            foreach (var word in groupWords)
            {
                var curWord = db.SubStatistics.FirstOrDefault(dbWord => dbWord.Word == word.Word);
                if (curWord == null)
                {
                    db.SubStatistics.Add(new SubStatistic {Count = word.Count, Word = word.Word});
                }
                else
                {
                    curWord.Count = curWord.Count + word.Count;
                }
            }
            db.SaveChanges();
        }

        public List<string> GetStatistic(List<Film> films)
        {
            var words = new List<string>();
            Action<Film> getStatistic = film => words.AddRange(StringHelper.Split(film.StartTitle));
            films.ForEach(getStatistic);
            return words;
        }

        public IEnumerable<string> GetWords()
        {
            EntityContext db = DB.Init;
            return (from stat in db.SubStatistics
                orderby stat.Count descending
                select stat.Word).Take(15);
        }
    }
}