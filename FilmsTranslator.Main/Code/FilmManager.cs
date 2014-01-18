using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FilmsTranslator.Main.Code.Helpers;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code
{
    public class FilmManager
    {
        public IEnumerable<Film> GetFilms(string path)
        {
            var films = new List<Film>();
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                var fileInfo = new FileInfo(file);
                films.Add(new Film
                {
                    AddDate = fileInfo.CreationTime,
                    Size = fileInfo.Length,
                    StartTitle = Path.GetFileNameWithoutExtension(fileInfo.Name),
                    Extension = Path.GetExtension(fileInfo.Name),
                    Year = YearHelper.GetYear(fileInfo.Name, 1),
                    Checked = false
                });
            }
            return films;
        }
    }
}