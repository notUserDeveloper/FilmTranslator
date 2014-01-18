using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FilmsTranslator.Main.Code.Helpers
{
    public static class YearHelper
    {
        private const string PatternYear = "^\\d{4}$";
        private const int StartFilmYear = 1900;

        /// <summary>
        /// Пытается вынуть год начиная с права
        /// </summary>
        /// <param name="text"></param>
        /// <param name="skipLeft">Сколько слов слева не считать годом</param>
        /// <returns></returns>
        public static int? GetYear(string text, int skipLeft = 0)
        {
            var filmWords = StringHelper.Split(text).Skip(skipLeft).Reverse();
            foreach (string filmWord in filmWords)
            {
                if (IsYear(filmWord))
                {
                    return filmWord.ToInt();
                }
            }
            return null;
        }

        public static bool IsYear(string text)
        {
            var year = text.ToInt();
            if (year == null)
            {
                return false;
            }
            return Regex.IsMatch(text, PatternYear)
                   && year > StartFilmYear
                   && year <= DateTime.Now.Year;
        }

        /// <summary>
        /// Удаляет год если он справа
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <returns>Если текст из одного года, вырезки не будет</returns>
        public static string CutYear(string text, char separator = ' ')
        {
            var words = text.Split(separator).Reverse();
            if (words.Count() == 1)
            {
                return text;
            }
            if (IsYear(words.First()))
            {
                return String.Join(separator.ToString(), words.Skip(1).Reverse());
            }
            return text;
        }
    }
}