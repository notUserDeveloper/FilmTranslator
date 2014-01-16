using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using FilmsTranslator.Main.Models;

namespace FilmsTranslator.Main.Code.Parsers
{
    public class KinopoiskParser
    {
        private readonly HttpRequester _httpRequester;

        private const string PatternFilm = @"\s*<h1 class=""moviename-big"" itemprop=""name"">(?<RusName>[^<]*)\s*.*alternativeHeadline"">(?<EngName>.*?)</span>.*?year.*?>(?<Year>.*?)</a>.*?country.*?>(?<Country>.*?)</a>.*?director"">(?:<a.*?>)?(?<Producer>[^<]*)(?:</a>)?.*?genre""><a.*?>(?<Genre>.*?)</a>";
        private const string PatternFilms = @"<p class=""name""><a href=""(?<url>[^""]*)"">(?<name>[^<]*)</a> <span class=""year"">(?<year>\d*)</span></p>";
        private readonly string[] _splitFilmActor = { @">Найденные имена</a>" };
        private readonly Regex _regexFilmGrab = new Regex(PatternFilm, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private readonly Regex _regexFilmsGrab = new Regex(PatternFilms, RegexOptions.Singleline | RegexOptions.IgnoreCase);

        public KinopoiskParser(HttpRequester httpRequester)
        {
            _httpRequester = httpRequester;
        }

        public Kinopoisk GetFilmInfo(string filmUrl)
        {
            var content = _httpRequester.DoRequest("http://www.kinopoisk.ru" + filmUrl);
            if (content == null || content.Contains("К сожалению, по вашему запросу ничего не найдено"))
            {
                return null;
            }
            return ParseFilm(HttpUtility.HtmlDecode(content));
        }

        private Kinopoisk ParseFilm(string content)
        {
            Match parse = _regexFilmGrab.Match(content);
            Func<string, double> getDouble = s =>
                {
                    double rez;
                    if (Double.TryParse(s, out rez))
                    {
                        return rez;
                    }
                    return 0;
                };
            return new Kinopoisk
                {
                    //Budget = getDouble(parse.Groups["Budget"].Value),
                    Country = parse.Groups["Country"].Value,
                    EngName = parse.Groups["EngName"].Value,
                    Genre = parse.Groups["Genre"].Value,
                    Producer = parse.Groups["Producer"].Value,
                    //Rating = getDouble(parse.Groups["Rating"].Value),
                    RusName = parse.Groups["RusName"].Value,
                    Year = parse.Groups["Year"].Value
                };
        }

        public List<KinopoiskFilmVariator> GetFilmVariations(string predictor)
        {
            var content = _httpRequester.DoRequest("http://www.kinopoisk.ru/index.php?first=no&what=&kp_query=" + predictor);
            content = HttpUtility.HtmlDecode(content.Split(_splitFilmActor, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault());
            return ParseFilms(content);
        }

        private List<KinopoiskFilmVariator> ParseFilms(string content)
        {
            var matches = _regexFilmsGrab.Matches(content);
            var films = new List<KinopoiskFilmVariator>();
            foreach (Match match in matches)
            {
                films.Add(new KinopoiskFilmVariator
                {
                    Title = match.Groups["name"].Value,
                    Year = match.Groups["year"].Value,
                    Url = match.Groups["url"].Value
                });
            }
            return films;
        }
    }
}