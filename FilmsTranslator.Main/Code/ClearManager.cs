using System;
using System.Collections.Generic;
using System.Linq;
using FilmsTranslator.Main.Code.Helpers;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code
{
    public class ClearManager
    {
        public List<NewTitle> DoClearDictionary(IEnumerable<Film> films, IEnumerable<string> dictionary)
        {
            var newTitles = new List<NewTitle>();
            foreach (Film film in films)
            {
                List<string> words = StringHelper.Split(film.StartTitle).ToList();
                var clrString = new List<string>();
                words.ForEach(s =>
                {
                    if (!dictionary.Contains(s,StringComparer.CurrentCultureIgnoreCase))
                    {
                        clrString.Add(s);
                    }
                });
                newTitles.Add(new NewTitle {FilmId = film.Id, ClearTitle =  String.Join(" ", clrString), TryDate = DateTime.Now});
            }
            return newTitles;
        }

        public string DoClearYear(string text)
        {
            return YearHelper.CutYear(text);
        }
    }
}