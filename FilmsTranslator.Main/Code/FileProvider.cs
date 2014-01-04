using System.Collections.Generic;
using System.IO;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code
{
    public class FileProvider
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
                        Checked = false
                    });
            }
            return films;
        }
    }
}